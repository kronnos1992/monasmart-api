using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Claver.Dominio.Entidades.Base;

namespace Claver.Dominio.Entidades
{
    public class Inscricao: ClasseBase
    {
        public string Rupe { get; set; }
        public bool Admitido { get; set; }
        public Guid PessoaId { get; set; }
        public Guid PeriodoId { get; set; }
        public Guid ClasseId { get; set; }
        public Guid CursoId { get; set; }
        public Pessoa Pessoa { get; set; }
        public Curso Curso { get; set; }
        public Classe Classe { get; set; }
        public AnoLectivo AnoLectivo { get; set; }
        public Periodo Periodo { get; set; }

        // New Properties
        public string NumCandidato { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderList { get; set; }
        public bool IsCompleto { get; set; } = false;
        public DateTime DataAdmissao { get; set; }
        public DateTime DataFinalizacao { get; set; }

    }
}