﻿using CleanArchitecture.Api.Middleware;
using CleanArchitecture.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Api.Extencions
{
    public static class ApplicationBuilderExtensions
    {

        public static void ApplyMigration(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var service = scope.ServiceProvider;
                var loggerFactory = service.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = service.GetRequiredService<ApplicationDbContext>();
                    context.Database.MigrateAsync();

                }
                catch (Exception ex)
                {

                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "Error en migracion");
                }

            }
        }


        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }


    }
}