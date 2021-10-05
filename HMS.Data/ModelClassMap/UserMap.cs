using HMS.Data.ContextModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Data.ModelClassMap
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Name).IsRequired();
            entityBuilder.Property(t => t.EmailId).IsRequired();
            entityBuilder.Property(t => t.Password).IsRequired();
            entityBuilder.Property(t => t.MobileNo).IsRequired();
            entityBuilder.Property(t => t.City).IsRequired();
            entityBuilder.Property(t => t.Gender).IsRequired();
            entityBuilder.Property(t => t.Username).IsRequired();
            entityBuilder.Property(t => t.RoleId).IsRequired();
        }
    }
}
