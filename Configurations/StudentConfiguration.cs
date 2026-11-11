using EntityFramework03.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework03.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(s => s.StudentId);
            builder.Property(s => s.FullName).IsRequired().HasMaxLength(150);
            builder.Property(s => s.Email).IsRequired().HasMaxLength(200);
            builder.HasIndex(s => s.Email).IsUnique();
            builder.Property(s => s.DateOfBirth).IsRequired();
        }
    }

}
