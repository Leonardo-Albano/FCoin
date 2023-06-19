using FCoin.Business.Interfaces;
using FCoin.Models;
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

        [HttpGet]
        public async Task<IActionResult> GetValidators(int? id)
        {
            var result = await _validatorManagement.GetValidator(id);
            if (result != null)
            {
                return Ok(result);

            }
            return BadRequest();
        }

        [HttpGet("Selector")]
        public async Task<IActionResult> GetValidatorBySelector(int selectorId)
        {
            var result = await _validatorManagement.GetValidatorsBySelector(selectorId);
            if(result != null)
            {
                return Ok(result);

            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Validator(Validator validator)
        {
            var result = await _validatorManagement.CreateValidator(validator);
            if(result != 0)
            {
                return Ok(result);
            }
            
            return BadRequest();
        }

        [HttpPost("Validate")]
        public async Task<bool> ValidateTransaction(int idValidator, string tokenValidator, int id)
        {
            var result = await _validatorManagement.ValidateTransaction(idValidator, tokenValidator, id);
            return result;
        }
    }
}
