using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Reviews;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Configurations
{
    internal sealed class ReviewsConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("reviews");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasConversion(reviewId => reviewId!.Value, value => new ReviewId(value));



            builder.Property(r => r.Rating)
                .HasConversion(r => r!.Value, value => Rating.Create(value).Value);

            builder.Property(r => r.Comentario)
                .HasMaxLength(200)
                .HasConversion(c => c!.Value, value => new Comentario(value));

            builder.HasOne<Vehiculo>()
                .WithMany()
                .HasForeignKey(r => r.VehiculoId);

            builder.HasOne<Alquiler>()
                .WithMany()
                .HasForeignKey(r => r.AlquilerId);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.UserId);









        }
    }
}
