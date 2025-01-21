using Microsoft.EntityFrameworkCore;
using MonitoraAPI.Models;

namespace MonitoraAPI.Infra.Repository.Esp;

public class EspRepository : IEspRepository
{
    private readonly ApplicationDbContext _context;

    public EspRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(EspEntity esp)
    {
        await _context.AddAsync(esp);
    }

    public async Task<EspEntity> GetById(string id)
    {
        return await _context.EspEntities.FindAsync(id);
    }

    public async Task<List<EspEntity>> GetAll()
    {
        return await _context.EspEntities
            .Include(e => e.temperaturaDados) // Inclui a lista relacionada
            .ToListAsync();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}