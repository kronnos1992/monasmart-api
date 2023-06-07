using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Claver.Aplicacao.Implemetancao;
using Claver.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Claver.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinaCursoController : ControllerBase
    {
        private readonly ImplementarCursoDisciplina _periodificar;

        public DisciplinaCursoController(ImplementarCursoDisciplina periodificar)
        {
            _periodificar = periodificar;
        }

    
        // [HttpPost(nameof(InserirDisciplinaEmCurso))]
        // public async Task<IActionResult> InserirDisciplinaEmCurso(CursoDisciplina cursoDisciplina, Guid curso, Guid periodo, Guid disciplina)
        // {
        //     try
        //     {
        //         await _periodificar.Adicionar(cursoDisciplina, curso, disciplina, periodo);
        //         await _periodificar.Persistir();
        //         return Ok($"Escala inserido.");
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }

        [HttpPut(nameof(Atualizar))]
        public async Task<IActionResult> Atualizar(CursoDisciplina cursoDisciplina, Guid id)
        {   
            try
            {
                await _periodificar.Atualizar(cursoDisciplina);
                await _periodificar.Persistir();
                return Ok("Escala Atualizado.");
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}");
            } 
        }

        [HttpGet(nameof(BuscarTodos))]
        public async Task<IActionResult> BuscarTodos()
        {   
            try
            {
                var CursoDisciplinas = await _periodificar.ExibirTudo();
                return Ok(CursoDisciplinas.ToList());
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}");
            } 
        }

        [HttpDelete(nameof(EliminarEscala))]
        public async Task<IActionResult> EliminarEscala(Guid id)
        {   
            try
            {
                await _periodificar.Eliminar(id);
                await _periodificar.Persistir();
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}");
            } 
        }

    }
}