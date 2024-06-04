using Microsoft.AspNetCore.Mvc;

using ZdzTest_Models;
using ZdzTest_Services;

namespace ZdzTest_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {            
            try
            {
                var activity = await _activityService.GetByIdAsync(id);
                if (activity == null) return NotFound();
                return Ok(activity);
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
                var activities = await _activityService.GetAllAsync();
                return Ok(activities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Activity activity)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                await _activityService.AddAsync(activity);
                return CreatedAtAction(nameof(GetById), new { id = activity.Id }, activity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Activity activity)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                if (id != activity.Id) return BadRequest();
                await _activityService.UpdateAsync(activity);
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
                await _activityService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }

}
