using Microsoft.AspNetCore.Mvc;

using ZdzTest_Models;
using ZdzTest_Services;

namespace ZdzTest_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloperService _developerService;

        public DeveloperController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var developer = await _developerService.GetByIdAsync(id);
                if (developer == null) return NotFound();
                return Ok(developer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var developers = await _developerService.GetAllAsync();
                return Ok(developers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Developer developer)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                await _developerService.AddAsync(developer);
                return CreatedAtAction(nameof(GetById), new { id = developer.Id }, developer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Developer developer)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                if (id != developer.Id) return BadRequest();
                await _developerService.UpdateAsync(developer);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _developerService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }

}
