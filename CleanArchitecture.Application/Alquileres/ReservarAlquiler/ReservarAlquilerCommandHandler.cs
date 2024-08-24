using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler
{
    internal sealed class ReservarAlquilerCommandHandler : ICommandHandler<ReservarAlquilerCommand, Guid>
    {

        private readonly IUserRepository _userRepository;
        private readonly IVehiculoRepository _vehiculoRepository;
        private readonly IAlquilerRepository _alquilerRepository;
        private readonly PrecioService _precioService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDatetimeProvider _datetimeProvider;

        public ReservarAlquilerCommandHandler(
            IUserRepository userRepository,
            IVehiculoRepository vehiculoRepository, 
            IAlquilerRepository alquilerRepository, 
            PrecioService recioService, IUnitOfWork unitOfWork,
            IDatetimeProvider datetimeProvider)
        {
            _userRepository = userRepository;
            _vehiculoRepository = vehiculoRepository;
            _alquilerRepository = alquilerRepository;
            _precioService = recioService;
            _unitOfWork = unitOfWork;
            _datetimeProvider = datetimeProvider;
        }

        public async Task<Result<Guid>> Handle(ReservarAlquilerCommand request, CancellationToken cancellationToken)
        {
            var userId = new UserId(request.UserId);

            var user = await _userRepository.GetByIdAsync(userId,cancellationToken);

            if(user is null)
            {
                return Result.Failure<Guid>(UserErrors.NotFound);
            }

            var vehiculoId = new VehiculoId(request.VehiculoId);

            var vehiculo = await _vehiculoRepository.GetByIdAsync(vehiculoId, cancellationToken);

            if(vehiculo is null)
            {
                return Result.Failure<Guid>(VehiculoErrors.NotFound);
            }

            var duracionRenta = DateRange.Create(request.FechaInicio, request.FechaFin);

            if(await _alquilerRepository.IsOverLappingAsync(vehiculo,duracionRenta,cancellationToken))
            {
                return Result.Failure<Guid>(AlquilerError.Overlap);
            }

            try
            {
                //aqui susede el erro de concurrency 
                //ejmplo cuando hay un conflico al momento de reservar un vehiculo al mismo tiempo que tora persona
                //concurrencia de un mismo record

                var alquiler = Alquiler.Reservar(
                    vehiculo,
                    user.Id!,
                    duracionRenta,
                    _datetimeProvider.currentTime,
                    _precioService);

                _alquilerRepository.Add(alquiler);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return alquiler.Id!.Value;

            }
            catch (ConcurracyException)
            {

                return Result.Failure<Guid>(AlquilerError.Overlap);
            }


        }
    }
}
