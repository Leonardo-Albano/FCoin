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
        public async Task<IActionResult> CreateTransaction(Transaction transaction)
        {
            transaction = await _transactionManagement.CreateTransaction(transaction);

            if (transaction == null)
            {
                return BadRequest();
            }

            return Ok(transaction);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransaction(Transaction transaction)
        {
            var result = await _transactionManagement.UpdateTransaction(transaction);

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
