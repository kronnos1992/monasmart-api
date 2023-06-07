using Claver.Dominio.DTOS.CursoDTOs;
using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.Entidades;
using AutoMapper;
using FluentValidation;

namespace Claver.Aplicacao.Implemetancao;

public class ImplementarCurso
{
    private readonly IUnidadeTrabalho _ut;
    private readonly IMapper _mapper;
    // variaveis necessarias
    DateTime _dataAtual = DateTime.Now;
    public ImplementarCurso( IUnidadeTrabalho ut, IMapper mapper)
    {
        _ut = ut;
        _mapper = mapper;
    }

    // inserir novo curso
    #region Inserir Curso   
        public async Task<Curso> InsertAsync(AddCursoDTO cursoDTO)
        {
            try
                {
                            
                    var _obCurso = _mapper.Map<Curso>(cursoDTO);  
                    _obCurso.DataRegistro = _dataAtual;        
                    // cursoDTO = _mapper.Map<AddCursoDTO>(_obCurso);

                    await _ut.Cursos.Inserir(_obCurso);
                    await _ut.Finalizar();
                    await _ut.Dispose();
                    return _obCurso;
                }
            catch (System.Exception e)
                { 
                    throw new Exception(e.Message);
                }  
        
        }
  
    #endregion

    // listar curso por id
    #region Listar(Id)
        public async Task<GetCursoDTO> GetAsync(Guid id)
        {
            var curso = await _ut.Cursos.Listar(id);
            if (curso == null)
            {
                return null;
            }
            var cursoDto = _mapper.Map<GetCursoDTO>(curso);
            return cursoDto;
        }
    #endregion

     // listar todos os cursos
    #region Listar
        public async Task<IEnumerable<GetCursoDTO>> GetAsync()
        {
            var curso = await _ut.Cursos.Listar();
            if (curso == null)
            {
                return null;
            }
            var cursoDto = _mapper.Map<List<GetCursoDTO>>(curso);
            return cursoDto;
        }
    #endregion
    
    // listar curso por nome
    #region Exibir por nome 
        public async Task<IEnumerable<GetCursoDTO>> Listar(string nome)
        {
            
            var curso = await _ut.Cursos.Listar(nome);
            var cursoDto = _mapper.Map<List<GetCursoDTO>>(curso);
            await _ut.Dispose();
            return cursoDto.ToList();

        }        
    #endregion

    // Atualizar curso
    #region Atualizar Curso
        public async Task<Curso> Atualizar(AtualizarCursoDTO curso)
        {

            // curso a ser atualizado 
            var buscarCurso =await  _ut.Cursos.Listar(curso.Id);            
            if (buscarCurso == null)
            {
                throw new NullReferenceException("Curso não encontrado.");
            }
            try
                {
                    buscarCurso.DataRevisao = _dataAtual;
                    var _curso = _mapper.Map(curso, buscarCurso);

                    await _ut.Cursos.Atualizar(_curso);
                    await _ut.Finalizar();
                    await _ut.Dispose();
                    return _curso;
                }
            catch (System.Exception e)
                { 
                    throw new Exception(e.Message + e.Source);
                } 
        }
    #endregion

    #region Eliminar Curso
        public async Task<bool> Eliminar(Guid id)
        {

            var buscarCurso =await  _ut.Cursos.Listar(id);

            if (buscarCurso == null)
            {
                throw new NullReferenceException("Curso nao encontrado.");
            }

            try
            {
                await _ut.Cursos.Eliminar(id);
                await _ut.Finalizar();
                await _ut.Dispose();
                return true;
            }
            catch (System.Exception ex)
            {
                throw new NullReferenceException(ex.Message);
            } 
        }
    #endregion 

}
