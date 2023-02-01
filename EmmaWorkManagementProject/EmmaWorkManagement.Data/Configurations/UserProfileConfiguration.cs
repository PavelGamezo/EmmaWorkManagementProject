using EmmaWorkManagement.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmmaWorkManagement.Entities.Entities;

namespace EmmaWorkManagement.Data.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Name).IsRequired().HasMaxLength(50);
            builder.Property(q => q.Surname).IsRequired().HasMaxLength(50);
            builder.Property(q => q.Email).IsRequired().HasMaxLength(50);
            builder.Property(q => q.About).IsRequired();
            builder.Property(q => q.Registered).IsRequired();
        }
    }
}
