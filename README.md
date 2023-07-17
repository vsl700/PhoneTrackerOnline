# Phone Tracker - Online
This project consists of two modules: 
- a web application, written in C# (ASP.Net)
- an Android application, written in Java, link: https://github.com/vsl700/GPSTrackerOnline

The goal of this project is, for example, to help parents track their kids' location. Here's how it works:
- The tracked devices (of the kids, for example) must have the 'GPS Tracker - Online' application installed
- The trackers (parents) must have registered and logged into the system, either through the web browser or the mobile app
- The trackers must create records for the tracked devices in their accounts, by entering some sort of a name to each tracked device ('My Son', 'My Wife' etc.). Each record has a randomly generated 6-digit code
- Each tracked device must initially enter the corresponding 6-digit code to be able to connect with the tracker.
- The trackers can see the target devices' current location, as well as their location history
- When not in use, the mobile app's tracking service can be terminated to increase battery life
- In case the tracking service on the target phone is not running (forgotten to be turned on), the tracker can send a special SMS to the target phone, which will be detected by the mobile app and the app will turn the service on
- In case there's no internet on a tracked device, the tracker can send a special SMS to the target phone, and in return the tracker will receive an SMS containing:
  - the current GPS location, as well as the location history, in case there is a GPS service available on the target phone
  - the last detected GPS location, as well as the location history, in case there is no GPS service available on the target phone

The project uses 'SignalR' for the websocket connection (for current location) between the server and the devices (browser & mobile app). The web application uses 'MS Sql Server 2019' as its database, whereas the mobile application uses 'SQLite'. The maps used in the web page and the mobile app are from Google.

## Infrastructure
On the picture below you can see the way different devices connect to the web app
![phtron%20System%20Scheme.png](https://github.com/vsl700/PhoneTrackerOnline/blob/readme/readme_pics/phtron%20System%20Scheme.png)

### API methods & usage
(caller = tracker)
![phtron%20API%201-pdnscheme.png](https://github.com/vsl700/PhoneTrackerOnline/blob/readme/readme_pics/phtron%20API%201-pdnscheme.png)
![phtron%20API%202-pdnscheme.png](https://github.com/vsl700/PhoneTrackerOnline/blob/readme/readme_pics/phtron%20API%202-pdnscheme.png)
![phtron%20API%203-pdnscheme.png](https://github.com/vsl700/PhoneTrackerOnline/blob/readme/readme_pics/phtron%20API%203-pdnscheme.png)

### Database structures
The picture below shows the database structure of the web application (all tables have an 'id' column as primary key)
![phtron%20DB%20Structure.png](https://github.com/vsl700/PhoneTrackerOnline/blob/readme/readme_pics/phtron%20DB%20Structure.png)

The picture below shows the database structure of the mobile application (the 'Locations' table has an 'id' column as primary key)
![phtron%20Mobile%20DB%20Structure.png](https://github.com/vsl700/PhoneTrackerOnline/blob/readme/readme_pics/phtron%20Mobile%20DB%20Structure.png)

## Screenshots
Here you can see some screenshots of the web and mobile applications
### The track page of the web application
'phone1' and 'phone2' are online and are visible on the map
![Screenshot%202022-02-16%20181916.png](https://github.com/vsl700/PhoneTrackerOnline/blob/readme/readme_pics/Screenshot%202022-02-16%20181916.png)

### Adding new target phones
![Screenshot%202022-02-27%20225440.png](https://github.com/vsl700/PhoneTrackerOnline/blob/readme/readme_pics/Screenshot%202022-02-27%20225440.png)

### Location history of a target phone
![Screenshot%202022-02-27%20225425.png](https://github.com/vsl700/PhoneTrackerOnline/blob/readme/readme_pics/Screenshot%202022-02-27%20225425.png)

### Mobile app - tracker's view
![phtron%20Mobile%20App%20Caller.jpg](https://github.com/vsl700/PhoneTrackerOnline/blob/readme/readme_pics/phtron%20Mobile%20App%20Caller.jpg)

### Mobile app - target's view
![phtron%20Mobile%20App%20Target.jpg](https://github.com/vsl700/PhoneTrackerOnline/blob/readme/readme_pics/phtron%20Mobile%20App%20Target.jpg)
