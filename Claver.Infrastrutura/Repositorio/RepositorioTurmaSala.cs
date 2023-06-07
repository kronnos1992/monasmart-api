using Claver.Dominio.Contratos;
using Claver.Dominio.Entidades;
using Claver.Infrastrutura.Context;
using Claver.Infrastrutura.Repositorio.Geral;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Claver.Infrastrutura.Repositorio
{
    public class RepositorioTurmaSala: RepositorioGeral<TurmaSala>, IContratoTurmaSala
    {
        private readonly ClaverDbContext _context;

        public RepositorioTurmaSala(ClaverDbContext context, ILogger logger) : base(context, logger)
        {
            _context = context;
        }

        public async Task<TurmaSala> BuscarPorId(Guid id)
        {
            var turma = await _context.Tb_TurmaSala 
                .Include(p => p.Sala)
                .Include(p => p.Turma) 
                .FirstOrDefaultAsync(b => b.Id == id);
            return turma;
        }
        public async Task<IEnumerable<TurmaSala>> BuscarTodos()
        {
            var turma = await _context.Tb_TurmaSala 
                .Include(p => p.Sala)
                .OrderByDescending(p => p.DataRegistro) 
                .Include(p => p.Turma) 
                .ToListAsync();
            return turma;
        }
    }
}