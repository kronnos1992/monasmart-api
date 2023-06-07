namespace Claver.Dominio.Contratos.Geral
{
    public interface IUnidadeTrabalho
    {
        IContratoInscricao Inscricao {get;}
        IContratoMatricula Matriculas { get; }
        IContratoTurma Turmas { get; }
        IContratoSala Salas { get; }
        IContratoDisciplina Disciplinas { get; }
        IContratoCurso Cursos { get; }
        IContratoClasse Classes { get; }
        IContratoPeriodo Periodos { get; }
        IContratoTurmaSala TurmaSala { get; }
        IContratoCursoDisciplina CursoDisciplinas { get; }
        IContratoAnoLectivo AnoLectivo { get; }
        Task<int> Finalizar();
        Task Dispose();
    }
}