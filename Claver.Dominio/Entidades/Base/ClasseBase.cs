using System.ComponentModel.DataAnnotations;

namespace Claver.Dominio.Entidades.Base;

public abstract class ClasseBase
{
    [Key]
    public Guid Id { get; set; }
    public DateTime DataRegistro { get; set; }
    public DateTime? DataRevisao { get; set; }
      
}
