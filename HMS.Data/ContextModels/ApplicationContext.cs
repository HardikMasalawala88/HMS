using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Data.ContextModels
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
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
                   RoleId = 1,
                   Name = "SuperAdmin"
               },
               new Role
               {
                   RoleId = 2,
                   Name = "Admin"
               },
               new Role
               {
                   RoleId = 3,
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
                   Username = "SuperAdmin1",
                   Password = "SuperAdmin1"
               }
           );
        }
    }
}
