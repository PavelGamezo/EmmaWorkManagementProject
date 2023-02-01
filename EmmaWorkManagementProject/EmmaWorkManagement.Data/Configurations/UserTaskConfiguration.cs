using EmmaWorkManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmmaWorkManagement.Data.Configurations
{
    public class UserTaskConfiguration : IEntityTypeConfiguration<UserTask>
    {
        public void Configure(EntityTypeBuilder<UserTask> builder)
        {
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Name).IsRequired().HasMaxLength(50);
            builder.Property(q => q.Summary).IsRequired().HasMaxLength(1000);
            builder.Property(q => q.Priority).IsRequired();
            builder.Property(q => q.DateOfCompletion).IsRequired();

            builder.HasOne(q => q.Account)
                   .WithMany(q => q.UserTasks);
        }
    }
}
