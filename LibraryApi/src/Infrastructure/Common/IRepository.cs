using Domain.Common;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public interface IRepository<Entity> where Entity : BaseEntity
    {
        /// <summary>
        /// Method adds new entity to DbSet
        /// </summary>
        /// <param name="entity">New entity object</param>
        /// <returns>A task that represents the asynchronous addition to DbSet operation</returns>
        Task AddAsync(Entity entity);

        /// <summary>
        /// Method removes entity from DbSet
        /// </summary>
        /// <param name="id">Id of entity that is being removed from DbSet</param>
        /// <returns>A task that represents the asynchronous removing entity from DbSet operation</returns>
        Task DeleteAsync(Entity entity);

        /// <summary>
        /// Method updates entity in DbSet
        /// </summary>
        /// <param name="entity">Entity that is being updated</param>
        /// <returns>A task that represents the asynchronous updating entity in DbSet operation</returns>
        Task UpdateAsync(Entity entity);

        /// <summary>
        /// Method to save changes in databse
        /// </summary>
        /// <returns>A task that represents the asynchronous save operation</returns>
        Task SaveChangesAsync();
    }
}