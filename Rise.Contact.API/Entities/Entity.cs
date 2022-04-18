using Rise.Contact.API.Interfaces;

namespace Rise.Contact.API.Entities
{
    public class Entity : IEntity
    {
        public Guid Id { get; private set; }

        public Entity()
        {
            this.Id = Guid.NewGuid();
        }

        public Entity(Guid id)
        {
            this.Id = id;
        }
    }
}
