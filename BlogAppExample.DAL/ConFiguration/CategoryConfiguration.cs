using BlogAppExample.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAppExample.DAL.ConFiguration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
           builder.Property(c=>c.CategoryName).IsRequired().HasMaxLength(30);
           builder.Property(c=>c.CategoryDescription).IsRequired().HasMaxLength(100);
        }
    }
}
