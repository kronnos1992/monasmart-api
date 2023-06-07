using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Claver.Aplicacao.Implemetancao;
using Claver.Dominio.DTOS.AlunoDTO;
using Claver.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Claver.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatriculaController : ControllerBase
    {
        private readonly ILogger<MatriculaController> _logger;
        private ImplementarMatricula _matricula;
        private readonly IMapper _mapper;

        public MatriculaController(ILogger<MatriculaController> 
            logger, ImplementarMatricula Matricula, IMapper mapper)
        {
            _logger = logger;
            _matricula = Matricula;
            _mapper = mapper;
        }

        // POST: api/matricula/Adicionar
        [HttpPost(nameof(Matricular))]
        public async Task<IActionResult> Matricular(AddAlunoDTO matricula, Guid turma, Guid candidato, Guid anoLectivo)
        {
            try
            {
                await _matricula.Matricular(matricula,candidato, anoLectivo,turma);
                await _matricula.Persistir();
                
                return Created(matricula.NumeroCartao, matricula.NumeroProcesso);
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro ao inserir matricula: {ex.Message}");
            }
        }
        
        // PUT: api/matricula/Atualizarmatricula
        // [HttpPut(nameof(Admitirmatricula))]
        // public async Task<IActionResult> Admitirmatricula(AdmitirmatriculaDTO matricula, Guid id)
        // {   
        //     try
        //     {
        //         await _matricula.Admitir(matricula, id);
        //         await _matricula.Persistir();
        //         return Ok("matricula Admitido.");
        //     }
        //     catch(Exception ex)
        //     {
        //         return BadRequest($"{ex.Message}");
        //     } 
        // }
        // PUT: api/matricula/Atualizarmatricula
        // [HttpPut(nameof(Atualizarmatricula))]
        // public async Task<IActionResult> Atualizarmatricula(AtualizarmatriculaDTO matricula, Guid id)
        // {   
        //     try
        //     {
        //         await _matricula.Atualizar(matricula, id);
        //         await _matricula.Persistir();
        //         return Ok("matricula Atualizado.");
        //     }
        //     catch(Exception ex)
        //     {
        //         return BadRequest($"{ex.Message}");
        //     } 
        // }
        
        // // GET: api/matricula/BuscarTodos
        // [HttpGet(nameof(BuscarTodosmatriculas))]
        // public async Task<IActionResult> BuscarTodosmatriculas()
        // {   
        //     try
        //     {
        //         var matriculas = await _matricula.ExibirTudo();
        //         return Ok(matriculas.ToList());
        //     }
        //     catch(Exception ex)
        //     {
        //         return NotFound($"{ex.Message}");
        //     } 
        // }
        
        // // GET: api/matricula/bilhete
        // [HttpGet(nameof(BuscarPorId))]
        // public async Task<IActionResult> BuscarPorId(Guid id)
        // {
        //     var matricula = await _matricula.ExibirPoId(id);
        //     return Ok(matricula);
        // }
        
        // // GET: api/matricula/rupe
        //  [HttpGet(nameof(BuscarPorRupe))]
        //   public async Task<IActionResult> BuscarPorRupe(string rupe)
        //   {
        //     try
        //      {
        //         var matricula = await _matricula.ExibirPorRupe(rupe.Trim().ToUpper());
        //         return Ok(matricula);
        //      }
        //      catch(Exception ex)
        //      {
        //         return NotFound($"{ex.Message}");
        //      } 
        // }

        // // GET: api/matricula/bilhete
        // [HttpGet(nameof(BuscarPorBilhete))]
        // public async Task<IActionResult> BuscarPorBilhete(string bilhete)
        // {
        //     try
        //     {
        //        var matricula = await _matricula.ExibirPorBilhete(bilhete.Trim().ToUpper());
        //        return Ok(matricula);
        //     }
        //     catch(Exception ex)
        //     {
        //        return NotFound($"{ex.Message}");
        //     } 
        // }

        // //  GET: api/matricula/candidato
        // [HttpGet(nameof(BuscarPorcandidato))]
        // public async Task<IActionResult> BuscarPorcandidato(Guid candidato)
        // {
        //     try
        //     {
        //        var matricula = await _matricula.ExibirPorcandidato(candidato);
        //         return Ok(matricula);
        //     }
        //     catch(Exception ex)
        //     {
        //        return NotFound($"{ex.Message}");
        //     } 
        // }

        // // GET: api/matricula/turma
        // [HttpGet(nameof(BuscarPorturma))]
        // public async Task<IActionResult> BuscarPorturma(Guid turma)
        // {
        //     var matricula = await _matricula.ExibirPorturma(turma);
        //     return Ok(matricula);
        // }

        // // GET: api/matricula/buscarAdmitidos
        // [HttpGet(nameof(BuscarAdmitidos))]
        // public async Task<IActionResult> BuscarAdmitidos(Guid candidato, Guid turma)
        // {
        //     try
        //     {
        //         var matricula = await _matricula.ExibirAdmitidos(candidato, turma);
        //         return Ok(matricula);
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception(ex.Message);
        //     }
        // }        
        
        // // GET: api/matricula/Eliminar
        // [HttpDelete(nameof(Eliminarmatricula))]
        // public async Task<IActionResult> Eliminarmatricula(Guid id)
        // {   
        //     try
        //     {
        //         await _matricula.Eliminar(id);
        //         await _matricula.Persistir();
        //         return NoContent();
        //     }
        //     catch(Exception ex)
        //     {
        //         return BadRequest($"{ex.Message}");
        //     } 
        // }
        private IActionResult Created( string cartao, string processo)
        {
            return StatusCode(201, $"Aluno matriculado. Cartão Nº{cartao}, Processo Nº{processo}");
        }
    }
}