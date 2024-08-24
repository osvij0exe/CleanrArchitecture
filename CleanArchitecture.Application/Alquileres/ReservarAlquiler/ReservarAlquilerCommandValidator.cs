using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler
{
    public class ReservarAlquilerCommandValidator :AbstractValidator<ReservarAlquilerCommand>
    {

        public ReservarAlquilerCommandValidator()
        {
            //RuleFor permite agregar una exprecion lamba ára validar una determinada propiedad
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.VehiculoId).NotEmpty();
            RuleFor(c => c.FechaInicio).LessThan(c => c.FechaFin);
        }
    }
}
