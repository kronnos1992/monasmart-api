using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Claver.Dominio.Entidades.Base;

namespace Claver.Dominio.Entidades;

public class Pessoa : ClasseBase
{
    [StringLength(200, MinimumLength =5, ErrorMessage ="Entre 5 e 200")]
    public string NomeCompleto { get; set; }
    
    [StringLength(200, MinimumLength =5, ErrorMessage ="Entre 5 e 200")]
    public string NomePai { get; set; }

    [StringLength(200, MinimumLength =5, ErrorMessage ="Entre 5 e 200")]
    public string NomeMae{ get; set; }
    public DateTime DataNascimento { get; set; }
    public int Idade {get; set;}
    [MaxLength(1)]
    public string Sexo { get; set; }
    public string NumeroBilhete { get; set; }  
    public string EstadoCivil { get; set; }
    // public byte[] Foto { get; set; }
    public string FotoDesc { get; set; }

    public Contacto Contacto { get; set; }
    public Endereco Endereco { get; set; }
      
}