using CleanArchitecture.Application.Abstractions.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Email
{
    internal sealed class EmailServices : IEmailService
    {
        public Task SendAsync(Domain.Users.Email recipient, string subject, string body)
        {
            //se implementa la logica de un provedor de email como SendGrid o Gmail verificar
            return Task.CompletedTask;
        }
    }
}
