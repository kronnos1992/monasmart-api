using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Claver.Dominio.Entidades.Base;

namespace Claver.Dominio.Entidades
{
    public class Turma : ClasseBase
    {
        public Turma()
        {
            Salas = new HashSet<TurmaSala> ();
        }
        public string NomeTurma { get; set; }
        public int TotalTurma { get; set; }

        public Guid CursoId { get; set; }        
        public Guid PeriodoId { get; set; }

        public Curso Curso { get; set; }
        public Periodo Periodo { get; set; }

        public ICollection<TurmaSala> Salas { get; set; }        

    }
}