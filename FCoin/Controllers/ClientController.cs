using FCoin.Business.Interfaces;
using FCoin.Models;
using Microsoft.AspNetCore.Mvc;

namespace FCoin.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClientController : ControllerBase
    {
        private readonly IClientManagement _clientManagement;

        public ClientController(IClientManagement clientManagement)
        {
            _clientManagement = clientManagement;
        }

        [HttpGet]
        public async Task<IActionResult> GetClient(int? id)
        {
            dynamic client = await _clientManagement.GetClient(id);

            if(client != null)
            {
                return Ok(client);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(Client client)
        {
            client = await _clientManagement.CreateClient(client);

            if (client == null)
            {
                return BadRequest();
            }

            return Ok(client);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient(int id, int qtdMoeda)
        {
            var result = await _clientManagement.UpdateClient(id, qtdMoeda);

            if (result is Client)
            {
                return Ok(result);
            }else if(result is Dictionary<dynamic, dynamic>) 
            {
                return StatusCode(405, "Method Not Allowed");
            }

            return BadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient(int id)
        {
            bool result = await _clientManagement.DeleteClient(id);

            if (result)
            {
                return Ok();
            }

            return StatusCode(405, "Method Not Allowed");
        }
    }
}
