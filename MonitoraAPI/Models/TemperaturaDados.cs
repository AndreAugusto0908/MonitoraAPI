namespace MonitoraAPI.Models;

public class TemperaturaDados
{
    public Guid Id { get; set; }
    public double temperatura { get; set; } 
    public string data { get; set; }

    public TemperaturaDados()
    {
        
    }

    public TemperaturaDados(double temperatura, string data)
    {
        this.Id = Guid.NewGuid();
        this.temperatura = temperatura;
        this.data = data;
    }
}