using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.Entidades;

namespace Claver.Dominio.Contratos;

public interface IContratoDisciplina: IContratoGeral<Disciplina>
{
    Task<IEnumerable<Disciplina>>BuscarDisciplina(string nome);
    Task<IEnumerable<Disciplina>> BuscarTodos();
    Task<Disciplina> BuscarPorId(Guid id);
}