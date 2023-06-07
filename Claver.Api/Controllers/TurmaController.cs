using Claver.Aplicacao.Implemetancao;
using Claver.Dominio.DTOS.TurmaDTOs;
using Claver.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Claver.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmaController : ControllerBase
    {
        private readonly ImplementarTurma _turma;

        public TurmaController(ImplementarTurma turma)
        {
            _turma = turma;
        }

    
        // [HttpPost(nameof(AdicionarTurma))]
        // public async Task<IActionResult> AdicionarTurma(AddTurmaDTO turma, Guid curso, Guid periodo)
        // {
        //     try
        //     {
        //         await _turma.Adicionar(turma, curso, periodo);
        //         await _turma.Persistir();
        //         return Ok($"Turma inserida. {turma.NomeTurma}");
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }

        [HttpGet(nameof(BuscarPorNome))]
        public async Task<IActionResult> BuscarPorNome(string nome)
        {
            var turmas = await _turma.ExibirPorNome(nome.Trim().ToUpper());
            if (turmas.Count() > 0)
            {
                return Ok(turmas.ToList());
            }
            else{
                return NotFound("Turma não encontrada");
            }
        }

        // [HttpPut(nameof(AtualizarTurma))]
        // public async Task<IActionResult> AtualizarTurma(AtualizarTurmaDTO turma)
        // {   
        //     try
        //     {
        //         await _turma.Atualizar(turma);
        //         await _turma.Persistir();
        //         return Ok("Turma Atualizada.");
        //     }
        //     catch(Exception ex)
        //     {
        //         return BadRequest($"{ex.Message}");
        //     } 
        // }

        [HttpGet(nameof(BuscarTodos))]
        public async Task<IActionResult> BuscarTodos()
        {   
            try
            {
                var turmas = await _turma.ExibirTudo();
                return Ok(turmas.ToList());
            }
            catch(Exception ex)
            {
                return NotFound($"{ex.Message}");
            } 
        }

        [HttpDelete(nameof(EliminarTurma))]
        public async Task<IActionResult> EliminarTurma(Guid id)
        {   
            try
            {
                await _turma.Eliminar(id);
                await _turma.Persistir();
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}");
            } 
        }

    }
}