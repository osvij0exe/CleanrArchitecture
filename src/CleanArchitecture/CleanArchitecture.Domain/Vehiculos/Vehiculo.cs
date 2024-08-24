using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Vehiculos
{
    public sealed class Vehiculo : Entity<VehiculoId>
    {

        private Vehiculo() 
        {
        }
        public Vehiculo(
            VehiculoId id,
            Modelo modelo,
            Vin vin,
            Moneda precio,
            Moneda mantenimiento,
            DateTime fechaultimaAlquiler,
            List<Accesorio> accesorios,
            Direccion direccion) : base(id)
        {

            Modelo = modelo;
            Vin = vin;
            Precio = precio;
            Mantenimiento = mantenimiento;
            FechaUltimaAlquiler = fechaultimaAlquiler;
            Accesorios = accesorios;
            Direccion = direccion;



        }

        public Modelo? Modelo { get; private set; }
        public Vin? Vin { get; private set; }
        public Direccion? Direccion { get; private set; }
        public Moneda? Precio { get; private set; }
        public Moneda? Mantenimiento { get;private set; }
        public DateTime? FechaUltimaAlquiler { get; internal set; }
        public List<Accesorio> Accesorios { get; private set; } = new();
    }
}
