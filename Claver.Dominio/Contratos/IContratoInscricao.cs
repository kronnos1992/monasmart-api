using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.Entidades;

namespace Claver.Dominio.Contratos
{
    public interface IContratoInscricao : IContratoGeral<Inscricao>
    {
        Task<IEnumerable<Inscricao>>BuscarPorRupe(string roupe);
        Task<IEnumerable<Inscricao>>BuscarPorBilhete(string bilhete);
        Task<IEnumerable<Inscricao>>BuscarPorCurso(Guid curso);
        Task<IEnumerable<Inscricao>>BuscarAdmitidos(Guid curso, Guid classe);
        Task<IEnumerable<Inscricao>>BuscarPorClasse(Guid classe);
        Task<IEnumerable<Inscricao>>BuscarPorIntervalo(DateTime data1, DateTime data2);
        Task <Inscricao> Validar(Inscricao candidato);

    }
}