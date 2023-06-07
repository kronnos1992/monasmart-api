using Claver.Dominio.Entidades.Base;

namespace Claver.Dominio.Entidades
{
    public class Matricula : ClasseBase
    {
        public int NumeroOrdem { get; set; }
        public Guid AlunoId { get; set; }
        public Guid TurmaId { get; set; }
        public Guid AnoLectivoId { get; set; }
        
        public Aluno Aluno { get; set; }
        public TurmaSala Turma { get; set; }
        public AnoLectivo AnoLectivo { get; set; }
    }
}