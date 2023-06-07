using AutoMapper;
using Claver.Aplicacao.Implemetancao;
using Claver.Dominio.DTOS.InscricaoDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Claver.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InscricaoController : ControllerBase
    {
        private readonly ILogger<InscricaoController> _logger;
        private ImplementarInscricao _inscricao;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env; 

        public InscricaoController(ILogger<InscricaoController> 
            logger, ImplementarInscricao inscricao, IMapper mapper, IWebHostEnvironment env)
        {
            _logger = logger;
            _inscricao = inscricao;
            _mapper = mapper;
            _env = env;
        }

        // GET: api/inscricao
        [HttpGet
        ]
        public async Task<IActionResult> Get()
        {
            try
            {
                var candidato =await _inscricao.Listar();
                return Ok(candidato);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/inscricao/{data1, data2}
        [HttpGet (nameof(GetByInterval))]
        public async Task<IActionResult> GetByInterval(DateTime data1, DateTime data2)
        {
            try
            {
                var candidato =await _inscricao.Listar(data1, data2);                
                return Ok(candidato);
            }
            catch (Exception ex)
            {
                return NotFound($"{ex.Message}");
            }
        }

        // GET: api/inscricao/{id}
        [HttpGet(nameof(GetById))]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var candidato =await _inscricao.Listar(id);
                if (candidato == null)
                    return NotFound("Sem registro");
                
                
                return Ok(candidato);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao se conectar com o banco: {ex.Message}");
            }
        }
    
        // POST: api/inscricao
        [HttpPost]
        public async Task<IActionResult> AdicionarCandidato([FromForm]AddCandidatoDTO candidato)
        {
            try
            {
                await _inscricao.Adicionar(
                    candidato, candidato.ClasseId, 
                    candidato.CursoId, candidato.AnoLEctivoId, 
                    candidato.PeriodoId); 

                // return CreatedAtAction(nameof(Listar), new {Nome = candidato.NomeCompleto }, candidato);
                return Ok($"Nome Completo: {candidato.NomeCompleto}\nBilhete: {candidato.NumeroBilhete}");      
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro ao inserir candidato: {ex.Message}");
            }
        }

        //PUT: api/inscricao/validar
        [HttpPut]
        [Route("validar")]
        public async Task<IActionResult> ValidarCandidatura([FromBody]AdmitirCandidatoDTO candidato)
        {
            try
            {
                // var uri = Url.Action("VerificarStatusAtualizacao", new { id = candidato });
                await _inscricao.ValidarCandidatura(candidato);
                return Accepted($"Candidatura finalizada com sucesso.{0}, Candidato: {candidato } ");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao finalizar a candidatura: {ex.Message}");
            }
        }
        
    }
}   