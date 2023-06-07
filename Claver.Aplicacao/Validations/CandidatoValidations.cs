using Claver.Dominio.DTOS.InscricaoDTOs;
using Claver.Dominio.Entidades;
using FluentValidation;

namespace Claver.Aplicacao.Validations
{
    public class CandidatoValidations: AbstractValidator<AddCandidatoDTO>
    {
        private readonly IEnumerable<Inscricao> _candidatos;
        private string _regex = @"^[0-9]{9}[a-zA-Z]{2}[0-9]{3}$";
        public CandidatoValidations(IEnumerable<Inscricao> candidatos)
        {
            _candidatos = candidatos;

            RuleFor(x => x.NomeCompleto)
                .NotEmpty()
                .MinimumLength(10)
                .WithMessage("Minimo de 10 Carateres")
                .MaximumLength(500)
                .WithMessage("MAximo 500 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.NomeMae)
                .MinimumLength(10)
                .WithMessage("Minimo de 10 Carateres")
                .MaximumLength(500)
                .WithMessage("MAximo 500 caracteres");

            RuleFor(x => x.NomePai)
                .MinimumLength(10)
                .WithMessage("Minimo de 10 Carateres")
                .MaximumLength(500)
                .WithMessage("MAximo 500 caracteres");

            RuleFor(x => x.Municipio)
                .MinimumLength(5)
                .WithMessage("Minimo de 5 Carateres")
                .MaximumLength(50)
                .WithMessage("MAximo 50 caracteres");

            RuleFor(x => x.Provincia)
                .MinimumLength(5)
                .WithMessage("Minimo de 5 Carateres")
                .MaximumLength(50)
                .WithMessage("MAximo 50 caracteres");    

            RuleFor(x => x.NumeroBilhete)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo obrigatorio")
                .Length(14,14)
                .Matches(_regex)
                .WithMessage("Erro ao escrever o bilhete, rever a escrita")
                .Must(IsBilheteUnique)
                .WithMessage("Bilhete duplicado");

            RuleFor(x => x.Rupe)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo obrigatorio")
                .Must(IsRupeUnique)
                .WithMessage("Name must be unique");
            
            RuleFor(x => x.Rua)
                .MinimumLength(10)
                .WithMessage("Minimo de 10 Carateres")
                .MaximumLength(500)
                .WithMessage("MAximo 500 caracteres");
            
            RuleFor(x => x.Casa)
                .MaximumLength(10)
                .WithMessage("Maximo de 3 carateres");

            RuleFor(x => x.Telefone)
                .Length(13)
                .WithMessage("Maximo de 13 carateres");
            
            RuleFor(x => x.Sexo)
                .Length(1)
                .WithMessage("Maximo de 1 carater");
            
            RuleFor(x => x.EstadoCivil)
                .Length(10)
                .WithMessage("Maximo de 10 carateres");
        } 
        public bool IsBilheteUnique(AddCandidatoDTO candidato, string newValue)
        {
            return _candidatos.All(_candidato => 
                _candidato.Equals(candidato) || 
                _candidato.Pessoa.NumeroBilhete != newValue);
        }  
        public bool IsRupeUnique(AddCandidatoDTO candidato, string newValue)
        {
            return _candidatos.All(_candidato => 
                _candidato.Equals(candidato) || 
                _candidato.Rupe != newValue);
        }   
    }
}