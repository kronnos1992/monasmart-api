using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.Entidades;

namespace Claver.Dominio.Contratos
{
    public interface IContratoPeriodo : IContratoGeral<Periodo>
    {
        Task<Periodo> BuscarPorId(Guid id);
        Task<IEnumerable<Periodo>> BuscarPeriodo(string periodo);
        Task<IEnumerable<Periodo>> BuscarTodos();
    }
}