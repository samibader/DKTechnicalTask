namespace DukkantekTask.Domain.Entities.Base
{
    /// <summary>
    /// Implementation of IEntity interface, specifying other type for primary key
    /// </summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        /// <summary>
        /// Entity primary key
        /// </summary>
        public virtual TKey Id { get; set; }

        /// <summary>
        /// Override method to return custom string representation of the entity
        /// </summary>
        /// <returns>Custom string representing the entity type and id</returns>
        public override string ToString()
        {
            return $"[{GetType().Name} {Id}]";
        }
    }

    /// <summary>
    /// Default entity, used as a shortcut for creating Entity with "int" as primary key
    /// </summary>
    public abstract class Entity : Entity<int>, IEntity
    {

    }
}
