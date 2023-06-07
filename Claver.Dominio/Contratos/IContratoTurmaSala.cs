using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.Entidades;

namespace Claver.Dominio.Contratos;

public interface IContratoTurmaSala: IContratoGeral<TurmaSala>
{
    Task<TurmaSala> BuscarPorId(Guid id);
    Task<IEnumerable<TurmaSala>>BuscarTodos();
}
