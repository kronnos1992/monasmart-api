using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.Entidades;

namespace Claver.Dominio.Contratos;

public interface IContratoSala: IContratoGeral<Sala>
{
    Task<IEnumerable<Sala>>BuscarSala(string nome);
    Task<IEnumerable<Sala>> BuscarTodos();
    Task<Sala> BuscarPorId(Guid id);
}