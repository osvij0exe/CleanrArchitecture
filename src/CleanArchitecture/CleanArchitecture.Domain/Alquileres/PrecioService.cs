using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Alquileres
{
    //Domain service Logica
    public class PrecioService
    {
        public PrecioDetalle CalcularPrecio(Vehiculos.Vehiculo vehiculo, DateRange periodo)
        {
            var tipoMoneda = vehiculo.Precio!.TipoMoneda;

            var monto = periodo.CantidadDias * vehiculo.Precio.Monto;

            var precioPorPeriodo = new Moneda( monto, tipoMoneda);

            decimal porcentageCharge = 0;


            foreach(var accesesorio in vehiculo.Accesorios)
            {

                porcentageCharge += accesesorio switch
                {
                    Accesorio.AppleCar or Accesorio.AndroidCar => 0.05m,
                    Accesorio.AireAcondicionado => 0.01m,
                    Accesorio.Mapas => 0.01m,
                    _ => 0 //descarte
                };
            }

            var accesorioCharges = Moneda.Zero(tipoMoneda);


            if(porcentageCharge > 0)
            {
                var montoPorcentajeAccesorios = precioPorPeriodo.Monto * porcentageCharge;

                accesorioCharges = new Moneda( montoPorcentajeAccesorios, tipoMoneda);
            }

            var precioTotal = Moneda.Zero();

            precioTotal += precioPorPeriodo;


            if(!vehiculo.Mantenimiento!.IsZero())
            {
                precioTotal += vehiculo.Mantenimiento;
            }
            precioTotal += accesorioCharges;


            return new PrecioDetalle
                (
                    precioPorPeriodo,
                    vehiculo.Mantenimiento, 
                    accesorioCharges,
                    precioTotal
                );


        }

    }
}
