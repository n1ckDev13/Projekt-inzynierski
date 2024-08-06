namespace TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync();
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(int id);
        Task DeleteAsync();
    }
}
