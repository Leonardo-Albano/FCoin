using FCoin.Business.Interfaces;
using FCoin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<IActionResult> CreateSelector(string ip, string nome)
        {
            Selector selector = new()
            {
                Ip = ip,
                Nome = nome
            };

            selector = await _selectorManagement.CreateSelector(selector);

            if (selector == null)
            {
                return BadRequest();
            }

            return Ok(selector);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSelector(int id, string nome, string ip)
        {
            Selector selector = new()
            {
                Ip = ip,
                Nome = nome
            };

            var result = await _selectorManagement.UpdateSelector(id, selector);

            if (result is Selector)
            {
                return Ok(result);
            }
            //else if (result is Dictionary<dynamic, dynamic>)
            //{
            //    return StatusCode(405, "Method Not Allowed");
            //}

            return BadRequest(result);
        }

        [HttpPost("SelectValidators")]
        public async Task<IActionResult> SelectValidators(int selectorId, int transactionId)
        {
            var result = await _selectorManagement.SelectValidators(selectorId, transactionId);

            if(!result.IsNullOrEmpty())
            {
                return Ok (result);
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSelector(int id)
        {
            var result = await _selectorManagement.DeleteSelector(id);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
