using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Claver.Dominio.DTOS.TurmaDTOs
{
    public class AtualizarTurmaDTO
    {
        public Guid Id { get; set; }
        public string NomeTurma { get; set; }
        public int TotalTurma { get; set; }
        public Guid CursoId { get; set; }
        public Guid PeriodoId { get; set; }
    }
}