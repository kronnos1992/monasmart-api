using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Claver.Dominio.DTOS.TurmaSalaDTO
{
    public class AddTurmaSalaDTO
    {
        public Guid TurmaId { get; set; }
        public Guid SalaId { get; set; }
    }
}