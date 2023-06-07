using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Claver.Dominio.DTOS.PeriodoDTOs
{
    public class UpdatePeriodoDTO
    {
        public Guid Id { get; set; }
        public string TipoPeriodo { get; set; }
        public string Descricao { get; set; }
    }
}