using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Users.LoginUser
{
    internal sealed class LogingCommandHandler : ICommandHandler<LogingCommand, string>
    {
        private readonly IUserRepository _userRepository;

        public LogingCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<Result<string>> Handle(LogingCommand request, CancellationToken cancellationToken)
        {
            //logica de loging
            //1. Verificar que el usuario exista en la base de datos
            //buscar usuario por email en la db
            


            //2. Validar password 


            //.3 Generar el JWT


            //4. Return jwt



        }
    }
}
