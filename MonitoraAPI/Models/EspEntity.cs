namespace MonitoraAPI.Models;

public class EspEntity
{
    public string idESP { get; set; }
    public List<TemperaturaDados> temperaturaDados { get; set; } = new List<TemperaturaDados>();
    
    public EspEntity (){}

    public EspEntity(string idESP, TemperaturaDados temperaturaDado)
    {
        this.idESP = idESP;
        this.temperaturaDados.Add(temperaturaDado);
    }
}