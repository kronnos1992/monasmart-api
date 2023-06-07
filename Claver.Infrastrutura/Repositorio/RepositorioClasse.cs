using Claver.Dominio.Contratos;
using Claver.Dominio.Entidades;
using Claver.Infrastrutura.Context;
using Claver.Infrastrutura.Repositorio.Geral;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Claver.Infrastrutura.Repositorio;

public class RepositorioClasse : RepositorioGeral<Classe>, IContratoClasse
{
    private ClaverDbContext _dbcontext;
    public RepositorioClasse(ClaverDbContext dbcontext, ILogger logger) : base(dbcontext, logger)
    {
        _dbcontext = dbcontext;
    }

    public async Task<IEnumerable<Classe>> BuscarPorNome(string nome)
    {
        var classes = await _dbcontext.Tb_Classe
            .Where(b => b.DescricaoClasse.Contains(nome))
            .ToListAsync();
        return classes;
    }
    public async Task<IEnumerable<Classe>> BuscarTodos()
    {
        var candidatos =  await _dbcontext.Tb_Classe
            .Include(p => p.Inscritos)
            .OrderByDescending(p => p.DataRegistro)
            .ToListAsync();
        return candidatos;
    }

    public async Task<Classe> BuscarPorId(Guid id)
    {
        var candidatos =  await _dbcontext.Tb_Classe
            .Include(p => p.Inscritos)
            .FirstOrDefaultAsync(p => p.Id == id);
        return candidatos;
    }
}
