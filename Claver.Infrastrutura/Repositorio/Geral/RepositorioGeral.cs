using Claver.Dominio.Contratos.Geral;
using Claver.Infrastrutura.Context;
using Microsoft.EntityFrameworkCore;

namespace Claver.Infrastrutura.Repositorio.Geral
{
    public class RepositorioGeral<T> : IContratoGeral<T> where T : class
    {
        private static Dictionary<Guid, T> entitidades = new Dictionary<Guid, T>();
        private readonly ClaverDbContext _dbcontext;
        protected DbSet<T> dbSet;

        public RepositorioGeral(ClaverDbContext dbcontext, Microsoft.Extensions.Logging.ILogger logger)
        {
            _dbcontext = dbcontext;
            dbSet = _dbcontext.Set<T>();
        }
        
        public async Task<T> Atualizar(T entidade)
        {
            //dbSet.RemoveRange(entidade);
            await Task.Delay(1000);
            dbSet.UpdateRange(entidade);
            return entidade;
        }

        public async Task Eliminar(Guid id)
        {
            var valor = await dbSet.FindAsync(id);
            dbSet.RemoveRange(valor);
            return;
        }

        public async Task<T> Inserir(T entidade)
        {
            await dbSet.AddRangeAsync(entidade);
            return entidade;
        }

        public async Task<IEnumerable<T>> Listar()
        {
            var listOfT = dbSet.ToListAsync();
            return await listOfT;
        }

        public async Task<T> Listar(Guid id)
        {
            var oneT = dbSet.FindAsync(id);
            return await oneT;
        }
    }
}