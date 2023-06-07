using Claver.Dominio.DTOS.ClasseDTOs;
using Claver.Aplicacao.Implemetancao;
using Microsoft.AspNetCore.Mvc;
namespace Claver.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClasseController : ControllerBase
{
    private readonly ImplementarClasse _classe;

    public ClasseController(ImplementarClasse classe)
    {
        _classe = classe;
    }
    
    [HttpPost]
    public async Task<IActionResult> AdicionarClasse(AddClasseDTO classeDTO)
    {
        try
        {
            await _classe.Adicionar(classeDTO);
            await _classe.Persistir();
            return Ok($"Classe inserida. {classeDTO.DescricaoClasse}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet(nameof(BuscarPorNome))]
    public async Task<IActionResult> BuscarPorNome(string nome)
    {
        var classes = await _classe.ExibirPorNome(nome.Trim().ToUpper());
        if (classes.Count() > 0)
        {
            return Ok(classes.ToList());
        }
        else
        {
            return NotFound("Classe não encontrada.");
        }
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> AtualizarClasse(AtualizarClasseDTO classe)
    {   
        try
        {
            await _classe.Atualizar(classe);
            await _classe.Persistir();
            return Ok("Classe Atualizada.");
        }
        catch(Exception ex)
        {
            return BadRequest($"{ex.Message}");
        } 
    }

    [HttpGet]
    public async Task<IActionResult> BuscarTodas()  
    {   
         try
        {
            var classes = await _classe.ExibirTudo();
            return Ok(classes.ToList());
        }
        catch(Exception ex)
        {
            return NotFound($"{ex.Message}");
        } 
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> EliminarClasse(Guid id)
    {   
         try
        {
            await _classe.Eliminar(id);
            await _classe.Persistir();
            return NoContent();
        }
        catch(Exception ex)
        {
            return BadRequest($"{ex.Message}");
        } 
    }
}