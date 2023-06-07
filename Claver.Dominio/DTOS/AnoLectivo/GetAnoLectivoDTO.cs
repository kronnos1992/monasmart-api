using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Claver.Dominio.DTOS.AnoLectivo
{
    public class GetAnoLectivoDTO
    {
        public Guid Id { get; set; }
        public string DataRegistro { get; set; }
        public string DataRevisao { get; set; }
        public string AnoAcademico { get; set; }
        public bool Ativo { get; set; }
    }
}