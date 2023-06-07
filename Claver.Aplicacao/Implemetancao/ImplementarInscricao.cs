using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using AutoMapper;
using Claver.Dominio.DTOS.InscricaoDTOs;
using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.Entidades;
using FluentValidation;

namespace Claver.Aplicacao.Implemetancao
{
    public class ImplementarInscricao
    {
        
        #region variaveis globais
            private readonly IUnidadeTrabalho _ut;
            private readonly IMapper _mapper;
            private readonly IValidator<AddCandidatoDTO> _validator;
            DateTime _dataAtual = DateTime.Now;
            private string _regex = @"^[0-9]{9}[a-zA-Z]{2}[0-9]{3}$";
            string order = new Random(2).Next(1, 10000).ToString();
        #endregion 

        public ImplementarInscricao( IUnidadeTrabalho ut, IMapper mapper, 
        IValidator<AddCandidatoDTO> validator)
        {
            _validator = validator;
            _ut = ut;
            _mapper = mapper;
        }

        // inserir novo inscricao
        #region Nova Candidatura
        public async Task<Inscricao> Adicionar(AddCandidatoDTO inscricao, Guid _classe, Guid _curso, Guid _ano, Guid _periodo)
        {
            // idade maior ou igual a 14 anos
            int idade = 0;
            idade = DateTime.Now.Year - inscricao.DataNascimento.Year;
            if (DateTime.Now.DayOfYear < inscricao.DataNascimento.DayOfYear)
                idade = idade - 1;

            try
            {

                #region verificacoes

                // buscar classe, anoAcademico e curso existente. por id
                var curso = await _ut.Cursos.Listar(_curso);
                var classe = await _ut.Classes.BuscarPorId(_classe);
                var anoAcademico = await _ut.AnoLectivo.BuscarPorId(_ano);
                var periodo = await _ut.Periodos.BuscarPorId(_periodo);

                // verificar dependecias, se existem
                
                // o nome nao pode ser nulo
                if ( inscricao.NomeCompleto.Trim().ToUpper()== string.Empty  
                    || inscricao.NomeCompleto.Trim().ToUpper()== null 
                    || inscricao.NomeCompleto.Trim().ToUpper()== "string" 
                    || inscricao.NomeCompleto.Trim().ToUpper()== "STRING")
                    throw new NullReferenceException("Nome mal inserido, inseira o Classe como deve ser.");
                if(inscricao.NumeroBilhete.Trim().ToUpper().Length < 14 ||inscricao.NumeroBilhete.Trim().ToUpper().Length > 14)
                    throw new ArgumentException("O bilhete de identidade nao foi bem escrito.");
                
                if(!Regex.IsMatch(inscricao.NumeroBilhete.Trim().ToUpper(), _regex))
                    throw new ArgumentException("O bilhete de identidade nao corresponde o padrão 000000000AZ000.");
                // ao cadastrar emitir nova candidatura deve informar o curso e a classe.
                if (curso == null && classe == null && anoAcademico == null)
                    throw new NullReferenceException("selecione o curso, a classe e o ano academico.");
                // verificar se o ano academico esta ativo
                if (anoAcademico.Ativo == false)
                {
                    throw new Exception("O ano académico selecionado, não está em vigor, por favor contacte o administrador do sistema.");
                }
                // verificar a extensao do sexo
                if(inscricao.Sexo.Length > 1)
                    throw new NullReferenceException("sexo so admite m para masculino e f para feminino.");

                if(inscricao.Sexo.Trim().ToUpper() != "M" && inscricao.Sexo.Trim().ToUpper() != "F")
                    throw new NullReferenceException("sexo so admite m para masculino e f para feminino.");
                #endregion             
                
                // variavel temporaria para armazenar informacoes para nova inscricao
                var inscricaoAdd = new Inscricao{
                    Admitido = false,
                    DataRegistro = _dataAtual,
                    Rupe = inscricao.Rupe.Trim().ToUpper(),
                    NumCandidato = curso.Sigla + classe.DescricaoClasse + anoAcademico.AnoAcademico + order,
                    Pessoa = new Pessoa{
                        NomeCompleto  = inscricao.NomeCompleto.Trim().ToUpper(),
                        NomePai  = inscricao.NomePai.Trim().ToUpper(),
                        NomeMae = inscricao.NomeMae.Trim().ToUpper(),
                        DataNascimento = inscricao.DataNascimento.Date,
                        NumeroBilhete  = inscricao.NumeroBilhete.Trim().ToUpper(),
                        DataRevisao = null,
                        Idade = idade,
                        DataRegistro  = _dataAtual, 
                        Sexo = inscricao.Sexo.Trim().ToUpper(),
                        EstadoCivil  = inscricao.EstadoCivil,
                        Contacto = new Contacto{
                            DataRegistro = _dataAtual,
                            Email = inscricao.Email,
                            Telefone = inscricao.Telefone
                        },
                        Endereco = new Endereco{
                            DataRegistro = _dataAtual,
                            Provincia = inscricao.Provincia.Trim().ToUpper(),
                            Municipio = inscricao.Municipio.Trim().ToUpper(),   
                            Rua = inscricao.Rua.Trim().ToUpper(),
                        },
                    },

                    Curso = curso,
                    Classe = classe,
                    Periodo = periodo,
                    AnoLectivo = anoAcademico
                };

                #region Verificacoes

                //verificar se rupe ja existe
                var buscarRupe = await _ut.Inscricao.BuscarPorRupe(inscricao.Rupe);
                if(buscarRupe.Count() > 0)
                {
                    throw new NullReferenceException("O rupe que tentou inserir ja existe no sistema");
                }
                //verificar se rupe ja existe
                var buscarBilhete = await _ut.Inscricao.BuscarPorBilhete(inscricao.NumeroBilhete);
                if(buscarBilhete.Count() != 0)
                {
                    throw new NullReferenceException("O bilhete que tentou inserir ja existe no sistema.");
                }

                
                if(idade < 14)
                        throw new NullReferenceException($"para se candidatar, precisa ter idade minima de 14 anos. voce tem {idade} anos");
                    
                if(inscricaoAdd.Classe.DescricaoClasse != "7A" && inscricaoAdd.Classe.DescricaoClasse != "10A")
                    throw new NullReferenceException("so sao permitidas candidaturas para a setima e para a decima classe.");

                #endregion
                if (inscricao.Foto != null)
                    {
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(inscricao.Foto.FileName)}";
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Arquivos", "Images", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await inscricao.Foto.CopyToAsync(stream);
                        }
                        inscricaoAdd.Pessoa.FotoDesc = fileName;
                    }
                    await _ut.Inscricao.Inserir(inscricaoAdd);
                    await _ut.Finalizar();
                    await _ut.Dispose();
                return inscricaoAdd;
                // return await _ut.Inscricao.Inserir(inscricaoAdd);
            }
            catch (Exception ex)
            {
                throw new SqlTypeException(ex.Message);
            }     
        
        }
        #endregion    

        // listar candidato por id
        #region Listar(Id)
        public async Task<Inscricao> Listar(Guid id)
        {
            var candidato = await _ut.Inscricao.Listar(id);
            if (candidato == null)
            {
                return null;
            }
            // var candidatoDto = _mapper.Map<AddCandidatoDTO>(candidato);
            return candidato;
        }
        #endregion
    
        // listar todos candidatos
        #region Listar tudo
        public async Task<IEnumerable<Inscricao>> Listar()
        {
            try
            {
                var inscritos =await _ut.Inscricao.Listar();
                if (inscritos != null)
                {
                    await _ut.Dispose();
                    return inscritos.ToList(); 
                }
                throw new NullReferenceException("Nenhum registro encontrado.");
            }
            catch (Exception)
            {
                throw new NullReferenceException("Nenhum registro encontrado.");
            }
        }
        #endregion 

        // Listar por intervalo de datas
        #region Listar por datas
        public async Task<IEnumerable<Inscricao>> Listar( DateTime data1, DateTime data2)
        {
            try
            {
                var inscritos = await _ut.Inscricao.BuscarPorIntervalo(data1, data2);
                if (inscritos != null)
                {
                    await _ut.Dispose();
                    return inscritos.ToList();
                    
                }
                throw new NullReferenceException("Nenhum registro encontrado.");
                
            }
            catch (Exception ex)
            {
                throw new NullReferenceException($"{ex.Message}");
            }
        }
        #endregion

        // Finalizar Inscricao
        #region Finalizar Inscricao
        public async Task<Inscricao> ValidarCandidatura(AdmitirCandidatoDTO candidato)
        {
            try
            {
                // candidato a ser atualizado 
                var buscarCandidato = await  _ut.Inscricao.Listar(candidato.Id);

                if (buscarCandidato == null)
                    throw new NullReferenceException("candidato nao encontrado, por favor cadastre.");
                
                buscarCandidato.Admitido = candidato.Admitido;
                if(candidato.Admitido == true){
                    buscarCandidato.DataAdmissao = _dataAtual;
                }
                
                buscarCandidato.IsCompleto = candidato.IsCompleto;
                if(candidato.IsCompleto == true){
                    buscarCandidato.DataFinalizacao = _dataAtual;
                }
                // buscarCandidato.OrderList = buscarCandidato.OrderList;

                await _ut.Inscricao.Validar(buscarCandidato);
                await _ut.Finalizar();
                await _ut.Dispose();

                return buscarCandidato;

            }
            catch (System.Exception ex)
            {
                throw new NullReferenceException(ex.Message);
            }
             
        }
        #endregion

    }
}