using Claver.Dominio.Contratos;
using Claver.Dominio.Entidades;
using Claver.Infrastrutura.Context;
using Claver.Infrastrutura.Repositorio.Geral;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Claver.Infrastrutura.Repositorio;

public class RepositorioTurma : RepositorioGeral<Turma>, IContratoTurma
{
    private ClaverDbContext _dbcontext;
    public RepositorioTurma(ClaverDbContext dbcontext, ILogger logger) : base(dbcontext, logger)
    {
        _dbcontext = dbcontext;
    }

    public async Task<Turma> BuscarPorId(Guid id)
    {
        var turma = await _dbcontext.Tb_Turma  
            .FirstOrDefaultAsync(b => b.Id == id);
        return turma;
    }

    public async Task<IEnumerable<Turma>> BuscarTurma(string nome)
    {
        var classes = await _dbcontext.Tb_Turma
            .Where(b => b.NomeTurma.Contains(nome))
            .ToListAsync();
        return classes;
    }

    public async Task<IEnumerable<Turma>> BuscarTodos()
    {
        var turmas =  await _dbcontext.Tb_Turma
            .OrderByDescending(p => p.DataRegistro)
            .ToListAsync();
        return turmas;
    }
}
