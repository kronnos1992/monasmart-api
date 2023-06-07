using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Claver.Dominio.DTOS.CursoDTOs
{
    public class AtualizarCursoDTO
    {
        public Guid Id { get; set; }
        public string NomeCurso { get; set; }
        public string Sigla { get; set; }
    }
}