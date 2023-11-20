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
    internal class BlogContentConfiguration : IEntityTypeConfiguration<BlogContent>
    {
        public void Configure(EntityTypeBuilder<BlogContent> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Content).IsRequired();
        }
    }
}
