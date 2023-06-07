using Claver.Dominio.DTOS.ClasseDTOs;
using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.Entidades;

namespace Claver.Aplicacao.Implemetancao;

public class ImplementarClasse
{
    private readonly IUnidadeTrabalho _ut;
    // variaveis necessarias
    DateTime _dataAtual = DateTime.Now;
    // private readonly IMapper _mapper;
    public ImplementarClasse( IUnidadeTrabalho ut)
    {
        _ut = ut;
        // _mapper = mapper;
    }

    // inserir novo candidato
    #region Inserir Classe   
    public async Task<Classe> Adicionar(AddClasseDTO classe)
    {
    
        // mapeamento
        var _Classe = new Classe{
            Id = Guid.NewGuid(),
            DescricaoClasse= classe.DescricaoClasse.Trim().ToUpper(),
            DataRegistro = _dataAtual,
            DataRevisao = null
        };
        // tratamento de erros
        var Classes = await _ut.Classes.BuscarPorNome(classe.DescricaoClasse);
        // Nome nao pode ser nulo
        if ( classe.DescricaoClasse== string.Empty  || classe.DescricaoClasse== null || classe.DescricaoClasse== "string" || classe.DescricaoClasse== "STRING")
        {
            throw new NullReferenceException("Classe mal inserida, inseira o Classe como deve ser.");
        }
        else{
            // verificar se o Classe ja exisate
            try
            {
                var _Classes = await this.ExibirPorNome(classe.DescricaoClasse.Trim().ToUpper());
                if (_Classes.Count() > 0)
                {
                    throw new NullReferenceException(" A Classe que tentou inserir ja existe.");
                }
                else{
                    return await _ut.Classes.Inserir(_Classe);
                }
            }
            catch (Exception ex)
            {
                throw new NullReferenceException(ex.Message);
            }
        }  
    
    }

    #endregion
    
    // Exibir por nome
    #region Exibir por nome 
        public async Task<IEnumerable<Classe>> ExibirPorNome(string nome)
        {
            
            try
            {
                var Classe =await _ut.Classes.BuscarPorNome(nome.Trim().ToUpper());
                return Classe.ToList(); 
            }
            catch (System.Exception)
            {
                throw new ArgumentNullException("Nenhum registro encontrado.");
            }
        }        
    #endregion  

    // Exibir todos Classes
    #region Exibir tudo
        public async Task<IEnumerable<Classe>> ExibirTudo()
        {
            try
            {
                var Classe =await _ut.Classes.BuscarTodos();
                return Classe.ToList(); 
            }
            catch (System.Exception)
            {
                throw new NullReferenceException("Nenhum registro encontrado.");
            }
        }
    #endregion 

    // atualizar classe
    #region Atualizar Classe
        public async Task<Classe> Atualizar(AtualizarClasseDTO Classe)
        {
            // Classe a ser atualizado 
            var buscarClasse =await  _ut.Classes.BuscarPorId(Classe.Id);

            if (buscarClasse == null)
            {
                throw new NullReferenceException("Classe nao encontrada, por favor cadastre.");
            }

            buscarClasse.DataRevisao = _dataAtual;
            buscarClasse.DescricaoClasse= Classe.DescricaoClasse.Trim().ToUpper();

            // Nome nao pode ser nulo
            if ( buscarClasse.DescricaoClasse.Trim().ToUpper()== string.Empty  
            || buscarClasse.DescricaoClasse.Trim().ToUpper()== null 
            || buscarClasse.DescricaoClasse.Trim().ToUpper()== "string" || buscarClasse.DescricaoClasse.Trim().ToUpper()== "STRING")
            {
                throw new NullReferenceException("Erro N. ICM-0001: Classe mal inserido, inseira o Classe como deve ser.");
            }
            else{
                try
                {
                    return await _ut.Classes.Atualizar(buscarClasse);
                }
                catch (System.Exception ex)
                {
                    throw new NullReferenceException(ex.Message);
                }
            }  
        }
    #endregion

    // eliminar classe
    #region Eliminar Classe
        public async Task<bool> Eliminar(Guid id)
        {
            // Classe a ser atualizado 
            var buscarClasse =await  _ut.Classes.BuscarPorId(id);

            if (buscarClasse == null)
            {
                throw new NullReferenceException("Classe não encontrada, por favor cadastre.");
            }
            // verificar se o nome ja exisate
            try
            {
                await _ut.Classes.Eliminar(id);
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
