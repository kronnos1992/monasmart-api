using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.DTOS.PeriodoDTOs;
using Claver.Dominio.Entidades;

namespace Claver.Aplicacao.Implemetancao
{
    public class ImplementarPeriodo
    {
        private readonly IUnidadeTrabalho _ut;
    // variaveis necessarias
    DateTime _dataAtual = DateTime.Now;
    // private readonly IMapper _mapper;
    public ImplementarPeriodo( IUnidadeTrabalho ut)
    {
        _ut = ut;
        // _mapper = mapper;
    }

    // inserir novo candidato
    #region Inserir Periodo   
    public async Task<Periodo> Adicionar(AddPeriodoDTO periodo)
    {
        try
        {
            var periodos  = await this.ExibirPeriodo(periodo.TipoPeriodo.Trim().ToUpper());
            if(periodos.Count() > 0)
                throw new NullReferenceException("o periodo que tentou inserir ja existe.");
        
            // mapeamento
            var _periodo = new Periodo{
                DataRegistro = _dataAtual,
                DataRevisao = null,
                TipoPeriodo = periodo.TipoPeriodo.Trim().ToUpper(),
                Descricao = periodo.Descricao.Trim().ToUpper() 
            };

            await _ut.Periodos.Inserir(_periodo);
            return _periodo;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }        
    }

    #endregion
    
    // Exibir por nome
    #region Exibir por Periodo
        public async Task<IEnumerable<Periodo>> ExibirPeriodo(string _periodo)
        {  
            try
            {
                var periodo =await _ut.Periodos.BuscarPeriodo(_periodo.Trim().ToUpper());
                return periodo.ToList(); 
            }
            catch (System.Exception)
            {
                throw new NullReferenceException("Nenhum registro encontrado.");
            }
        }        
    #endregion  

    // Exibir todos Periodos
    #region Exibir tudo
         public async Task<IEnumerable<Periodo>> ExibirTudo()
         {
            try
            {
                var Periodo =await _ut.Periodos.BuscarTodos();
                return Periodo.ToList(); 
            }
            catch (System.Exception)
            {
                throw new NullReferenceException("Nenhum registro encontrado.");
            }
         }
    #endregion 

    // atualizar Periodo
    #region Atualizar Periodo
        public async Task<Periodo> Atualizar(UpdatePeriodoDTO periodo)
        {
            // Periodo a ser atualizado 
            var buscarPeriodo =await  _ut.Periodos.BuscarPorId(periodo.Id);

            if (buscarPeriodo == null)
            {
                throw new NullReferenceException("Periodo nao encontrado, por favor cadastre.");
            }

            buscarPeriodo.DataRevisao = _dataAtual;
            buscarPeriodo.TipoPeriodo= periodo.TipoPeriodo.Trim().ToUpper();
            buscarPeriodo.Descricao= periodo.Descricao.Trim().ToUpper();

            // Nome nao pode ser nulo
            if ( buscarPeriodo.TipoPeriodo.Trim().ToUpper()== string.Empty  
            || buscarPeriodo.TipoPeriodo.Trim().ToUpper()== null 
            || buscarPeriodo.TipoPeriodo.Trim().ToUpper()== "string" || buscarPeriodo.TipoPeriodo.Trim().ToUpper()== "STRING")
            {
                throw new NullReferenceException("Periodo mal inserido, inseira o Periodo como deve ser.");
            }
            else{
                try
                {
                    return await _ut.Periodos.Atualizar(buscarPeriodo);
                }
                catch (System.Exception ex)
                {
                    throw new NullReferenceException(ex.Message);
                }
            }  
        }
    #endregion

    // eliminar Periodo
    #region Eliminar Periodo
        public async Task<bool> Eliminar(Guid id)
        {
            // Periodo a ser atualizado 
            var buscarPeriodo =await  _ut.Periodos.BuscarPorId(id);

            if (buscarPeriodo == null)
            {
                throw new NullReferenceException("Periodo nao encontrado, por favor cadastre.");
            }
            // verificar se o nome ja exisate
            try
            {
                await _ut.Periodos.Eliminar(id);
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