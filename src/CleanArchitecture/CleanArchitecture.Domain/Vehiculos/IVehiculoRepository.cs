using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Vehiculos
{
    public interface IVehiculoRepository
    {
        // verificar nullabilidad
        Task<Vehiculo?> GetByIdAsync(VehiculoId id, CancellationToken cancellationToken = default!);

    }
}
