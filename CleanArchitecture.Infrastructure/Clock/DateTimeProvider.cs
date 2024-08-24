using CleanArchitecture.Application.Abstractions.Clock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Clock
{
    internal sealed class DateTimeProvider : IDatetimeProvider
    {
        public DateTime currentTime => DateTime.UtcNow;
    }
}
