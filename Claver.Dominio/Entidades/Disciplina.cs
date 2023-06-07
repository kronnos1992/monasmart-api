using Claver.Dominio.Entidades.Base;

namespace Claver.Dominio.Entidades
{
    public class Disciplina : ClasseBase
    {
        public Disciplina()
        {
            Cursos = new HashSet<CursoDisciplina>();
        }
        public string Descricao { get; set; }
        public ICollection<CursoDisciplina> Cursos { get; set; }
    }
}