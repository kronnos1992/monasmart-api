using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Claver.Dominio.DTOS.SalaDTOs
{
    public class AtualizarSalaDTO
    {
        public Guid Id { get; set; }
        public string NumeroSala { get; set; }
        public int Capacidade { get; set; }
    }
}