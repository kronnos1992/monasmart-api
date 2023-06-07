using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.DTOS.TurmaDTOs;
using Claver.Dominio.Entidades;

namespace Claver.Aplicacao.Implemetancao;

public class ImplementarTurma
{
    private readonly IUnidadeTrabalho _ut;
    // variaveis necessarias
    string _dataAtual = DateTime.Now.ToString("dd/MM/yyyy - HH:mm");
    // private readonly IMapper _mapper;
    public ImplementarTurma( IUnidadeTrabalho ut)
    {
        _ut = ut;
        // _mapper = mapper;
    }

    // inserir nova turma
    #region Inserir Turma   
    // public async Task<Turma> Adicionar(AddTurmaDTO turma, Guid _curso, Guid _periodo)
    // {
    //     try
    //     {
    //         var curso = await _ut.Cursos.BuscarPorId(_curso);
    //         var periodo = await _ut.Periodos.BuscarPorId(_periodo);
            
    //         // tratamento de erros
    //         var _turmas = await this.ExibirPorNome(turma.NomeTurma.Trim().ToUpper());

    //         if(curso == null)
    //             throw new NullReferenceException("Curso não encontrado.");

    //         if(periodo == null)
    //             throw new NullReferenceException("Periodo não encontrado.");
            
            
    //         // Nome nao pode ser nulo
    //         if ( turma.NomeTurma== string.Empty  || turma.NomeTurma== null || turma.NomeTurma== "string" || turma.NomeTurma== "STRING")
    //             throw new NullReferenceException("Turma mal inserido, inseira o Turma como deve ser.");
            
    //         if (_turmas.Count() > 0)
    //             throw new NullReferenceException("A Turma que tentou inserir ja existe.");

    //         // mapeamento
    //         var turmaAdd = new Turma{
    //             Id = Guid.NewGuid(),
    //             NomeTurma= turma.NomeTurma.Trim().ToUpper(),
    //             TotalTurma = turma.TotalTurma,
    //             Curso = curso,
    //             Periodo = periodo,
    //             DataRegistro = _dataAtual,
    //             DataRevisao = string.Empty
    //         };
    //         return await _ut.Turmas.Inserir(turmaAdd);
    //     }
    //     catch (Exception ex)
    //     {
    //         throw new Exception(ex.Message);
    //     }    
    // }

    #endregion
    
    // Exibir por nome
    #region Exibir por nome 
        public async Task<IEnumerable<Turma>>ExibirPorNome(string nome)
        {
            
            try
            {
                var turma = await _ut.Turmas.BuscarTurma(nome.Trim().ToUpper());
                return turma.ToList(); 
            }
            catch (System.Exception)
            {
                throw new ArgumentNullException("Nenhum registro encontrado.");
            }
        }        
    #endregion  

    // Exibir todos Turmas
    #region Exibir tudo
        public async Task<IEnumerable<Turma>> ExibirTudo()
        {
            try
            {
                var Turma =await _ut.Turmas.BuscarTodos();
                return Turma.ToList(); 
            }
            catch (System.Exception)
            {
                throw new NullReferenceException("Nenhum registro encontrado.");
            }
        }
    #endregion 

    // atualizar Turma
    // #region Atualizar Turma
    //     public async Task<Turma> Atualizar(AtualizarTurmaDTO turma)
    //     {
    //         try
    //         {
    //             // Turma a ser atualizado 
    //             var buscarTurma =await  _ut.Turmas.BuscarPorId(turma.Id);
    //             var buscarCurso =await  _ut.Cursos.BuscarPorId(turma.CursoId);
    //             var buscarPeriodo =await  _ut.Periodos.BuscarPorId(turma.PeriodoId);

    //             // validar existencia de curso e periodo
    //             if (buscarCurso == null)
    //                 throw new NullReferenceException("Curso nao encontrado.");

    //             if (buscarPeriodo == null)
    //                 throw new NullReferenceException("Periodo nao encontrado.");

    //             if (buscarTurma == null)
    //                 throw new NullReferenceException("Turma nao encontrada, por favor cadastre.");

    //             buscarTurma.DataRevisao = DateTime.Now.ToString("dd/MM/yyyy - HH:mm");
    //             buscarTurma.TotalTurma = turma.TotalTurma;
    //             buscarTurma.NomeTurma = turma.NomeTurma.Trim().ToUpper();
    //             buscarTurma.Curso = buscarCurso;
    //             buscarTurma.Periodo = buscarPeriodo;

    //             // Nome nao pode ser nulo
    //             if ( buscarTurma.NomeTurma.Trim().ToUpper()== string.Empty  
    //             || buscarTurma.NomeTurma.Trim().ToUpper()== null 
    //             || buscarTurma.NomeTurma.Trim().ToUpper()== "string" || buscarTurma.NomeTurma.Trim().ToUpper()== "STRING")
    //                 throw new NullReferenceException("Turma mal inserida, inseira o Turma como deve ser.");

    //             return await _ut.Turmas.Atualizar(buscarTurma);
    //         }
    //         catch (Exception ex)
    //         {
    //             throw new Exception(ex.Message);
    //         } 
    //     }
    // #endregion

    // eliminar Turma
    #region Eliminar Turma
        public async Task<bool> Eliminar(Guid id)
        {
            // Turma a ser atualizado 
            var buscarTurma =await  _ut.Turmas.BuscarPorId(id);

            if (buscarTurma == null)
            {
                throw new NullReferenceException("Turma nao encontrado, por favor cadastre.");
            }
            // verificar se o nome ja exisate
            try
            {
                await _ut.Turmas.Eliminar(id);
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
