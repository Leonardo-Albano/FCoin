using FCoin.Business.Interfaces;
using FCoin.Models;
using Microsoft.AspNetCore.Mvc;

namespace FCoin.Controllers
{
    [ApiController]
    [Route("validador")]
    public class ValidationController : ControllerBase
    {
        private readonly IValidationManagement _validationManagement;
        public ValidationController(IValidationManagement validationManagement)
        {
            _validationManagement = validationManagement;
        }

        [HttpGet]
        public int ValidateTransaction([FromQuery]Transaction transaction)
        {
            var result = _validationManagement.ValidateTransaction(transaction);
            return result;
        }
    }
}
