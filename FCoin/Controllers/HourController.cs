using FCoin.Business;
using FCoin.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FCoin.Controllers
{
    [ApiController]
    [Route("hora")]
    public class HourController : ControllerBase
    {
        private readonly IHourManagement _hourManagement;

        public HourController(IHourManagement hourManagement)
        {
            _hourManagement = hourManagement;
        }

        [HttpGet]
        public async Task<IActionResult> GetHour() 
        {
            var hour = await _hourManagement.GetHour();

            if (hour != new DateTime())
            {
                return Ok(hour);
            }

            return NotFound();
        }
    }
}
