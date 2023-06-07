using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Claver.Aplicacao.Implemetancao;
using Claver.Dominio.DTOS.SalaDTOs;
using Claver.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Claver.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaController : ControllerBase
    {
        private readonly ImplementarSala _sala;

        public SalaController(ImplementarSala sala)
        {
            _sala = sala;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodas()
        {
            try
            {
                var salas = await _sala.ExibirTudo();
                return Ok(salas.ToList());
            }
            catch (Exception ex)
            {
                return NotFound($"{ex.Message}");
            }
        }

        [HttpGet(nameof(BuscarPorNome))]
        public async Task<IActionResult> BuscarPorNome(string nome)
        {
            try
            {
                var salas = await _sala.ExibirPorSala(nome.Trim().ToUpper());
                if (salas.Count() > 0)
                {
                    return Ok(salas.ToList());
                }
                else
                {
                    return NotFound("sala não encontrada");
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            };
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarSala(AddSalaDTO sala)
        {
            try
            {
                await _sala.Adicionar(sala);
                await _sala.Persistir();
                return Ok($"Sala inserida. {sala.NumeroSala}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> AtualizarSala(AtualizarSalaDTO sala)
        {   
            try
            {
                await _sala.Atualizar(sala);
                await _sala.Persistir();
                return Ok("Sala Atualizada.");
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}");
            } 
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> EliminarSala(Guid id)
        {   
            try
            {
                await _sala.Eliminar(id);
                await _sala.Persistir();
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}");
            } 
        }

    }
}