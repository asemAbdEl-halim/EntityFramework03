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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c => c.CategoryId);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(500);

            builder.HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Category { CategoryId = 1, Name = "Programming", Description = "Programming courses", ParentCategoryId = null },
                new Category { CategoryId = 2, Name = "Backend", Description = "Backend development", ParentCategoryId = 1 },
                new Category { CategoryId = 3, Name = "ASP.NET", Description = "ASP.NET courses", ParentCategoryId = 2 },
                new Category { CategoryId = 4, Name = "Frontend", Description = "Frontend development", ParentCategoryId = 1 },
                new Category { CategoryId = 5, Name = "Databases", Description = "Database courses", ParentCategoryId = 2 }
            );
        }
    }

}
