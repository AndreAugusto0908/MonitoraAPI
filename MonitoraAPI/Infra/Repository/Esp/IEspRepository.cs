using MonitoraAPI.Models;

namespace MonitoraAPI.Infra.Repository.Esp;

public interface IEspRepository
{
    Task AddAsync(EspEntity esp);

    Task<EspEntity> GetById(string id);

    Task<List<EspEntity>> GetAll();

    Task CommitAsync();
}