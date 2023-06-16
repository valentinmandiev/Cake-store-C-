using CakeStore.BL.Interfaces;
using CakeStore.Models.Models;
using CakeStore.Models.Request.Factory;
using Microsoft.AspNetCore.Mvc;

namespace CakeStore.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FactoryController : ControllerBase
    {
        private readonly IFactoryService _service;
        private readonly ILogger<FactoryController> _logger;

        public FactoryController(IFactoryService service, ILogger<FactoryController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Factory>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAll();

                if (result != null && result.Any()) return Ok(result);

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ControllerName}: {ErrorMessage}",
                    nameof(GetAll),
                    nameof(FactoryController),
                    ex.Message);

                return StatusCode(500, "An error occurred");
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Factory))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("ById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _service.GetById(id);

                if (result != null) return Ok(result);

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ControllerName}: {ErrorMessage}",
                    nameof(GetById),
                    nameof(FactoryController),
                    ex.Message);

                return StatusCode(500, "An error occurred");
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create(AddFactoryRequest factory)
        {
            try
            {
                var result = await _service.Add(factory);

                return result is not null ? Created(nameof(this.Create), new { CakeId = result.Id }) : BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ControllerName}: {ErrorMessage}",
                    nameof(Create),
                    nameof(FactoryController),
                    ex.Message);

                return StatusCode(500, "An error occurred");
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(nameof(Update))]
        public async Task<IActionResult> Update(UpdateFactoryRequest factory)
        {
            try
            {
                var isUpdated = await _service.Update(factory);

                return isUpdated ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ControllerName}: {ErrorMessage}",
                    nameof(Update),
                    nameof(FactoryController),
                    ex.Message);

                return StatusCode(500, "An error occurred");
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete(nameof(Delete))]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var isDeleted = await _service.Delete(id);

                return isDeleted ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ControllerName}: {ErrorMessage}",
                    nameof(Delete),
                    nameof(FactoryController),
                    ex.Message);

                return StatusCode(500, "An error occurred");
            }
        }
    }
}
