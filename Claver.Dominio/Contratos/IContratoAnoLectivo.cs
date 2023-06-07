using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.Entidades;

namespace Claver.Dominio.Contratos;

public interface IContratoAnoLectivo: IContratoGeral<AnoLectivo>
{
    Task<IEnumerable<AnoLectivo>>BuscarAnoLectivo(string nome);
    Task<IEnumerable<AnoLectivo>> BuscarTodos();
    Task<AnoLectivo> BuscarPorId(Guid id);
}