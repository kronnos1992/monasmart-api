using Claver.Dominio.Entidades.Base;

namespace Claver.Dominio.Entidades;

public class Classe : ClasseBase
{
    public Classe()
    {
        Inscritos = new HashSet<Inscricao>();
    }
    public string DescricaoClasse { get; set; }
    public ICollection<Inscricao> Inscritos { get; set; }  
    
}
