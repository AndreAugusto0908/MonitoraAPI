using MonitoraAPI.Models;
using MonitoraAPI.Models.Requests;

namespace MonitoraAPI.Service.EspService;

public interface IEspService
{
    Task<Result<EspEntity>> ExecuteAsync(EspRequest request);
    
    Task<Result<List<EspEntity>>> GetEsps();
}