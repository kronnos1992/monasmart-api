using Claver.Dominio.Contratos;
using Claver.Dominio.Entidades;
using Claver.Infrastrutura.Context;
using Claver.Infrastrutura.Repositorio.Geral;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Claver.Infrastrutura.Repositorio
{
    public class RepositorioCursoDisciplina : RepositorioGeral<CursoDisciplina>, IContratoCursoDisciplina
    {
        private readonly ClaverDbContext _context;

        public RepositorioCursoDisciplina(ClaverDbContext context, ILogger logger) : base(context, logger)
        {
            _context = context;
        }

        public async Task<CursoDisciplina> BuscarPorId(Guid id)
        {
            var periodo = await _context.Tb_CursoDisciplina  
                .FirstOrDefaultAsync(b => b.Id == id);
            return periodo;
        }

        public async Task<IEnumerable<CursoDisciplina>> BuscarTodos()
        {
            var periodos =  await _context.Tb_CursoDisciplina
                .OrderByDescending(p => p.DataRegistro)
                .ToListAsync();
            return periodos;
        }
    }
}