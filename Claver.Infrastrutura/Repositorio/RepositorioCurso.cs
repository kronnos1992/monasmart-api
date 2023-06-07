using Claver.Dominio.Contratos;
using Claver.Dominio.Entidades;
using Claver.Infrastrutura.Context;
using Claver.Infrastrutura.Repositorio.Geral;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Claver.Infrastrutura.Repositorio;

public class RepositorioCurso : RepositorioGeral<Curso>, IContratoCurso
{
    private ClaverDbContext _dbcontext;
    public RepositorioCurso(ClaverDbContext dbcontext, ILogger logger) : base(dbcontext, logger)
    {
        _dbcontext = dbcontext;
    }

    public async Task<IEnumerable<Curso>> Listar(string nome)
    {
        var cursos = await _dbcontext.Tb_Curso
            .Where(b => b.NomeCurso.Trim().ToUpper().Contains(nome.Trim().ToUpper()))
            .ToListAsync();
        return cursos;
    }
}
