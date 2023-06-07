using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.Entidades;

namespace Claver.Dominio.Contratos;

public interface IContratoCurso  : IContratoGeral<Curso>
{
    Task<IEnumerable<Curso>>Listar(string nome);
}
