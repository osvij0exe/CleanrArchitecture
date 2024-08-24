using CleanArchitecture.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Reviews
{
    public sealed record Rating
    {

        public static readonly Error Invalid = new(
            "Rating.Invalid",
            "El rating es invalido"); 

        public int Value {  get; init; }

        //formas de generar el constructor
        //private Rating(int value)
        //{
        //    Value = value;
        //}
        private Rating(int value) => Value = value;
        
        public static Result<Rating> Create(int value)
        {
            if(value < 1 || value > 5)
            {
                return Result.Failure<Rating>(Invalid);
            }

            return new Rating(value);

        }

    }
}
