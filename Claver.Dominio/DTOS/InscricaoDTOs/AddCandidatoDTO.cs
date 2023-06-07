using Microsoft.AspNetCore.Http;
using System.ComponentModel;

using System.ComponentModel.DataAnnotations.Schema;

namespace Claver.Dominio.DTOS.InscricaoDTOs;

public class AddCandidatoDTO
{
    public string NomeCompleto { get; set; }
    public string NomePai { get; set; }
    public string NomeMae{ get; set; }
    public DateTime DataNascimento { get; set; }
    public string NumeroBilhete{ get; set; }
    public string Casa { get; set; }   
    public IFormFile Foto { get; set; }
    public string Sexo { get; set; }
    public string EstadoCivil { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }  
    public string Provincia { get; set; }  
    public string Municipio { get; set; }    
    public string Rua { get; set; }
    public string Rupe { get; set; }  
    
    // chaves estrangeiras
    public Guid ClasseId { get; set; }
    public Guid CursoId { get; set; }
    public Guid AnoLEctivoId { get; set; }
    public Guid PeriodoId { get; set; }
}
