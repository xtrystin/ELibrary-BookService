using ELibrary_BookService.Domain.Entity;
using ELibrary_BookService.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;

namespace ELibrary_BookService.Domain.EF.Config;

internal class BookEntityTypeConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.ToTable("Book");

        builder.Property<Description>("_description")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Description")
            .HasMaxLength(Description.MaxLength)
            .HasConversion(v => v.Value, v => new Description(v));
        
        builder.Property<Title>("_title")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Title")
            .HasMaxLength(Title.MaxLength)
            .HasConversion(v => v.Value, v => new Title(v));

        builder.Property<DateTime>("_createdDate")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("CreatedDate");

        builder.Property<string>("_imageUrl")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("ImageUrl");

        builder.Property<int>("_bookAmount")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("BookAmount");

        builder.Property<string>("_pdfUrl")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("PdfUrl")
            .IsRequired(false);

        //Set as field (New since EF 1.1) to access the OrderItem collection property through its field
       /* builder.Metadata.FindNavigation(nameof(Book.Autors))
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata.FindNavigation(nameof(Book.Categories))
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata.FindNavigation(nameof(Book.Tage))
            .SetPropertyAccessMode(PropertyAccessMode.Field);*/


    }
}