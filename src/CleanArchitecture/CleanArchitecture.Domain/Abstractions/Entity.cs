using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Abstractions
{
    public abstract class Entity<TEntityId>: IEntity
    {

        protected Entity()
        {

        }

        private readonly List<IDomainEvent> _domainEvents = new();



        protected Entity(TEntityId id)
        {
            Id = id;
        }

        public TEntityId? Id { get; init; }


        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.ToList();
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        protected void RaiseDomainEvent(IDomainEvent domainEvents)
        {
            //agrega a la coleccion
            _domainEvents.Add(domainEvents);
        }



    }
}
