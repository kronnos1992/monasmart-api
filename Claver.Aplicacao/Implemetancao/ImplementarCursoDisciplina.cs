using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.Entidades;

namespace Claver.Aplicacao.Implemetancao
{
    public class ImplementarCursoDisciplina
    {
        private readonly IUnidadeTrabalho _ut;
    // variaveis necessarias
    DateTime _dataAtual = DateTime.Now;
    // private readonly IMapper _mapper;
    public ImplementarCursoDisciplina( IUnidadeTrabalho ut)
    {
        _ut = ut;
        // _mapper = mapper;
    }

    // inserir novo candidato
    #region Inserir CursoDisciplina   
    // public async Task<CursoDisciplina> Adicionar(CursoDisciplina cursoDisciplina, 
    //     Guid curso, Guid periodo, Guid disciplina)
    // {
    //     var _curso = await _ut.Cursos.BuscarPorId(curso);
    //     var _periodo = await _ut.Periodos.BuscarPorId(periodo);
    //     var _disciplina = await _ut.Disciplinas.BuscarPorId(disciplina);
    
    //     // mapeamento
    //     var _cursoDisciplina = new CursoDisciplina{
    //         Duracao = cursoDisciplina.Duracao,
    //         CargaHoraria = cursoDisciplina.CargaHoraria,
    //         DataRegistro = _dataAtual,
    //         CursoId = _curso.Id,
    //         PeriodoId = _periodo.Id,
    //         DisciplinaId = _disciplina.Id,
    //         DataRevisao = string.Empty
    //     };
    //     await _ut.CursoDisciplinas.Inserir(_cursoDisciplina);
    //     return _cursoDisciplina;        
    // }

    #endregion
    
    // Exibir por nome
    #region Exibir por CursoDisciplina
        // public async Task<IEnumerable<CursoDisciplina>?> ExibirPorCursoDisciplina(string CursoDisciplina)
        // {
            
        //     try
        //     {
        //         var CursoDisciplina =await _ut.CursoDisciplinas.BuscarCursoDisciplina(CursoDisciplina.Trim().ToUpper());
        //         return CursoDisciplina.ToList(); 
        //     }
        //     catch (System.Exception ex)
        //     {
        //         throw new ArgumentNullException("Nenhum registro encontrado.");
        //     }
        // }        
    #endregion  

    // Exibir todos CursoDisciplinas
    #region Exibir tudo
         public async Task<IEnumerable<CursoDisciplina>> ExibirTudo()
         {
            try
            {
                var CursoDisciplina =await _ut.CursoDisciplinas.BuscarTodos();
                return CursoDisciplina.ToList(); 
            }
            catch (System.Exception)
            {
                throw new NullReferenceException("Nenhum registro encontrado.");
            }
         }
    #endregion 

    // atualizar CursoDisciplina
    #region Atualizar CursoDisciplina
        public async Task<CursoDisciplina> Atualizar(CursoDisciplina cursoDisciplina)
        {
            // CursoDisciplina a ser atualizado 
            var buscarCursoDisciplina =await  _ut.CursoDisciplinas.BuscarPorId(cursoDisciplina.Id);

            if (buscarCursoDisciplina == null)
            {
                throw new NullReferenceException("CursoDisciplina nao encontrado, por favor cadastre.");
            }

            buscarCursoDisciplina.DataRevisao = _dataAtual;
            buscarCursoDisciplina.CursoId= cursoDisciplina.CursoId;
            buscarCursoDisciplina.Duracao = cursoDisciplina.Duracao;
            buscarCursoDisciplina.CargaHoraria = cursoDisciplina.CargaHoraria;
            buscarCursoDisciplina.CursoId = cursoDisciplina.CursoId;
            buscarCursoDisciplina.PeriodoId = cursoDisciplina.PeriodoId;
            buscarCursoDisciplina.DisciplinaId =cursoDisciplina.DisciplinaId;

            return await _ut.CursoDisciplinas.Atualizar(buscarCursoDisciplina); 
        }
    #endregion

    // eliminar CursoDisciplina
    #region Eliminar CursoDisciplina
        public async Task<bool> Eliminar(Guid id)
        {
            // CursoDisciplina a ser atualizado 
            var buscarCursoDisciplina =await  _ut.CursoDisciplinas.BuscarPorId(id);

            if (buscarCursoDisciplina == null)
            {
                throw new NullReferenceException("CursoDisciplina nao encontrado, por favor cadastre.");
            }
            // verificar se o nome ja exisate
            try
            {
                await _ut.CursoDisciplinas.Eliminar(id);
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