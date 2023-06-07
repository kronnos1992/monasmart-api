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
    public class RepositorioSala: RepositorioGeral<Sala>, IContratoSala
    {
        private ClaverDbContext _dbcontext;
        public RepositorioSala(ClaverDbContext dbcontext, ILogger logger) : base(dbcontext, logger)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Sala> BuscarPorId(Guid id)
        {
            var sala = await _dbcontext.Tb_Sala 
                .Include(p => p.Turmas)  
                .FirstOrDefaultAsync(b => b.Id == id);
            return sala;
        }

        public async Task<IEnumerable<Sala>> BuscarSala(string sala)
        {
            return await _dbcontext.Tb_Sala   
                .Where(p => p.NumeroSala.Contains(sala))
                .Include(p => p.Turmas)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sala>> BuscarTodos()
        {
            return await _dbcontext.Tb_Sala  
                .Include(p => p.Turmas)
                .OrderByDescending(p => p.DataRegistro)
                .ToListAsync();
        }
    }
}