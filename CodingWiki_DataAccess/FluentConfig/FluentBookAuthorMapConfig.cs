using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_DataAccess.FluentConfig
{
    public class FluentBookAuthorMapConfig : IEntityTypeConfiguration<Fluent_BookAuthorMap>
    {
        public void Configure(EntityTypeBuilder<Fluent_BookAuthorMap> modelBuilder)
        {
            modelBuilder.HasKey(u => new { u.BookId, u.AuthorId });
            modelBuilder.HasOne(b => b.Book)
                .WithMany(b => b.BookAuthorMap)
                .HasForeignKey(u => u.BookId);
            modelBuilder.HasOne(b => b.Author)
                .WithMany(b => b.BookAuthorMap)
                .HasForeignKey(u => u.AuthorId);
        }
    }
}
