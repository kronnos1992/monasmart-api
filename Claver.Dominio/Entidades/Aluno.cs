using Claver.Dominio.Entidades.Base;

namespace Claver.Dominio.Entidades;

public class Aluno : ClasseBase
{
    public string NumeroCartao { get; set; }
    public string NumeroProcesso { get; set; }
    public Guid InscricaoId { get; set; }
    public Inscricao Inscricao { get; set; }
}