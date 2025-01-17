using ArsHistoriaAPI.Models;
using ArsHistoriaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArsHistoriaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StyleController : ControllerBase
    {
        private readonly ILogger<StyleController> _logger;
        private readonly IStyleService _service;

        public StyleController(ILogger<StyleController> logger, IStyleService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("Styles")]
        public IActionResult GetStyles()
        {
            try
            {
                var styles = _service.GetStyles();
                if (styles == null || !styles.Any())
                {
                    return NotFound("No styles found.");
                }
                return Ok(styles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching styles.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetStyleById(int id)
        {
            try
            {
                var style = _service.GetStyleById(id);
                if (style == null)
                {
                    return NotFound($"Style with ID {id} not found.");
                }
                return Ok(style);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching style with ID {StyleId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("ByName/{name}")]
        public IActionResult GetStyleByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return BadRequest("Name parameter cannot be null or empty.");
                }

                var style = _service.GetStyleByName(name);
                if (style == null)
                {
                    return NotFound($"Style with NAME '{name}' not found.");
                }
                return Ok(style);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching style with NAME {StyleName}.", name);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateStyle([FromBody] Style style)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdStyle = _service.CreateStyle(style);

                if (createdStyle == null)
                {
                    return StatusCode(500, "Failed to create style.");
                }

                return CreatedAtAction(nameof(_service.GetStyleById), new { id = createdStyle.Id }, createdStyle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating style.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStyle([FromBody] Style style)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedStyle = _service.UpdateStyle(style);
                if (updatedStyle == null)
                {
                    return NotFound($"Style with ID {style.Id} not found.");
                }

                return Ok(updatedStyle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating style.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStyle(int id)
        {
            try
            {
                var style = _service.GetStyleById(id);
                if (style == null)
                {
                    return NotFound($"Style with ID {id} not found.");
                }

                _service.DeleteStyle(style);
                return Ok(new { message = $"Style with ID {id} was successfully deleted." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting style with ID {StyleId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}