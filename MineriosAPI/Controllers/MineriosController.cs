using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MineriosAPI.Data;
using MineriosAPI.Models;
using MineriosAPI.DTOs;
using MineriosAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace MineriosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MineriosController : ControllerBase
    {
        private readonly IServiceMinerio _serviceMinerio;
        private readonly ILogger _logger;

        public MineriosController(IServiceMinerio serviceMinerio, ILogger<MineriosController> logger)
        {
            _serviceMinerio = serviceMinerio;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("infos")]
        public IActionResult GetInfo() 
        {
            var userAgent = Request.Headers.UserAgent.ToString();

            if(string.IsNullOrEmpty(userAgent))
                return NoContent();

            _logger.LogInformation($"Método ativado: GetInfo - User-Agent recebido: {userAgent}");
            return Ok($"User-Agent recebido: {userAgent}");
        }

        [HttpGet("minerios")]
        public async Task<IActionResult> GetAllMinerios() 
        {
            try
            {
                var minerios = await _serviceMinerio.GetAllMinerios();

                if (!minerios.Any())
                {
                    return NoContent();
                }

                _logger.LogInformation($"Método ativado: GetAllMinerios - Dados recebidos {minerios.Count()} minerios cadastrados");
                return Ok(minerios);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        [HttpGet("minerio/{id}")]
        public async Task<IActionResult> GetMinerioById([FromRoute] int id)
        {
            try
            {
                var minerio = await _serviceMinerio.GetMinerioById(id);

                _logger.LogInformation($"Método ativado: GetMineriosById - Dados recebidos {minerio.id} - {minerio.nome} - {minerio.estado} - {minerio.descricao}");
                return Ok(minerio);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Nenhum produto foi encontrado com o ID: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Erro interno no servidor.");
            }
        }
        
        [HttpPut("minerio/{id}")]
        public async Task<IActionResult> UpdateMinerio([FromRoute] int id, [FromBody] UpdateMinerioDto updateMinerio) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                var minerio = await _serviceMinerio.UpdateMinerio(id, updateMinerio);

                _logger.LogInformation($"Método ativado: UpdateMinerio - Dados atualizados {minerio.nome} - {minerio.estado} - {minerio.descricao}");
                return Ok(minerio);
            }
            catch (KeyNotFoundException) 
            {
                return NotFound($"Nenhum produto foi encontrado com o ID: {id}");
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        [HttpPost("minerio")]
        public async Task<IActionResult> CreateMinerio([FromBody] CreateMinerioDto newMinerio) 
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try 
            {
                var novoMinerio = await _serviceMinerio.CreateMinerio(newMinerio);

                _logger.LogInformation($"Método ativado: CreateMinerio - Minerio criado: {novoMinerio.id} - {novoMinerio.nome}");
                return CreatedAtAction(nameof(GetMinerioById), new {id = novoMinerio.id}, novoMinerio);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        [HttpDelete("minerio/{id}")]
        public async Task<IActionResult> DeleteMinerio([FromRoute] int id)
        {
            try
            {
                var minerio = await _serviceMinerio.DeleteMinerio(id);

                _logger.LogInformation($"Método ativado: DeleteMinerio - Minerio ");
                return Ok($"O produto com o ID: {minerio.id} foi excluído.");
            }
            catch (KeyNotFoundException) 
            {
                return NotFound($"Nenhum produto encontrado com o ID: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        [HttpDelete("minerios")]
        public async Task<IActionResult> DeleteAllMinerios() 
        {
            try
            {
                await _serviceMinerio.DeleteAllMinerios();

                return Ok("Todos os minerios foram excluídos.");
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex,$" Erro interno no servidor");
                return StatusCode(500, "Erro interno no servidor");
            }
        }
    }
}
