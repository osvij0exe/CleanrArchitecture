﻿using CleanArchitecture.Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Abstractions.Messaging
{
    public interface IQueryHandler<TQuery,TResponse> : IRequestHandler<TQuery,Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
