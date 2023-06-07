using Claver.Dominio.Entidades.Base;

namespace Claver.Dominio.Entidades
{
    public class CursoDisciplina: ClasseBase
    {
        public int CargaHoraria { get; set; }
        public string Duracao { get; set; }

        public Guid CursoId { get; set; }
        public Guid ClasseId { get; set; }
        public Guid DisciplinaId { get; set; }
        public Guid PeriodoId { get; set; }
        
        public Classe Classe { get; set; }
        public Curso Curso { get; set; }
        public Disciplina Disciplina { get; set; }
        public Periodo Periodo { get; set; }
    }
}