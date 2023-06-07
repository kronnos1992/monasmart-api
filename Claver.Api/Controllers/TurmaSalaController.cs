using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Claver.Aplicacao.Implemetancao;
using Claver.Dominio.DTOS.TurmaSalaDTO;
using Claver.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Claver.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmaSalaController : ControllerBase
    {
         private readonly ImplementarTurmaSala _turmasala;

        public TurmaSalaController(ImplementarTurmaSala turmaSala)
        {
            _turmasala = turmaSala;
        }

    
        [HttpPost(nameof(Inserir))]
        public async Task<IActionResult> Inserir(Guid sala, Guid turma)
        {
            try
            {
                await _turmasala.Associar(sala, turma);
                await _turmasala.Persistir();
                return Ok($"Registro inserido.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut(nameof(Atualizar))]
        public async Task<IActionResult> Atualizar(TurmaSala turmaSala,Guid id)
        {   
            try
            {
                await _turmasala.Atualizar(turmaSala,id);
                await _turmasala.Persistir();
                return Ok("Registro Atualizado.");
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
                var TurmaSalas = await _turmasala.ExibirTudo();
                return Ok(TurmaSalas.ToList());
            }
            catch(Exception ex)
            {
                return NotFound($"{ex.Message}");
            } 
        }

        [HttpDelete(nameof(Eliminar))]
        public async Task<IActionResult> Eliminar(Guid id)
        {   
            try
            {
                await _turmasala.Eliminar(id);
                await _turmasala.Persistir();
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}");
            } 
        }

    }
}