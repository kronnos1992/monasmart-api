using Claver.Aplicacao.Implemetancao;
using Claver.Dominio.DTOS.AnoLectivo;
using Claver.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Claver.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnoLectivoController : ControllerBase
    {
        private readonly ImplementarAnoLectivo _anoLectivo;

        public AnoLectivoController(ImplementarAnoLectivo anoLectivo)
        {
            _anoLectivo = anoLectivo;
        }

    
        [HttpPost]
        public async Task<IActionResult> AdicionarAnoLectivo(AddAnoLectivoDTO anoLectivo)
        { 
            try
            {
                await _anoLectivo.Adicionar(anoLectivo);
                await _anoLectivo.Persistir();
                return Ok($"AnoLectivo inserido. {anoLectivo.AnoAcademico}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // [HttpGet]
        // public async Task<IActionResult> BuscarPorNome(string nome)
        // {
            
        //     try
        //     {
        //         var __anoLectivos = await _anoLectivo.ExibirPorNome(nome.Trim().ToUpper());
        //         if (__anoLectivos.Count() > 0)
        //         {
        //             return Ok(__anoLectivos.ToList());
        //         }
        //         else
        //         {
        //             return NotFound("AnoLectivo não encontrada");
        //         }
        //     }
        //     catch (System.Exception ex)
        //     {
        //         return NotFound(ex.Message);
        //     }
        // }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> AtualizarAnoLectivo(UpdateAnoLectivoDTO anoLectivo)
        {   
            try
            {
                await _anoLectivo.Atualizar(anoLectivo);
                await _anoLectivo.Persistir();
                return Ok("AnoLectivo Atualizada.");
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
                var AnoLectivos = await _anoLectivo.ExibirTudo();
                return Ok(AnoLectivos.ToList());
            }
            catch(Exception ex)
            {
                return NotFound($"{ex.Message}");
            }    
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> EliminarAnoLectivo(Guid id)
        {   
            try
            {
                await _anoLectivo.Eliminar(id);
                await _anoLectivo.Persistir();
                return Ok("AnoLectivo eliminada");
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}");
            } 
        }

    }
}