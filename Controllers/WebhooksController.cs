
using System.Text.Json.Serialization;
using EjemploWebhookBioengine.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace EjemploWebhookBioengine
{
    /// <summary>
    /// Controlador para recibir notificaciones de bioEngine vía webhook
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/[controller]")]
    public class WebhooksController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
     

        /// <summary>
        /// Constructor default
        /// </summary>
        /// <param name="logger"></param>
        public WebhooksController(Serilog.ILogger logger)
        {
            _logger = logger;

        }

        /// <summary>
        /// Recibe las actualizaciones cuando se ejecuta una operación IdentifyAsync
        /// </summary>
        /// <param name="filtro">DTO con criterios de búsqueda</param>
        /// <returns>Regresa un ID para darle seguimiento a la operación.</returns>
        /// <response code="200">En caso de que el job se ejecute correctamente.</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpPost("Resultado")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(String))]
        [ProducesResponseType(StatusCodes.Status204NoContent,Type = typeof(String))]
        [ProducesResponseType(StatusCodes.Status409Conflict,Type = typeof(String))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError,Type = typeof(String))]
        public async  Task<IActionResult> Resultado([FromBody] OperacionDto dto)
        {
            try
            {
               _logger.Debug(JsonConvert.SerializeObject(dto));
                //Insertar lógica de negocio
                return Ok("PROCESADO");
            }
            catch (Exception exc)
            {
                _logger.Error(exc,exc.Message);
                return StatusCode(500, exc.Message);
            }
        }
        
        /// <summary>
        /// Recibe las actualizaciones cuando hay un hit. Prácticamente va a recibir la misma
        /// notificación que el webhook  EnrollWebhook, pero se puede usar  para procesar
        /// otro tipo de alertas.
        /// </summary>
        /// <param name="filtro">DTO con criterios de búsqueda</param>
        /// <returns>Regresa un ID para darle seguimiento a la operación.</returns>
        /// <response code="200">En caso de que el job se ejecute correctamente.</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpPost("HitWebhook")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(String))]
        [ProducesResponseType(StatusCodes.Status204NoContent,Type = typeof(String))]
        [ProducesResponseType(StatusCodes.Status409Conflict,Type = typeof(String))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError,Type = typeof(String))]
        public async  Task<IActionResult> Hit([FromBody] OperacionDto dto)
        {
            try
            {
                _logger.Debug(JsonConvert.SerializeObject(dto));
                //Insertar lógica de negocio
                return Ok("PROCESADO");
            }
            catch (Exception exc)
            {
                _logger.Error(exc,exc.Message);
                return StatusCode(500, exc.Message);
            }
        }


       
    }
}