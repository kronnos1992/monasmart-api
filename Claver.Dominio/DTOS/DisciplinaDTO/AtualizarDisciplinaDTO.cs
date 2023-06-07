using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Claver.Dominio.DTOS.DisciplinaDTO
{
    public class AtualizarDisciplinaDTO
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
    }
}