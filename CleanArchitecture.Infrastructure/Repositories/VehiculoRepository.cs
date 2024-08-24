using CleanArchitecture.Domain.Vehiculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    internal sealed class VehiculoRepository : Repository<Vehiculo,VehiculoId>, IVehiculoRepository
    {
        public VehiculoRepository(ApplicationDbContext context) 
            : base(context)
        {
            //aqui van los metodos propios de la logica  de vehiculo
        }
    }
}
