using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Claver.Dominio.DTOS.AnoLectivo
{
    public class AddAnoLectivoDTO
    {
        public string AnoAcademico { get; set; }
        public bool Ativo { get; set; }
    }
}