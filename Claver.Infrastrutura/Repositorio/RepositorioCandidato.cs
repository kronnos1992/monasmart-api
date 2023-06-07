using Claver.Dominio.Contratos;
using Claver.Dominio.Entidades;
using Claver.Infrastrutura.Context;
using Claver.Infrastrutura.Repositorio.Geral;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Claver.Infrastrutura.Repositorio
{
    public class RepositorioInscricao : RepositorioGeral<Inscricao>, IContratoInscricao
    {
        private readonly ClaverDbContext _context;

        public RepositorioInscricao(ClaverDbContext context, ILogger logger) : base(context, logger)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inscricao>> BuscarAdmitidos(Guid curso, Guid classe)
        {
            var Inscricao = await _context.Tb_Inscricao
                .Where(b => b.Admitido == true)
                .Where(p => p.CursoId == curso)
                .Where(p => p.ClasseId == classe)
                .Include(p => p.Pessoa)
                .ToListAsync();

            return Inscricao;
        }

        public async Task<IEnumerable<Inscricao>> BuscarPorBilhete(string bilhete)
        {
            var Inscricao = await _context.Tb_Inscricao
                .Where(p => p.Pessoa.NumeroBilhete == bilhete)
                .Include(p => p.Pessoa)
                .AsNoTracking()
                .ToListAsync();

            return Inscricao;
        }

        public async Task<IEnumerable<Inscricao>> BuscarPorClasse(Guid classe)
        {
            var Inscricao = await _context.Tb_Inscricao
                .Where(b => b.Classe.Id == classe)
                .Include(p => p.Pessoa)
                .AsNoTracking()
                .ToListAsync();

            return Inscricao;
        }

        public async Task<IEnumerable<Inscricao>> BuscarPorCurso(Guid curso)
        {
            var Inscricao = await _context.Tb_Inscricao
                .Where(b => b.Curso.Id == curso)
                .Include(p => p.Pessoa)
                .AsNoTracking()
                .ToListAsync();

            return Inscricao;
        }

        public async Task<IEnumerable<Inscricao>> BuscarPorRupe(string rupe)
        {
            var Inscricao = await _context.Tb_Inscricao
                .Where(b => b.Rupe.Contains(rupe))
                .Include(p => p.Pessoa)
                .ToListAsync();

            return Inscricao;
        }

        public new async Task<IEnumerable<Inscricao>> Listar()
        {
            var Inscricaos =  await _context.Tb_Inscricao
                .Include(p => p.Pessoa)
                .ThenInclude(p => p.Contacto)
                .Include(p => p.Pessoa)
                .ThenInclude(p => p.Endereco)
                .Include(c => c.Classe)
                .Include( c => c.Curso)
                .Include( p => p.Periodo)
                .Include( a => a.AnoLectivo)
                .ToListAsync();

            return Inscricaos;
        }

        public new async Task<Inscricao> Listar(Guid id)
        {
            var candidato = await _context
                .Tb_Inscricao
                .Include(p => p.Pessoa)
                .Include(p => p.Pessoa.Endereco)
                .Include(p => p.Pessoa.Contacto)
                .Include(p => p.Curso)
                .Include(p => p.AnoLectivo)
                .Include(p => p.Classe)
                .Include(p => p.Periodo)
                .FirstAsync(p => p.Id == id);
            return candidato;
        }
        public async Task<Inscricao> Validar(Inscricao _candidato)
        {
            //dbSet.RemoveRange(entidade);
            var candidato = _context.Tb_Inscricao.FindAsync( _candidato.Id);
            _context.UpdateRange(candidato);
            return await candidato;

            // _context.Tb_Inscricao.UpdateRange(InscricaoDTO);
            // return InscricaoDTO;
        }

        // public async Task<Inscricao> AtualizarInscricao(Inscricao inscricao)
        // {
        //     //dbSet.RemoveRange(entidade);
        //     _context.Tb_Inscricao.UpdateRange(inscricao);
        //     return inscricao;
        // }
        // public async Task<Inscricao> BuscarPorId(Guid id)
        // {
        //     var Inscricaos =  await _context.Tb_Inscricao
        //         .Include(p =>p.Curso)
        //         .Include(p => p.Classe)
        //         .FirstOrDefaultAsync(p => p.Id == id);
        //     return Inscricaos;
        // }
        public Inscricao Concluir(Inscricao candidato)
        {
            _context.Update(candidato).Property(propertyName => propertyName.OrderList).IsModified = false;
            return candidato;
        }

        public async Task<IEnumerable<Inscricao>> BuscarPorIntervalo(DateTime data1, DateTime data2)
        {
            var candidatos = await _context.Tb_Inscricao
                .Where(b => b.DataRegistro >= data1 && b.DataRegistro < data2).ToListAsync();
            
            return candidatos;
        }
    }
}