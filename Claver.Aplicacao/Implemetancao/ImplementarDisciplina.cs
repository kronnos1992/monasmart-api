using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.DTOS.DisciplinaDTO;
using Claver.Dominio.Entidades;

namespace Claver.Aplicacao.Implemetancao
{
    public class ImplementarDisciplina
    {
        private readonly IUnidadeTrabalho _ut;
    // variaveis necessarias
    DateTime _dataAtual = DateTime.Now;
    // private readonly IMapper _mapper;
    public ImplementarDisciplina( IUnidadeTrabalho ut)
    {
        _ut = ut;
        // _mapper = mapper;
    }

    // inserir novo candidato
    #region Inserir Disciplina   
    public async Task<Disciplina> Adicionar(AddDisciplinaDTO disciplina)
    {
    
        // mapeamento
        var _disciplina = new Disciplina{
            Id = Guid.NewGuid(),
            Descricao= disciplina.Descricao.Trim().ToUpper(),
            DataRegistro = _dataAtual,
            DataRevisao = null
        };

        // Nome nao pode ser nulo
        if ( disciplina.Descricao== string.Empty  || disciplina.Descricao== null || disciplina.Descricao== "string" 
        || disciplina.Descricao== "STRING")
        {
            throw new NullReferenceException("Erro N. ICM-0001: Disciplina mal inserido, inseira o Disciplina como deve ser.");
        }
        else{
            // verificar se o Disciplina ja exisate
            try
            {
                var _disciplinas = await this.ExibirPorNome(disciplina.Descricao.Trim().ToUpper());
                if (_disciplinas.Count() > 0)
                {
                    throw new NullReferenceException(" A Disciplina que tentou inserir ja existe.");
                }
                else{
                    return await _ut.Disciplinas.Inserir(_disciplina);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }  
    
    }

    #endregion
    
    // Exibir por nome
    #region Exibir por nome 
        public async Task<IEnumerable<Disciplina>> ExibirPorNome(string nome)
        {
            
            try
            {
                var Disciplina =await _ut.Disciplinas.BuscarDisciplina(nome.Trim().ToUpper());
                return Disciplina.ToList(); 
            }
            catch (System.Exception)
            {
                throw new ArgumentNullException("Nenhum registro encontrado.");
            }
        }        
    #endregion  

    // Exibir todos Disciplinas
    #region Exibir tudo
        public async Task<IEnumerable<Disciplina>> ExibirTudo()
        {
            try
            {
                var Disciplina =await _ut.Disciplinas.BuscarTodos();
                return Disciplina.ToList(); 
            }
            catch (System.Exception)
            {
                throw new NullReferenceException("Nenhum registro encontrado.");
            }
        }
    #endregion 

    // atualizar Disciplina
    #region Atualizar Disciplina
        public async Task<Disciplina> Atualizar(AtualizarDisciplinaDTO disciplina)
        {
            // Disciplina a ser atualizado 
            var buscarDisciplina =await  _ut.Disciplinas.BuscarPorId(disciplina.Id);

            if (buscarDisciplina == null)
            {
                throw new NullReferenceException("Disciplina nao encontrado, por favor cadastre.");
            }

            buscarDisciplina.DataRevisao = _dataAtual;
            buscarDisciplina.Descricao= disciplina.Descricao.Trim().ToUpper();

            // Nome nao pode ser nulo
            if ( buscarDisciplina.Descricao.Trim().ToUpper()== string.Empty  
            || buscarDisciplina.Descricao.Trim().ToUpper()== null 
            || buscarDisciplina.Descricao.Trim().ToUpper()== "string" || buscarDisciplina.Descricao.Trim().ToUpper()== "STRING")
            {
                throw new NullReferenceException("Erro N. ICM-0001: Disciplina mal inserido, inseira o Disciplina como deve ser.");
            }
            else{
                try
                {
                    return await _ut.Disciplinas.Atualizar(buscarDisciplina);
                }
                catch (System.Exception ex)
                {
                    throw new NullReferenceException(ex.Message);
                }
            }  
        }
    #endregion

    // eliminar Disciplina
    #region Eliminar Disciplina
        public async Task<bool> Eliminar(Guid id)
        {
            // Disciplina a ser atualizado 
            var buscarDisciplina =await  _ut.Disciplinas.BuscarPorId(id);

            if (buscarDisciplina == null)
            {
                throw new NullReferenceException("Disciplina nao encontrado, por favor cadastre.");
            }
            // verificar se o nome ja exisate
            try
            {
                await _ut.Disciplinas.Eliminar(id);
                return true;
            }
            catch (System.Exception ex)
            {
                throw new NullReferenceException(ex.Message);
            } 
        }
    #endregion

    // Salvar no banco
    #region Salvar no Banco
        public async Task<int> Persistir()
        {
            return await _ut.Finalizar();
        }
    #endregion  
    }
}