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
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.ToTable("Instructors");
            builder.HasKey(i => i.InstructorId);

            builder.Property(i => i.FullName).IsRequired().HasMaxLength(150);
            builder.Property(i => i.Email).IsRequired().HasMaxLength(200);
            builder.Property(i => i.Bio).HasMaxLength(500);
            builder.Property(i => i.JoinedDate).IsRequired();

            builder.HasData(
                new Instructor { InstructorId = 1, FullName = "Ahmed Hassan", Email = "ahmed@example.com", Bio = "C# instructor", JoinedDate = new DateTime(2024, 1, 10) },
                new Instructor { InstructorId = 2, FullName = "Mona Ali", Email = "mona@example.com", Bio = "Web development instructor", JoinedDate = new DateTime(2024, 2, 15) },
                new Instructor { InstructorId = 3, FullName = "Omar Samy", Email = "omar@example.com", Bio = null, JoinedDate = new DateTime(2024, 3, 20) }
            );
        }
    }
}
