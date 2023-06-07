using Claver.Dominio.Entidades.Base;

namespace Claver.Dominio.Entidades;
public class Endereco : ClasseBase
{
    public string Provincia { get; set; } 
    public string Municipio { get; set; }
    public string Casa { get; set; } 
    public string Rua { get; set; }
    
}
