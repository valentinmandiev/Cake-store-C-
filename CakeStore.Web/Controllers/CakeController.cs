using CakeStore.BL.Interfaces;
using CakeStore.Models.Models;
using CakeStore.Models.Request.Cake;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CakeStore.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CakeController : ControllerBase
    {
        private readonly ICakeService _service;
        private readonly ILogger<CakeController> _logger;

        public CakeController(ICakeService service, ILogger<CakeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Cake>))]
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
                    nameof(CakeController),
                    ex.Message);

                return StatusCode(500, "An error occurred");
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cake))]
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
                    nameof(CakeController),
                    ex.Message);

                return StatusCode(500, "An error occurred");
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create(AddCakeRequest addCakeReaquest)
        {
            try
            {
                var result = await _service.Add(addCakeReaquest);

                return result is not null ? Created(nameof(this.Create), new { CakeId = result.Id }) : BadRequest("Factory does not exists!");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ControllerName}: {ErrorMessage}",
                    nameof(Create),
                    nameof(CakeController),
                    ex.Message);

                return StatusCode(500, "An error occurred");
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(nameof(Update))]
        public async Task<IActionResult> Update(UpdateCakeRequest updateCakeRequest)
        {
            try
            {
                var isUpdated = await _service.Update(updateCakeRequest);

                return isUpdated ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ControllerName}: {ErrorMessage}",
                    nameof(Update),
                    nameof(CakeController),
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
                    nameof(CakeController),
                    ex.Message);

                return StatusCode(500, "An error occurred");
            }
        }
    }
}
