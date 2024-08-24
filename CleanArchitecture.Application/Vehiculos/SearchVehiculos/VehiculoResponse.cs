﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos
{
    public sealed class VehiculoResponse
    {
        public Guid Id { get; init; }
        public string? Modelo { get; init; }
        public string? Vin { get; set; }
        public decimal Precio { get; init; }
        public string? TipoMoneda { get; init; }
        public DireccionResponse? Direccion { get; set; }



    }
}
