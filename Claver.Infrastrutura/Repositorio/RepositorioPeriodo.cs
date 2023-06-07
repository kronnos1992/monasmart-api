using Claver.Dominio.Contratos;
using Claver.Dominio.Entidades;
using Claver.Infrastrutura.Context;
using Claver.Infrastrutura.Repositorio.Geral;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Claver.Infrastrutura.Repositorio
{
    public class RepositorioPeriodo : RepositorioGeral<Periodo>, IContratoPeriodo
    {
        private readonly ClaverDbContext _context;

        public RepositorioPeriodo(ClaverDbContext context, ILogger logger) : base(context, logger)
        {
            _context = context;
        }

        public async Task<IEnumerable<Periodo>> BuscarPeriodo(string periodo)
        {
            var periodos = await _context.Tb_Periodo
                .Where(b => b.TipoPeriodo.Contains(periodo))
                .ToListAsync();
            return periodos;
        }

        public async Task<Periodo> BuscarPorId(Guid id)
        {
            var periodo = await _context.Tb_Periodo 
                .Include(p => p.Cursos) 
                .FirstOrDefaultAsync(b => b.Id == id);
            return periodo;
        }

        public async Task<IEnumerable<Periodo>> BuscarTodos()
        {
            var periodos =  await _context.Tb_Periodo
                .Include(p => p.Cursos)
                .OrderByDescending(p => p.DataRegistro)
                .ToListAsync();
            return periodos;
        }
    }
}