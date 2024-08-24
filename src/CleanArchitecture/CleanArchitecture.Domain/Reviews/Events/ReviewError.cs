using CleanArchitecture.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Reviews.Events
{
    public static class ReviewError
    {
        public static readonly Error NotEligible = new(
            "Review.NotEligible",
            "Este review y calificacion no es elegible por que aun no se completa");


    }
}
