
using Claver.Aplicacao.Implemetancao;
using Claver.Dominio.DTOS.CursoDTOs;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
namespace Claver.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CursoController : ControllerBase
{
    private readonly ImplementarCurso _curso;
    private readonly IValidator<AddCursoDTO> _addValidator;
    private readonly IValidator<AtualizarCursoDTO> _updateValidator;

    public CursoController(ImplementarCurso curso, IValidator<AddCursoDTO> addValidator, IValidator<AtualizarCursoDTO> updateValidator)
    {
        _addValidator = addValidator;
        _updateValidator = updateValidator;
        _curso = curso;
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {  
        try
        {
            var cursos = await _curso.GetAsync();
            return Ok(cursos.ToList());
        }
        catch(Exception ex)
        {
            return NotFound($"{ex.Message}");
        }  
        
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {  
        try
        {
            var curso = await _curso.GetAsync(id);
            return Ok(curso);
        }
        catch(Exception ex)
        {
            return NotFound($"{ex.Message}");
        }  
        
    }

    // [Route("getname")]
    // [HttpGet]
    // public async Task<IActionResult> GetName(string value)
    // {  
    //     try
    //     {
    //         var curso = await _curso.Listar(value);
    //         return Ok(curso);
    //     }
    //     catch(Exception ex)
    //     {
    //         return NotFound($"{ex.Message}");
    //     }  
        
    // }

    [HttpPost]
    public async Task<IActionResult> Insert(AddCursoDTO cursoDTO)
    {
        try
        {
            ValidationResult validationResult = await _addValidator.ValidateAsync(cursoDTO);
            if (!validationResult.IsValid)
            {
                foreach (var fail in validationResult.Errors)
                {
                    throw new  ValidationException("Codigo: " + fail.ErrorCode + 
                    "\n" +  "Campo: " + fail.PropertyName +
                    "\n" +  "Erro: " + fail.ErrorMessage);
                }
                
            }
            await _curso.InsertAsync(cursoDTO);
            return Ok($"Curso: {cursoDTO.NomeCurso}  inserido com sucesso");
        }
        catch (Exception ex)
        {
            return BadRequest( ex.Message);
        }
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update([FromBody]AtualizarCursoDTO curso)
    {   
        try
        {
            ValidationResult validationResult = await _updateValidator.ValidateAsync(curso);
            if (!validationResult.IsValid)
            {
                foreach (var fail in validationResult.Errors)
                {
                    throw new  ValidationException("Codigo: " + fail.ErrorCode + 
                    "\n" +  "Campo: " + fail.PropertyName +
                    "\n" +  "Erro: " + fail.ErrorMessage);
                }
                
            }
            await _curso.Atualizar(curso);
            return Ok($"Curso Atualizado, dados: {curso}");
        }
        catch(Exception ex)
        {
            return BadRequest($"{ex.Message}");
        } 
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> EliminarCurso(Guid id)
    {   
         try
        {
            await _curso.Eliminar(id);
            return Ok($"Curso Deletado, id: {id}");
        }
        catch(Exception ex)
        {
            return BadRequest($"{ex.Message}");
        } 
    }
}