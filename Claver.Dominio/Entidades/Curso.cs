using Claver.Dominio.Entidades.Base;

namespace Claver.Dominio.Entidades;

public class Curso : ClasseBase
{
    public Curso()
    {
        Inscritos = new HashSet<Inscricao>();
        Disciplinas = new HashSet<CursoDisciplina>();
        Turmas = new HashSet<Turma>();
    }
    public string NomeCurso { get; set; }
    public string Sigla { get; set; }
    // public string DuracaoCurso { get; set; }
    public ICollection<Inscricao> Inscritos { get; set; } 
    public ICollection<CursoDisciplina> Disciplinas { get; set; }
    public ICollection<Turma> Turmas { get; set; }   
    
}
