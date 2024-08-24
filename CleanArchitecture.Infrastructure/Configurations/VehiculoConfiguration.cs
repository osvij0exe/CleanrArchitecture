using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public sealed class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
    {
        public void Configure(EntityTypeBuilder<Vehiculo> builder)
        {
            builder.ToTable("vehiculos");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasConversion(vehiculoId => vehiculoId!.Value, value => new VehiculoId(value));

            builder.OwnsOne(v => v.Direccion);

            builder.Property(v => v.Modelo)
                .HasMaxLength(200)
                .HasConversion(m => m!.Value,Objvalue => new Modelo(Objvalue));

            builder.Property(v => v.Vin)
                .HasMaxLength(500)
                .HasConversion(vin => vin!.value, ObjValue => new Vin(ObjValue));

            // dos valores tipo y moneda
            builder.OwnsOne(v => v.Precio, priceBuilder =>
            {
                priceBuilder.Property(m => m.TipoMoneda)
                .HasConversion(tMoneda => tMoneda.Codigo, cd => TipoMoneda.FromCodigo(cd!));
            });

            builder.OwnsOne(v => v.Mantenimiento, priceBuilder =>
            {
                priceBuilder.Property(moneda => moneda.TipoMoneda)
                .HasConversion(tMoneda => tMoneda.Codigo, cd => TipoMoneda.FromCodigo(cd!));
            });

            //revisar concurrencia potimista clase 47  clean architecture
            builder.Property<uint>("Version").IsRowVersion();

        }
    }
}
