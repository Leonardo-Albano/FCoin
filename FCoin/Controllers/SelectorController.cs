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
        public async Task<IActionResult> Selector(int? id)
        {
            dynamic selector = await _selectorManagement.GetSelector(id);

            if (selector != null)
            {
                return Ok(selector);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Selector(Selector selector)
        {
            selector = await _selectorManagement.CreateSelector(selector);

            if (selector == null)
            {
                return BadRequest();
            }

            return Ok(selector);
        }
    }
}
