using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Claver.Dominio.Contratos;
using Claver.Dominio.Entidades;
using Claver.Infrastrutura.Context;
using Claver.Infrastrutura.Repositorio.Geral;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Claver.Infrastrutura.Repositorio
{
    public class RepositorioDisciplina: RepositorioGeral<Disciplina>, IContratoDisciplina
    {
        private ClaverDbContext _dbcontext;
        public RepositorioDisciplina(ClaverDbContext dbcontext, ILogger logger) : base(dbcontext, logger)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<Disciplina>> BuscarDisciplina(string nome)
        {
            var Disciplinas = await _dbcontext.Tb_Disciplina
                .Where(b => b.Descricao.Contains(nome))
                .ToListAsync();
            return Disciplinas;
        }
        public async Task<IEnumerable<Disciplina>> BuscarTodos()
        {
            var candidatos =  await _dbcontext.Tb_Disciplina
                .Include(p => p.Cursos)
                .OrderByDescending(p => p.DataRegistro)
                .ToListAsync();
            return candidatos;
        }

        public async Task<Disciplina> BuscarPorId(Guid id)
        {
            var candidatos =  await _dbcontext.Tb_Disciplina
                .Include(p => p.Cursos)
                .FirstOrDefaultAsync(p => p.Id == id);
            return candidatos;
        }
    }
}
