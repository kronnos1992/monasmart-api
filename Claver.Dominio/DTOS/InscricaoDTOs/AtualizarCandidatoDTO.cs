namespace Claver.Dominio.DTOS.InscricaoDTOs;
using Microsoft.AspNetCore.Http;

public class AtualizarCandidatoDTO
{
    public string NomeCompleto { get; set; }
    public string NomePai { get; set; }
    public string NomeMae{ get; set; }
    public int AnoNascimento { get; set; }
    public string Sexo { get; set; }

    public string EstadoCivil { get; set; }
    public string NumCandidato { get; set; }

    public string TFoto { get; set; }
    public string TCertificado { get; set; }
    public string TBilhete { get; set; }

    public IFormFile Foto { get; set; }
    public IFormFile Certificado { get; set; }
    public IFormFile Bilhete { get; set; }

    public string Email { get; set; }
    public string Telefone { get; set; }

    
    public string Provincia { get; set; }  
    public string Municipio { get; set; }    
    public string Rua { get; set; }   
}