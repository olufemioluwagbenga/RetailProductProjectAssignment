using Microsoft.EntityFrameworkCore;
using StockaProSSO.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockaProSSO.Core.Domain
{
    public partial class StockaProContext : DbContext 
    {
        public StockaProContext()
        {
        }

        public StockaProContext(DbContextOptions<StockaProContext> options)
            : base(options)
        {
        }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audit>().ToTable("Audit");
            //modelBuilder.Entity<UserClient>().ToTable("UserClient");
            //modelBuilder.Entity<UserScope>().ToTable("UserScope");
            //modelBuilder.Entity<AdminClearance>().ToTable("AdminClearance");
            base.OnModelCreating(modelBuilder);
        }
    }


  

}
