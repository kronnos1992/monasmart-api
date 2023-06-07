using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.Entidades;

namespace Claver.Dominio.Contratos;

public interface IContratoClasse  : IContratoGeral<Classe>
{
    Task<IEnumerable<Classe>>BuscarPorNome(string nome);
    Task<IEnumerable<Classe>> BuscarTodos();
    Task<Classe> BuscarPorId(Guid id);
}
