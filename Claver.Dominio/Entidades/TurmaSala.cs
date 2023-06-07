using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Claver.Dominio.Entidades.Base;

namespace Claver.Dominio.Entidades
{
    public class TurmaSala: ClasseBase
    {
        public Guid TurmaId { get; set; }
        public Guid SalaId { get; set; }

        public Turma Turma { get; set; }
        public Sala Sala { get; set; }
    }
}