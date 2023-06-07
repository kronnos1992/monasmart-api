using Claver.Aplicacao.Implemetancao;
using Claver.Dominio.DTOS.CursoDTOs;
using Claver.Dominio.Entidades;
using FluentValidation;

namespace Claver.Aplicacao.Validations
{

    public class AddCursoValidation: AbstractValidator<AddCursoDTO>
    {
        private readonly IEnumerable<Curso> _cursos;
        private ImplementarCurso _icurso;
        public AddCursoValidation(IEnumerable<Curso> cursos, ImplementarCurso icurso)
        {
            _cursos = cursos;
            _icurso = icurso;

            RuleFor(x => x.NomeCurso)
                .NotEmpty()
                .NotNull()
                .NotEqual("STRING")
                .NotEqual("string");
                  
            RuleFor(x => x.Sigla)
                .NotEmpty()
                .NotNull()
                .NotEqual("STRING")
                .NotEqual("string");
        }
    }

    public class AtualizarCursoValidation: AbstractValidator<AtualizarCursoDTO>
    {
        public AtualizarCursoValidation()
        {
            RuleFor(x => x.NomeCurso)
                .NotEmpty()
                .NotNull()
                .NotEqual("STRING")
                .NotEqual("string");
                
            RuleFor(x => x.Sigla)
                .NotEmpty()
                .NotNull()
                .NotEqual("STRING")
                .NotEqual("string");
        } 
    }
}