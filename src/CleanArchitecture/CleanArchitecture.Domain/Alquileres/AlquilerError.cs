using CleanArchitecture.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Alquileres
{
    public static class AlquilerError
    {
        public static Error NotFound = new Error(
            "Alquiler.Found",
            "El alquiler con el id especificado no fue encontrado");

        public static Error Overlap = new Error(
            "Alquiler.Overlap",
            "El alquiler esta siendo tomado por 2 o mas clientes al mismo timepo en la misma fecha");

        public static Error NotReserved = new Error(
            "Alquiler.notReserved",
            "El alquiler no esta reservado");


        public static Error NoConfirmado = new Error(
            "Alquiler.notConfirmed",
            "El alquiler no esta confirmado");

        public static Error AlreadyStarted = new Error(
            "Alquiler.AlreadyStarted",
            "El alquiler ya a comenzado");

    }
}
