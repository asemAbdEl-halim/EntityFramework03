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
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
            builder.HasKey(c => c.CourseId);
            builder.Property(c => c.Title).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Description).HasMaxLength(2000);
            builder.Property(c => c.Price).HasPrecision(10, 2).IsRequired();
            builder.Property(c => c.Level).IsRequired();
            builder.Property(c => c.IsPublished).IsRequired();

            builder.HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Category)
                .WithMany(c => c.Courses)
                .HasForeignKey(c => c.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Course { CourseId = 1, Title = "C# Basics", Description = "Learn C# from zero", Price = 500m, Level = CourseLevel.Beginner, IsPublished = true, PublishedDate = new DateTime(2024, 4, 1), InstructorId = 1, CategoryId = 2 },
                new Course { CourseId = 2, Title = "ASP.NET Core", Description = "Build web APIs", Price = 900m, Level = CourseLevel.Intermediate, IsPublished = true, PublishedDate = new DateTime(2024, 4, 5), InstructorId = 1, CategoryId = 3 },
                new Course { CourseId = 3, Title = "HTML and CSS", Description = "Frontend foundations", Price = 300m, Level = CourseLevel.Beginner, IsPublished = true, PublishedDate = new DateTime(2024, 5, 1), InstructorId = 2, CategoryId = 4 },
                new Course { CourseId = 4, Title = "SQL Server", Description = "Relational databases", Price = 650m, Level = CourseLevel.Intermediate, IsPublished = true, PublishedDate = new DateTime(2024, 5, 10), InstructorId = 3, CategoryId = 5 },
                new Course { CourseId = 5, Title = "Advanced C#", Description = "Advanced language topics", Price = 1100m, Level = CourseLevel.Advanced, IsPublished = false, PublishedDate = null, InstructorId = 1, CategoryId = 2 },
                new Course { CourseId = 6, Title = "JavaScript", Description = "JavaScript essentials", Price = 450m, Level = CourseLevel.Beginner, IsPublished = false, PublishedDate = null, InstructorId = 2, CategoryId = 4 }
            );
        }
    }

}
