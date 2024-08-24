using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using Dapper;


namespace CleanArchitecture.Application.Alquileres.GetAlquiler
{
    internal sealed class GetAlquilerQueryHandler : IQueryHandler<GetAlquilerQuery, AlquilerResponse>
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public GetAlquilerQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }



        public async Task<Result<AlquilerResponse>> Handle(GetAlquilerQuery request, CancellationToken cancellationToken)
        {

            using var connection = _connectionFactory.CreateConnecton();

            var sql = """
                SELECT
                    id AS Id,
                    vehiculo_id AS VehiculoId,
                    user_id AS UserId,
                    status AS Status,
                    precio_por_periodo AS PrecioAlquiler,
                    precio_por_periodo AS TipoMonedaAlquiler,
                    precio_mantenimiento AS PrecioMantenimiento,
                    precio_mantenimiento AS TipoMonedaMantenimineto,
                    precio_accesorios AS AccesosrioPrecio,
                    precio_accesorios_tipo_moneda AS TipoMonedaAccesosrio,
                    precio_total AS PrecioTotal,
                    precio_total_tipo_moneda AS PrecioTotalTipoMoneda,
                    duracion_inicio AS DuracionInicio,
                    duracion_final AS DuracionFinal,
                    fecha_creacion AS FechaCreacion
                FROM alquileres WHERE id=@AlquilerId
                """;


            var alquiler = await connection.QueryFirstOrDefaultAsync<AlquilerResponse>(
                sql,
                new
                {
                    request.AlquilerId
                });


            return alquiler!;


        }
    }
}
