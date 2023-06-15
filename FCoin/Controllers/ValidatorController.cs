using FCoin.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FCoin.Controllers
{
    [ApiController]
    [Route("validador")]
    public class ValidatorController : ControllerBase
    {
        private readonly IValidatorManagement _validatorManagement;

        public ValidatorController(IValidatorManagement validatorManagement)
        {
            _validatorManagement = validatorManagement;
        }

        [HttpPost]
        public async Task<bool> ValidateTransaction(int id)
        {
            return false;
        }
    }
}
