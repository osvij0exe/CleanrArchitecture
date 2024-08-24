﻿using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Alquileres.Events;
using CleanArchitecture.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler
{
    internal sealed class ReservarAlquilerDomainEventHandler :
        INotificationHandler<AlquilerReservadoDomainEvent>
    {

        private readonly IAlquilerRepository _alquilerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public ReservarAlquilerDomainEventHandler(IAlquilerRepository alquilerRepository, IUserRepository userRepository, IEmailService emailService)
        {
            _alquilerRepository = alquilerRepository;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task Handle(AlquilerReservadoDomainEvent notification, CancellationToken cancellationToken)
        {
            var alquiler = await _alquilerRepository.GetByIdAsync(notification.AlquilerId, cancellationToken);

            if(alquiler is null)
            {
                return;
            }

            var user = await _userRepository.GetByIdAsync(alquiler.UserId!, cancellationToken);


            if(user is null) 
            { 
                return; 
            }


            await _emailService.SendAsync(user.Email!,
                "Alquiler Reservado",
                "Tienes que confirmar esta reserva de lo contrario se va a perder");

        }
    }

}
