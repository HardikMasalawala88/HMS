using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Data.ContextModels
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
               new Role
               {
                   Id=1,
                   Name= "Admin"
               },
               new Role
               {
                   Id = 2,
                   Name = "User"
               }
           );

            modelBuilder.Entity<User>().HasData(
               new User
               {
                   Id = 1,
                   Name = "Harry",
                   Address = "101,Adajan",
                   City = "Surat",
                   EmailId = "harryjain12@gmail.com",
                   MobileNo = "9879555131",
                   Gender = "Male",
                   RoleId = 1,
                   UserName = "Admin1",
                   Password = "Admin1"
               },
               new User
               {
                   Id = 2,
                   Name = "Sejal",
                   Address = "101,Paal",
                   City = "Surat",
                   EmailId = "sejal22@gmail.com",
                   MobileNo = "81569992354",
                   Gender = "FeMale",
                   RoleId = 2,
                   UserName = "User1",
                   Password = "User1"
               }
           );
        }
    }
}
