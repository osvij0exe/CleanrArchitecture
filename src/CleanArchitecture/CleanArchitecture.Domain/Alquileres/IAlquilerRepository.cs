﻿using CleanArchitecture.Domain.Vehiculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Alquileres
{
    public interface IAlquilerRepository
    {

        Task<Alquiler?> GetByIdAsync(AlquilerId id, CancellationToken cancellationToken = default!);

        Task<bool> IsOverLappingAsync(Vehiculo vehiculo, DateRange duracion, CancellationToken cancellationToken = default!);

        void Add(Alquiler alquiler);


    }
}
