using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Claver.Aplicacao.Implemetancao;
using Claver.Dominio.DTOS.DisciplinaDTO;
using Claver.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Claver.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinaController : ControllerBase
    {
        private readonly ImplementarDisciplina _Disciplina;

        public DisciplinaController(ImplementarDisciplina Disciplina)
        {
            _Disciplina = Disciplina;
        }

    
        [HttpPost]
        public async Task<IActionResult> AdicionarDisciplina(AddDisciplinaDTO disciplina)
        {
            try
            {
                await _Disciplina.Adicionar(disciplina);
                await _Disciplina.Persistir();
                return Ok($"Disciplina inserido. {disciplina.Descricao}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(nameof(BuscarPorNome))]
        public async Task<IActionResult> BuscarPorNome(string nome)
        {
            
            try
            {
                var disciplinas = await _Disciplina.ExibirPorNome(nome.Trim().ToUpper());
                if (disciplinas.Count() > 0)
                {
                    return Ok(disciplinas.ToList());
                }
                else
                {
                    return NotFound("Disciplina não encontrada");
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> AtualizarDisciplina(AtualizarDisciplinaDTO Disciplina, Guid id)
        {   
            try
            {
                await _Disciplina.Atualizar(Disciplina);
                await _Disciplina.Persistir();
                return Ok("Disciplina Atualizada.");
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
                var disciplinas = await _Disciplina.ExibirTudo();
                return Ok(disciplinas.ToList());
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            } 
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> EliminarDisciplina(Guid id)
        {   
            try
            {
                await _Disciplina.Eliminar(id);
                await _Disciplina.Persistir();
                return Ok("Disciplina eliminada");
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}");
            } 
        }

    }
}