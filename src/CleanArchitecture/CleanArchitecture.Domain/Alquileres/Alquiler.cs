using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres.Events;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Alquileres
{
    public sealed class Alquiler : Entity<AlquilerId>
    {
        private Alquiler()
        {

        }

        private Alquiler(
            AlquilerId id,
            VehiculoId vheiculoId,
            UserId userId,
            DateRange duracion,
            Moneda precioPorPeriodo,
            Moneda manenimiento,
            Moneda accesorios,
            Moneda precioTotal,
            AlquilerStatus status,
            DateTime fechaCreacion
            ) 
            : base(id)
        {
            VehiculoId = vheiculoId;
            UserId = userId;
            Duracion = duracion;
            PrecioPorPeriodo = precioPorPeriodo;
            Mantenimiento = manenimiento;
            Accesorios =accesorios;
            PrecioTotal = precioTotal;
            Status = status;
            FechaCreacion = fechaCreacion;
        }

        public VehiculoId? VehiculoId { get;private set; }
        public UserId? UserId { get; private set; }
        public Moneda? PrecioPorPeriodo { get;private set; }
        public Moneda? Mantenimiento { get;private set; }
        public Moneda? Accesorios { get;private set; }
        public Moneda? PrecioTotal { get;private set; }
        public AlquilerStatus Status { get; private set; }
        public DateRange? Duracion { get; private set; }

        public DateTime? FechaCreacion { get;private set; }
        public DateTime? FechaConfiramcion { get; private set; }
        public DateTime? FechaNegacion { get; private set; }
        public DateTime? FechaCompletado { get; private set; }
        public DateTime? FechaCnacelacion { get;private set; }
    
    
    public static Alquiler Reservar(
        Vehiculo vehiculo,
        UserId userId,
        DateRange duracion,
        DateTime fechaCreacion,
        PrecioService precioService
        )
        {


            var precioDetalle = precioService.CalcularPrecio(vehiculo,duracion);

            var alquiler = new Alquiler(
                AlquilerId.New(),
                vehiculo.Id!,
                userId,
                duracion,
                precioDetalle.PrecioPorPeriodo,
                precioDetalle.Mantenimiento,
                precioDetalle.Accesorios,
                precioDetalle.PrecioTotal,
                AlquilerStatus.Reservado,
                fechaCreacion);

            alquiler.RaiseDomainEvent(new AlquilerReservadoDomainEvent(alquiler.Id!));

            vehiculo.FechaUltimaAlquiler = fechaCreacion;

            return alquiler;
        }

        public Result Confiramar(DateTime utcNow)
        {
            if(Status != AlquilerStatus.Reservado)
            {
                // se dispara un mensaje de error como exception
                return Result.Failure(AlquilerError.NotReserved);
            }

            Status = AlquilerStatus.Confirmado;
            FechaConfiramcion = utcNow;

            RaiseDomainEvent(new AlquilerConfirmadoDomainEvent(Id!));

            return Result.Success();
        }

        public Result Rechazar(DateTime utcNow)
        {
            if(Status != AlquilerStatus.Reservado)
            {
                return Result.Failure(AlquilerError.NotReserved);
            }

            Status = AlquilerStatus.Rechazado;
            FechaNegacion = utcNow;

            RaiseDomainEvent(new AlquilerRechazadoDomainEvent(Id!));

            return Result.Success();

        }

        public Result Cancelado(DateTime utcNow)
        {
            if (Status != AlquilerStatus.Confirmado)
            {
                return Result.Failure(AlquilerError.NoConfirmado);
            }

            var currenday = DateOnly.FromDateTime(utcNow);


            if(currenday > Duracion!.Inicio)
            {
                return Result.Failure(AlquilerError.AlreadyStarted);
            }


            Status = AlquilerStatus.Cancelado;
            FechaCnacelacion = utcNow;


            RaiseDomainEvent(new AlquilerCanceladoDomainEvent(Id!));

            return Result.Success();

        }

        public Result Completar(DateTime utcNow)
        {
            if (Status != AlquilerStatus.Confirmado)
            {
                return Result.Failure(AlquilerError.NoConfirmado);
            }



            Status = AlquilerStatus.Completado;
            FechaNegacion = utcNow;

            RaiseDomainEvent(new AlquilerCompletadooDomainEvent(Id!));




            return Result.Success();

        }


    }
}
