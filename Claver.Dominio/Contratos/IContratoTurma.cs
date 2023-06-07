using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.Entidades;

namespace Claver.Dominio.Contratos;

public interface IContratoTurma: IContratoGeral<Turma>
{
    Task<IEnumerable<Turma>>BuscarTurma(string nome);
    Task<IEnumerable<Turma>> BuscarTodos();
    Task<Turma> BuscarPorId(Guid id);
}