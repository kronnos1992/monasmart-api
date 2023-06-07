using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.DTOS.TurmaSalaDTO;
using Claver.Dominio.Entidades;

namespace Claver.Aplicacao.Implemetancao
{
    public class ImplementarTurmaSala
    {
        private readonly IUnidadeTrabalho _ut;
    // variaveis necessarias
    DateTime _dataAtual = DateTime.Now;
    // private readonly IMapper _mapper;
    public ImplementarTurmaSala( IUnidadeTrabalho ut)
    {
        _ut = ut;
        // _mapper = mapper;
    }

    // inserir novo candidato
    #region Inserir TurmaSala   
    public async Task<TurmaSala> Associar(Guid sala, Guid turma)
    {
        var _sala = await _ut.Salas.BuscarPorId(sala);
        var _turma = await _ut.Turmas.BuscarPorId(turma);

        if(_sala == null)
            throw new NullReferenceException("Nenhuma sala encontrada");

        if(_turma == null)
            throw new NullReferenceException("Nenhuma turma encontrada");

        try
        {        
            // mapeamento
            var _turmaSala = new TurmaSala{
                Id = Guid.NewGuid(),
                DataRegistro = _dataAtual,
                Sala = _sala,
                Turma = _turma
            };
            await _ut.TurmaSala.Inserir(_turmaSala);
            return _turmaSala;
        }
        catch (Exception ex)
        {
            
            throw new Exception(ex.Message);
        }        
    }

    #endregion  

    // Exibir todos TurmaSalas
    #region Exibir tudo
         public async Task<IEnumerable<TurmaSala>> ExibirTudo()
         {
            try
            {
                var TurmaSala =await _ut.TurmaSala.BuscarTodos();
                return TurmaSala.ToList(); 
            }
            catch (System.Exception)
            {
                throw new NullReferenceException("Nenhum registro encontrado.");
            }
         }
    #endregion 

    // atualizar TurmaSala
    #region Atualizar TurmaSala
        public async Task<TurmaSala> Atualizar(TurmaSala turmaSala, Guid id)
        {
            // TurmaSala a ser atualizado 
            var buscarTurmaSala =await  _ut.TurmaSala.BuscarPorId(id);
            var _turma =await  _ut.Turmas.BuscarPorId(turmaSala.TurmaId);
            var _sala =await  _ut.Salas.BuscarPorId(turmaSala.SalaId);

            if (buscarTurmaSala == null)
            {
                throw new NullReferenceException("Registro não encontrado, por favor cadastre.");
            }

            buscarTurmaSala.DataRevisao = _dataAtual;
            buscarTurmaSala.Sala = _sala;
            buscarTurmaSala.Turma = _turma;


            return await _ut.TurmaSala.Atualizar(buscarTurmaSala); 
        }
    #endregion

    // eliminar TurmaSala
    #region Eliminar TurmaSala
        public async Task<bool> Eliminar(Guid id)
        {
            // TurmaSala a ser atualizado 
            var buscarTurmaSala =await  _ut.TurmaSala.BuscarPorId(id);

            if (buscarTurmaSala == null)
            {
                throw new NullReferenceException("TurmaSala nao encontrado, por favor cadastre.");
            }
            // verificar se o nome ja exisate
            try
            {
                await _ut.TurmaSala.Eliminar(id);
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