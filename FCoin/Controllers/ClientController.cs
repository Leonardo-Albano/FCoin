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
        public async Task<IActionResult> Client(int? id)
        {
            dynamic client = await _clientManagement.GetClient(id);

            if(client != null)
            {
                return Ok(client);
            }

            return NotFound(client);
        }

        [HttpPost]
        public async Task<IActionResult> Client(Client client)
        {
            client = await _clientManagement.CreateClient(client);

            if (client == null)
            {
                return BadRequest();
            }

            return Ok(client);
        }

        [HttpPut]
        public async Task<IActionResult> Client(int id, int qtdMoeda)
        {
            var result = await _clientManagement.UpdateClient(id, qtdMoeda);

            //fazer uma lógica para corrigir o status code

            if (result is Client)
            {
                return Ok(result);
            }else if(result is KeyValuePair<string, string>) 
            {
                return StatusCode(405, "Method Not Allowed");
            }

            return BadRequest(result);
        }
    }
}
