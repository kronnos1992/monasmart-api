using Claver.Dominio.Contratos;
using Claver.Dominio.Entidades;
using Claver.Infrastrutura.Context;
using Claver.Infrastrutura.Repositorio.Geral;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Claver.Infrastrutura.Repositorio;

public class RepositorioAnoLectivo : RepositorioGeral<AnoLectivo>, IContratoAnoLectivo
{
    private ClaverDbContext _dbcontext;
    public RepositorioAnoLectivo(ClaverDbContext dbcontext, ILogger logger) : base(dbcontext, logger)
    {
        _dbcontext = dbcontext;
    }

    public async Task<AnoLectivo> BuscarPorId(Guid id)
    {
        var AnoLectivo = await _dbcontext.Tb_AnoLectivo 
            .Include(p => p.Alunos)  
            .FirstOrDefaultAsync(b => b.Id == id);
        return AnoLectivo;
    }

    public async Task<IEnumerable<AnoLectivo>> BuscarAnoLectivo(string AnoLectivo)
    {
        return await _dbcontext.Tb_AnoLectivo   
            .Where(p => p.AnoAcademico.Contains(AnoLectivo))
            .Include(p => p.Alunos)
            .ToListAsync();
    }

    public async Task<IEnumerable<AnoLectivo>> BuscarTodos()
    {
        return await _dbcontext.Tb_AnoLectivo  
            .Include(p => p.Alunos)
            .ToListAsync();
    }
}