using FCoin.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

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
        public async Task<bool> ValidateTransaction(int idValidator, string tokenValidator, int id)
        {
            var result = await _validatorManagement.ValidateTransaction(idValidator, tokenValidator, id);
            return result;
        }
    }
}
