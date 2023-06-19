using FCoin.Business;
using FCoin.Business.Interfaces;
using FCoin.Models;
using Microsoft.AspNetCore.Mvc;

namespace FCoin.Controllers
{
    [ApiController]
    [Route("transacoes")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionManagement _transactionManagement;

        public TransactionController(ITransactionManagement transactionManagement)
        {
            _transactionManagement = transactionManagement;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransaction(int? id)
        {
            dynamic transaction = await _transactionManagement.GetTransaction(id);

            if (transaction != null)
            {
                return Ok(transaction);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(int RemetenteId, int RecebedorId, int valor)
        {
            Transaction transaction = new()
            {
                Remetente = RemetenteId,
                Recebedor = RecebedorId,
                Valor = valor
            };

            transaction = await _transactionManagement.CreateTransaction(transaction);

            if (transaction == null)
            {
                return BadRequest();
            }

            return Ok(transaction);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransaction(int id, int status)
        {
            var result = await _transactionManagement.UpdateTransaction(id, status);

            if (result is Transaction)
            {
                return Ok(result);
            }
            //else if (result is Dictionary<dynamic, dynamic>)
            //{
            //    return StatusCode(405, "Method Not Allowed");
            //}

            return BadRequest(result);
        }
    }
}
