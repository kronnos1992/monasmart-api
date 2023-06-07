using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.DTOS.SalaDTOs;
using Claver.Dominio.Entidades;

namespace Claver.Aplicacao.Implemetancao;

public class ImplementarSala
{
    private readonly IUnidadeTrabalho _ut;
    // variaveis necessarias
    DateTime _dataAtual = DateTime.Now;
    // private readonly IMapper _mapper;
    public ImplementarSala( IUnidadeTrabalho ut)
    {
        _ut = ut;
        // _mapper = mapper;
    }

    // inserir novo candidato
    #region Inserir Sala   
    public async Task<Sala> Adicionar(AddSalaDTO sala)
    {
    
        // mapeamento
        var _sala = new Sala{
            Id = Guid.NewGuid(),
            NumeroSala= sala.NumeroSala.Trim().ToUpper(),
            // Capacidade = sala.Capacidade,
            DataRegistro = _dataAtual,
            DataRevisao = null
        };
        // tratamento de erros
        var Salas = await _ut.Salas.BuscarSala(sala.NumeroSala);
        // Nome nao pode ser nulo
        if ( sala.NumeroSala== string.Empty  || sala.NumeroSala== null || sala.NumeroSala== "string" || sala.NumeroSala== "STRING")
        {
            throw new NullReferenceException("Erro N. ICM-0001: Sala mal inserido, inseira o Sala como deve ser.");
        }
        else{
            // verificar se o Sala ja exisate
            try
            {
                var _Salas = await this.ExibirPorSala(sala.NumeroSala.Trim().ToUpper());
                if (_Salas.Count() > 0)
                {
                    throw new NullReferenceException("Erro N. ICM-0002: A Sala que tentou inserir ja existe.");
                }
                else{
                    return await _ut.Salas.Inserir(_sala);
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
    #region Exibir por Sala
        public async Task<IEnumerable<Sala>> ExibirPorSala(string sala)
        {
            
            try
            {
                var Sala =await _ut.Salas.BuscarSala(sala.Trim().ToUpper());
                return Sala.ToList(); 
            }
            catch (System.Exception)
            {
                throw new ArgumentNullException("Nenhum registro encontrado.");
            }
        }        
    #endregion  

    // Exibir todos Salas
    #region Exibir tudo
        public async Task<IEnumerable<Sala>> ExibirTudo()
        {
            try
            {
                var Sala =await _ut.Salas.BuscarTodos();
                return Sala.ToList(); 
            }
            catch (System.Exception)
            {
                throw new NullReferenceException("Nenhum registro encontrado.");
            }
        }
    #endregion 

    // atualizar Sala
    #region Atualizar Sala
        public async Task<Sala> Atualizar(AtualizarSalaDTO sala)
        {
            // Sala a ser atualizado 
            var buscarSala =await  _ut.Salas.BuscarPorId(sala.Id);

            if (buscarSala == null)
            {
                throw new NullReferenceException("Sala nao encontrado, por favor cadastre.");
            }

            buscarSala.DataRevisao = _dataAtual;
            buscarSala.NumeroSala= sala.NumeroSala.Trim().ToUpper();
            // buscarSala.Capacidade = sala.Capacidade;

            // Nome nao pode ser nulo
            if ( buscarSala.NumeroSala.Trim().ToUpper()== string.Empty  
            || buscarSala.NumeroSala.Trim().ToUpper()== null 
            || buscarSala.NumeroSala.Trim().ToUpper()== "string" || buscarSala.NumeroSala.Trim().ToUpper()== "STRING")
            {
                throw new NullReferenceException("Erro N. ICM-0001: Sala mal inserido, inseira o Sala como deve ser.");
            }
            else{
                try
                {
                    return await _ut.Salas.Atualizar(buscarSala);
                }
                catch (System.Exception ex)
                {
                    throw new NullReferenceException(ex.Message);
                }
            }  
        }
    #endregion

    // eliminar Sala
    #region Eliminar Sala
        public async Task<bool> Eliminar(Guid id)
        {
            // Sala a ser atualizado 
            var buscarSala =await  _ut.Salas.BuscarPorId(id);

            if (buscarSala == null)
            {
                throw new NullReferenceException("Sala nao encontrado, por favor cadastre.");
            }
            // verificar se o nome ja exisate
            try
            {
                await _ut.Salas.Eliminar(id);
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
