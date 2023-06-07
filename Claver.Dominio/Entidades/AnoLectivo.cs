using Claver.Dominio.Entidades.Base;

namespace Claver.Dominio.Entidades;

public class AnoLectivo : ClasseBase
{
    public AnoLectivo()
    {
        Alunos = new HashSet<Matricula>();
        Inscritos = new HashSet<Inscricao>();
    }
    public string AnoAcademico { get; set; }
    public bool Ativo { get; set; }
    public ICollection<Matricula> Alunos { get; set; }
    public ICollection<Periodo> Periodos { get; set; }
    public ICollection<Inscricao> Inscritos { get; set; }
}