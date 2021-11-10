using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Teste.Application.DTOs;
using Teste.Application.Interfaces;
using Teste.Domain.Entities;

namespace Teste.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BaseController<Entidade, EntidadeDTO> : Controller
         where Entidade : BaseEntitie
         where EntidadeDTO : BaseDTO
    {
        private readonly ILogger<ControllerBase> logger;
        readonly protected IBaseApp<Entidade, EntidadeDTO> app;

        public BaseController(IBaseApp<Entidade, EntidadeDTO> app, ILogger<ControllerBase> logger)
        {
            this.app = app;
            this.logger = logger;
        }

        /// <summary>
        /// Retorna uma lista de registros
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public virtual IActionResult GetAll()
        {
            try
            {
                var obj = app.GetAll();
                return new OkObjectResult(obj);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

/// <summary>
        /// Retorna o registro selecionado pelo identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Object</returns>
        [HttpGet]
        [Route("{id}")]
        public virtual IActionResult GetId(int id)
        {
            try
            {
                var obj = app.GetId(id);
                return new OkObjectResult(obj);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

/// <summary>
        /// Inclui novos registros e retorna o identificador
        /// </summary>
        /// <param name="entidadeDTO"></param>
        /// <returns>int</returns>
        /// <response code="200">Recurso criado com sucesso</response>
        /// <response code="400">Requisição mal formatada</response>
        /// <response code="401">Usuário não autenticado ou autenticação inválida</response>
        /// <response code="403">Usuário não tem permissão de acesso ao recurso</response>
        /// <response code="422">Erro(s) de validação da camada de negócio</response>
        /// <response code="403">Usuário não tem permissão de acesso ao recurso</response>
        /// <response code="500">Erro interno no servidor</response>
        /// <response code="503">Serviço indisponível</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [HttpPost]
        public virtual IActionResult Create([FromBody] EntidadeDTO entidadeDTO)
        {
            try
            {
                return new OkObjectResult(app.Create(entidadeDTO));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Faz a alteração dos registros
        /// </summary>
        /// <param name="entidadeDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public virtual IActionResult Edit([FromBody] EntidadeDTO entidadeDTO)
        {
            try
            {
                app.Edit(entidadeDTO);
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui fisicamente o registro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public virtual IActionResult Delete(int id)
        {
            try
            {
                app.Delete(id);
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
