using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Exceptions
{
    public sealed class ConcurracyException : Exception
    {

        public ConcurracyException(string massege, Exception innerException)
            :base(massege, innerException) 
        {
           // ocurre cuando existe un erro dentro de la base de datos
        }


    }
}
