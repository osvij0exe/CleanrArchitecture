using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Alquileres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Alquileres.GetAlquiler
{
    public sealed record GetAlquilerQuery(Guid AlquilerId) : IQuery<AlquilerResponse>;
}
