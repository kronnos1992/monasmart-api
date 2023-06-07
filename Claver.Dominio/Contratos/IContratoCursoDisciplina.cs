using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.Entidades;

namespace Claver.Dominio.Contratos
{
    public interface IContratoCursoDisciplina : IContratoGeral<CursoDisciplina>
    {
        Task<CursoDisciplina> BuscarPorId(Guid id);
        Task<IEnumerable<CursoDisciplina>> BuscarTodos();
    }
}