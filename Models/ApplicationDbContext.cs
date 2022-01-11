using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhoneTrackerOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneTracker.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<User> CallerUsers { get; set; }
        public DbSet<TargetPhone> TargetPhones { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
