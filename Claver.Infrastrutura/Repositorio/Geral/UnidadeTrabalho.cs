using Claver.Dominio.Contratos;
using Claver.Dominio.Contratos.Geral;
using Claver.Infrastrutura.Context;
using Microsoft.Extensions.Logging;

namespace Claver.Infrastrutura.Repositorio.Geral
{
    public class UnidadeTrabalho : IUnidadeTrabalho
    {
        private readonly ClaverDbContext _context;
        private readonly ILogger _logger;

        
        #region Contratos
        public IContratoInscricao Inscricao {get;}
        public IContratoMatricula Matriculas {get;}
        public IContratoTurma Turmas {get;}
        public IContratoSala Salas {get;}
        public IContratoDisciplina Disciplinas {get;}
        public IContratoCurso Cursos {get;}
        public IContratoClasse Classes {get;}
        public IContratoPeriodo Periodos {get;}
        public IContratoTurmaSala TurmaSala {get;}
        public IContratoCursoDisciplina CursoDisciplinas {get;}
        public IContratoAnoLectivo AnoLectivo {get;}
        #endregion

        public UnidadeTrabalho(ClaverDbContext context, ILoggerFactory logger)
        {
            _context = context;
            _logger = logger.CreateLogger("./logs");

            Inscricao = new RepositorioInscricao(_context, _logger);
            Cursos = new RepositorioCurso(_context, _logger);
            Classes = new RepositorioClasse(_context, _logger);
            Matriculas = new RepositorioMatricula(_context, _logger);
            Salas = new RepositorioSala(_context, _logger);
            Periodos = new RepositorioPeriodo(_context, _logger);
            TurmaSala = new RepositorioTurmaSala(_context, _logger);
            Turmas = new RepositorioTurma(_context, _logger);
            Salas = new RepositorioSala(_context, _logger);
            Disciplinas = new RepositorioDisciplina(_context, _logger);
            CursoDisciplinas = new RepositorioCursoDisciplina(_context, _logger);
            AnoLectivo = new RepositorioAnoLectivo(_context, _logger);
        }

        public async Task<int> Finalizar()
        {
            return await _context.SaveChangesAsync();
        }

        public Task Dispose()
        {
            _context.Dispose();
            return Task.Delay(1000);            
        }
    }
}