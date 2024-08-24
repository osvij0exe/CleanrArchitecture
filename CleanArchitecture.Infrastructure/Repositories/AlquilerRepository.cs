using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    internal sealed class AlquilerRepository : Repository<Alquiler,AlquilerId>, IAlquilerRepository
    {

        private static readonly AlquilerStatus[] ActiveAlquilerStatuses =
        {
            AlquilerStatus.Rechazado,
            AlquilerStatus.Rechazado,
            AlquilerStatus.Completado
        };

        public AlquilerRepository(ApplicationDbContext context) : base(context)
        {


        }

        //Metodo propio de alquiler
        public async Task<bool> IsOverLappingAsync(Vehiculo vehiculo, DateRange duracion, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Alquiler>()
                .AnyAsync(alquiler => alquiler.VehiculoId == vehiculo.Id 
                && alquiler.Duracion!.Inicio <= duracion.Fin
                && alquiler.Duracion.Fin >= duracion.Inicio
                && ActiveAlquilerStatuses.Contains(alquiler.Status),
                cancellationToken);
        }
    }
}
