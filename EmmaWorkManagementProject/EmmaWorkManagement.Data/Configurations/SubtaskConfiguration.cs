using EmmaWorkManagement.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmmaWorkManagement.Data.Configurations
{
    public class SubtaskConfiguration : IEntityTypeConfiguration<Subtask>
    {
        public void Configure(EntityTypeBuilder<Subtask> builder)
        {
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Name).IsRequired();
            builder.Property(q => q.Comment).IsRequired();

            builder.HasOne(q => q.UserTask)
                   .WithMany(q => q.Subtasks);
        }
    }
}
