namespace Claver.Dominio.DTOS.CursoDTOs;

public class GetCursoDTO
{
    public Guid Id { get; set; }
    public string NomeCurso { get; set; }
    public string DataRegistro { get; set; }
    public string DataRevisao { get; set; }
    public string Sigla { get; set; }
}
