using Microsoft.EntityFrameworkCore;
using MonitoraAPI.Models;

namespace MonitoraAPI.Infra;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<EspEntity> EspEntities { get; set; }
    public DbSet<TemperaturaDados> TemperaturaDados { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EspEntity>(entity =>
        {
            entity.HasKey(e => e.idESP); // Define o idESP como chave primária
            entity.Property(e => e.idESP).IsRequired(); // Define idESP como obrigatório
            entity.HasMany(e => e.temperaturaDados) // Define o relacionamento com TemperaturaDados
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade); // Deletar TemperaturaDados ao remover EspEntity
        });

        // Configuração para TemperaturaDados
        modelBuilder.Entity<TemperaturaDados>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(t => t.temperatura).IsRequired(); // Define temperatura como obrigatório
            entity.Property(t => t.data).IsRequired(); // Define data como obrigatória
        });
    }
}