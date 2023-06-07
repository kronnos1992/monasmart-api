using Claver.Dominio.Contratos;
using Claver.Dominio.Entidades;
using Claver.Infrastrutura.Context;
using Claver.Infrastrutura.Repositorio.Geral;
using Microsoft.Extensions.Logging;

namespace Claver.Infrastrutura.Repositorio
{
    public class RepositorioMatricula: RepositorioGeral<Matricula>, IContratoMatricula
    {
        private readonly ClaverDbContext _context;

        public RepositorioMatricula(ClaverDbContext context, ILogger logger) : base(context, logger)
        {
            _context = context;
        }
        
    }
}