using Microsoft.AspNetCore.Mvc;
using CredentialValidator.Models;

namespace CredentialValidator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CredentialValidatorController : Controller
    {
        [HttpPost("authenticate")]
        public IActionResult ValidateUserCredentials([FromBody] CredentialInputModel userInput)
        {
            var result = new CredentialValidatorModel();
            
            // TODO: Create faux DB of users and validate against it
            if (string.IsNullOrWhiteSpace(userInput.Username))
            {
                result.Errors.Add("Username is required.");
            }

            if (string.IsNullOrWhiteSpace(userInput.Password))
            {
                result.Errors.Add("Password is required.");
            }

            if (string.IsNullOrWhiteSpace(userInput.CertificateString))
            {
                result.Errors.Add("Malformed certificate, please contact your systems administrator.");
            }

            if (result.Errors.Count > 0)
            {
                result.ValidCredentials = false;
                return BadRequest(result);
            }
            else
            {
                result.ValidCredentials = true;
                return Ok(result);
            }
        }
    }
}
