using Claver.Dominio.Entidades.Base;

namespace Claver.Dominio.Entidades
{
    public class Periodo: ClasseBase
    {
        public Periodo()
        {
            Cursos = new HashSet<CursoDisciplina>();
            Turmas = new HashSet<Turma>();
        }
        public string TipoPeriodo { get; set; }
        public string Descricao { get; set; }
        
        public ICollection<CursoDisciplina> Cursos { get; set; }
        public ICollection<Turma> Turmas { get; set; }
    }
}