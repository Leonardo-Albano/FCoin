using FCoin.Business.Interfaces;
using FCoin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<IActionResult> Validator(int offer, int selectorId)
        {
            Validator validator = new()
            {
                Offer = offer,
                SelectorId = selectorId
            };

            var result = await _validatorManagement.CreateValidator(validator);
            if(result.Key != 0 && result.Value != null)
            {
                return Ok(result);
            }
            
            return BadRequest();
        }

        [HttpGet("LastTransactionToValidate")]
        public async Task<IActionResult> LastTransactionToValidate(int validatorId)
        {
            var result = await _validatorManagement.LastTransactionToValidate(validatorId);

            if(result != null)
            {
                if(result == 0)
                {
                    return Ok("No transactions to validate");
                }

                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("Validate")]
        public async Task<bool> ValidateTransaction(int validatorId, string tokenValidator, int transactionId)
        {
            var result = await _validatorManagement.ValidateTransaction(validatorId, tokenValidator, transactionId);
            return result;
        }
    }
}
