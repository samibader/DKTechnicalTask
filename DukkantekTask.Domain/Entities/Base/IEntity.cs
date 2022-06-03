namespace DukkantekTask.Domain.Entities.Base
{
    /// <summary>
    /// Main entity interface, all entities must inherit this interface
    /// </summary>
    /// <typeparam name="TKey">Primary key type<</typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// Unique identifier for this entity.
        /// </summary>
        TKey Id { get; set; }
    }

    /// <summary>
    /// Default entity interface, used as a shortcut for creating IEntity with "int" as primary key
    /// </summary>
    public interface IEntity : IEntity<int>
    {

    }
}
