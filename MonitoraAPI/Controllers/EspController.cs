
using Microsoft.AspNetCore.Mvc;
using MonitoraAPI.Models.Requests;
using MonitoraAPI.Service.EspService;

namespace MonitoraAPI.Controllers;

[ApiController]
[Route("api/esp")]
public class EspController : Controller
{
    
    private readonly IEspService _espService;

    public EspController(IEspService espService)
    {
        _espService = espService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateEsp(EspRequest request)
    {
        var result = await _espService.ExecuteAsync(request);

        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }
        
        return CreatedAtAction(nameof(CreateEsp), new { id = result.Value.idESP }, result.Value);
    }
    
    [HttpGet("nao-finalizados")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPacientesNaoFinalizados()
    {
        try
        {
            var result = await _espService.GetEsps();

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Value);
        }
        catch (Exception ex)
        {
            return BadRequest("Ocorreu um erro ao obter a lista de pacientes." + ex.Message);
        }
    }

}