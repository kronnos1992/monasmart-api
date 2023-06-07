using Claver.Aplicacao.Implemetancao;
using Claver.Dominio.DTOS.PeriodoDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Claver.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeriodoController : ControllerBase
    {
        private readonly ImplementarPeriodo _periodo;

        public PeriodoController(ImplementarPeriodo periodo)
        {
            _periodo = periodo;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarPeriodo(AddPeriodoDTO periodo)
        {
            try
            {
                await _periodo.Adicionar(periodo);
                await _periodo.Persistir();
                return Ok($"Periodo inserido.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // [HttpGet]
        // public async Task<IActionResult> BuscarPorNome(string nome)
        // {
        //     var periodos = await _periodo.ExibirPeriodo(nome.Trim().ToUpper());
        //     return Ok(periodos.ToList());
        // }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> AtualizarPeriodo(UpdatePeriodoDTO periodo)
        {   
            try
            {
                await _periodo.Atualizar(periodo);
                await _periodo.Persistir();
                return Ok("Periodo Atualizado.");
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}");
            } 
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {   
            try
            {
                var Periodos = await _periodo.ExibirTudo();
                return Ok(Periodos.ToList());
            }
            catch(Exception ex)
            {
                return NotFound($"{ex.Message}");
            } 
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> EliminarPeriodo(Guid id)
        {   
            try
            {
                await _periodo.Eliminar(id);
                await _periodo.Persistir();
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}");
            } 
        }

    }
}