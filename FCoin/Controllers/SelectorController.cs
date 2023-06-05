using FCoin.Business;
using FCoin.Business.Interfaces;
using FCoin.Models;
using Microsoft.AspNetCore.Mvc;

namespace FCoin.Controllers
{
    [ApiController]
    [Route("seletor")]
    public class SelectorController : ControllerBase
    {
        private readonly ISelectorManagement _selectorManagement;

        public SelectorController(ISelectorManagement selectorManagement)
        {
            _selectorManagement = selectorManagement;
        }

        [HttpGet]
        public async Task<IActionResult> GetSelector(int? id)
        {
            dynamic selector = await _selectorManagement.GetSelector(id);

            if (selector != null)
            {
                return Ok(selector);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSelector(Selector selector)
        {
            selector = await _selectorManagement.CreateSelector(selector);

            if (selector == null)
            {
                return BadRequest();
            }

            return Ok(selector);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSelector(Selector selector)
        {
            var result = await _selectorManagement.UpdateSelector(selector);

            if (result is Client)
            {
                return Ok(result);
            }
            else if (result is Dictionary<dynamic, dynamic>)
            {
                return StatusCode(405, "Method Not Allowed");
            }

            return BadRequest(result);
        }
    }
}
