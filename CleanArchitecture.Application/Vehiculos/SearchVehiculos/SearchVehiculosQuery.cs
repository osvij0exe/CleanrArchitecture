using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Vehiculos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos
{
    public sealed record SearchVehiculosQuery(DateOnly fechaInicio,DateOnly fecaFin) 
        : IQuery<IReadOnlyList<VehiculoResponse>>;
}
