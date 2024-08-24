using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Shared;
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
    internal sealed class AlquilerConfiguration : IEntityTypeConfiguration<Alquiler>
    {
        public void Configure(EntityTypeBuilder<Alquiler> builder)
        {
            builder.ToTable("alquileres");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasConversion(alquilerId => alquilerId!.Value, value => new AlquilerId(value));

            builder.OwnsOne(a => a.PrecioPorPeriodo, precioBulder =>
            {
                precioBulder.Property(moneda => moneda.TipoMoneda)
                .HasConversion(tipoM => tipoM.Codigo, cd => TipoMoneda.FromCodigo(cd!));
            });


            builder.OwnsOne(a => a.Mantenimiento, precioBulder =>
            {
                precioBulder.Property(moneda => moneda.TipoMoneda)
                .HasConversion(tipoM => tipoM.Codigo, cd => TipoMoneda.FromCodigo(cd!));
            });

            builder.OwnsOne(a => a.Accesorios, precioBulder =>
            {
                precioBulder.Property(moneda => moneda.TipoMoneda)
                .HasConversion(tipoM => tipoM.Codigo, cd => TipoMoneda.FromCodigo(cd!));
            });

            builder.OwnsOne(a => a.PrecioTotal, precioBulder =>

            {
                precioBulder.Property(moneda => moneda.TipoMoneda)
                .HasConversion(tipoM => tipoM.Codigo, cd => TipoMoneda.FromCodigo(cd!));
            });

            builder.OwnsOne(a => a.Duracion);

            //realcion de uno a muchos
            builder.HasOne<Vehiculo>()
                .WithMany()
                .HasForeignKey(a => a.VehiculoId);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(a => a.UserId);



        }
    }
}
