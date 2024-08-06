namespace TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
    }
}
