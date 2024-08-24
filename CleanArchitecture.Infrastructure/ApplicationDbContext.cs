using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace CleanArchitecture.Infrastructure
{
    public sealed class ApplicationDbContext : DbContext,IUnitOfWork
    {

        private readonly IPublisher _publisher;

        public ApplicationDbContext(DbContextOptions options,
            IPublisher publisher)
            : base(options) 
        {

            _publisher = publisher;

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);


        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default!)
        {

            try
            {

                var result = await base.SaveChangesAsync(cancellationToken);

                // pblicar los eventos
                await publishDomainEventAsync();
            
            
            
                return result;

            }
            catch (DbUpdateConcurrencyException ex)
            {
                // para cuando ocurren errores dentro de la base de datos
                throw new ConcurracyException("La exception por concurrenca se disparo", ex);
            }
        
        
        
        }



        private async Task publishDomainEventAsync()
        {

            var domaiinEvents = ChangeTracker
                .Entries<IEntity>()
                .Select(entry => entry.Entity)
                .SelectMany(entitry =>
                {
                    var domainEvents = entitry.GetDomainEvents();
                    entitry.ClearDomainEvents();
                    return domainEvents;
                }).ToList();
            
            foreach(var domainEent in domaiinEvents)
            {
                await _publisher.Publish(domainEent);
            }

        }
        
    }


}
