﻿using Microsoft.AspNetCore.SignalR;
using PhoneTracker.Models;
using PhoneTrackerOnline.Interface;
using PhoneTrackerOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneTrackerOnline.Hubs
{
    public class NotificationUserHub : Hub
    {
        private readonly IUserConnectionManager _userConnectionManager;
        private readonly ApplicationDbContext _db;

        public NotificationUserHub(IUserConnectionManager userConnectionManager, ApplicationDbContext db)
        {
            _userConnectionManager = userConnectionManager;
            _db = db;
        }

        public override Task OnConnectedAsync()
        {
            GetConnectionId();

            string userId = Context.GetHttpContext().Request.Query["userId"];
            if (IsACaller(userId))
            {
                SendTrackRequest(userId, "trackin");
            }
            else
            {
                foreach(var phone in _db.TargetPhones)
                {
                    if(phone.Code == int.Parse(userId))
                    {
                        string callerUsername = _db.CallerUsers.Find(phone.UserID).Username;
                        var callerConnections = _userConnectionManager.GetUserConnections(callerUsername);

                        var connections = _userConnectionManager.GetUserConnections(userId);
                        if (callerConnections != null && callerConnections.Count > 0) // If the Caller is tracking at the time the Target (re)connects
                        {
                            Clients.Client(connections[0]).SendAsync("sendToTarget", "trackin");
                        }
                        else
                        {
                            Clients.Client(connections[0]).SendAsync("sendToTarget", "no-trackin"); // That keeps the application's socket alive, otherwise it may close right after starting back
                        }

                        break;
                    }
                }
            }
            

            return base.OnConnectedAsync();
        }

        public string GetConnectionId()
        {
            var httpContext = this.Context.GetHttpContext();
            var userId = httpContext.Request.Query["userId"];
            _userConnectionManager.KeepUserConnection(userId, Context.ConnectionId); 
            return Context.ConnectionId;
        }
        //Called when a connection with the hub is terminated.
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            //get the connectionId
            var connectionId = Context.ConnectionId;
            _userConnectionManager.RemoveUserConnection(connectionId);

            string userId = Context.GetHttpContext().Request.Query["userId"];
            if (IsACaller(userId))
            {
                var connections = _userConnectionManager.GetUserConnections(userId);
                if (connections == null || connections.Count == 0)
                    SendTrackRequest(userId, "no-trackin");
            }

            var value = await Task.FromResult(0);
        }

        private void SendTrackRequest(string userId, string message)
        {
            foreach (var phone in _db.TargetPhones)
            {
                var user = _db.CallerUsers.Find(phone.UserID);
                if (user != null && user.Username == userId)
                {
                    var connections = _userConnectionManager.GetUserConnections(phone.Code + "");
                    if (connections != null && connections.Count > 0)
                    {
                        Clients.Client(connections[0]).SendAsync("sendToTarget", message);
                    }
                }
            }
        }

        private bool IsACaller(string userId)
        {
            foreach(char ch in userId)
            {
                if (char.IsLetter(ch))
                    return true;
            }

            return false;
        }
    }
}
