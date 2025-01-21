using MonitoraAPI.Infra.Repository.Esp;
using MonitoraAPI.Models;
using MonitoraAPI.Models.Requests;

namespace MonitoraAPI.Service.EspService;

public class EspService : IEspService
{
    private readonly IEspRepository _espRepository;

    public EspService(IEspRepository espRepository)
    {
        _espRepository = espRepository;
    }
    
    public async Task<Result<EspEntity>> ExecuteAsync(EspRequest request)
    {
        EspEntity espExists = await _espRepository.GetById(request.idEsp);
        
        TemperaturaDados temp = new TemperaturaDados(request.temperatura, request.data);
        
        if (espExists != null)
        {
            espExists.temperaturaDados.Add(temp);
            _espRepository.CommitAsync();
        }
        
        EspEntity newEsp = new EspEntity(request.idEsp, temp);
        
        _espRepository.AddAsync(newEsp);
        _espRepository.CommitAsync();
        return Result<EspEntity>.Success(newEsp);
    }

    public async Task<Result<List<EspEntity>>> GetEsps()
    {
        
        List<EspEntity> espList = await _espRepository.GetAll();
        return Result<List<EspEntity>>.Success(espList);
    }
}