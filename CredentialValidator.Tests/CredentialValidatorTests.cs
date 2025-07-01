using Xunit;
using CredentialValidator.Models;
using CredentialValidator.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CredentialValidator.Tests
{
    public class CredentialValidatorTests
    {
        [Fact]
        public void ValidateUserCredentials_ValidInput_ReturnsValidCredentials()
        {
            var input = new CredentialInputModel
            {
                Username = "CharlieGo_",
                Password = "strongpassword2025",
                Email = "c@sharp.com",
                CertificateString = "validCertificateString"
            };

            CredentialValidatorController validator = new();
            var callResult = validator.ValidateUserCredentials(input);

            var expectedOkResult = Assert.IsType<OkObjectResult>(callResult);
            var result = Assert.IsType<CredentialValidatorModel>(expectedOkResult.Value);

            Assert.True(result.ValidCredentials);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void ValidateUserCredentials_InvalidInput_UsernameEmpty()
        {
            var input = new CredentialInputModel
            {
                Username = "",
                Password = "strongpword2025",
                Email = "c@sharp.com",
                CertificateString = "validCertificateString"
            };
       
            CredentialValidatorController validator = new();
            var callResult = validator.ValidateUserCredentials(input);

            var expectedBadRequest = Assert.IsType<BadRequestObjectResult>(callResult);
            var result = Assert.IsType<CredentialValidatorModel>(expectedBadRequest.Value);

            Assert.False(result.ValidCredentials);
            Assert.Single(result.Errors);
            Assert.Equal("Username is required.", result.Errors[0]);           
        }

        [Fact]
        public void ValidateUserCredentials_InvalidInput_PasswordEmpty()
        {
            var input = new CredentialInputModel
            {
                Username = "CharlieGo_",
                Password = "",
                Email = "c@sharp.com",
                CertificateString = "validCertificateString"
            };
            
            CredentialValidatorController validator = new();
            var callResult = validator.ValidateUserCredentials(input);

            var expectedBadRequest = Assert.IsType<BadRequestObjectResult>(callResult);
            var result = Assert.IsType<CredentialValidatorModel>(expectedBadRequest.Value);

            Assert.False(result.ValidCredentials);
            Assert.Single(result.Errors);
            Assert.Equal("Password is required.", result.Errors[0]);
        }

        [Fact]
        public void ValidateUserCredentials_InvalidInput_CertificateEmpty()
        {
            var input = new CredentialInputModel
            {
                Username = "CharlieGo_",
                Password = "strongpassword2025",
                Email = "c@sharp.com",
                CertificateString = ""
            };

            CredentialValidatorController validator = new();
            var callResult = validator.ValidateUserCredentials(input);

            var expectedBadRequest = Assert.IsType<BadRequestObjectResult>(callResult);
            var result = Assert.IsType<CredentialValidatorModel>(expectedBadRequest.Value);

            Assert.False(result.ValidCredentials);
            Assert.Single(result.Errors);
            Assert.Equal("Malformed certificate, please contact your systems administrator.", result.Errors[0]);
        }

        [Fact]
        public void ValidateUserCredentials_InvalidInput_AggregateErrors()
        {
            var input = new CredentialInputModel
            {
                Username = "",
                Password = "",
                Email = "c@sharp.com",
                CertificateString = ""
            };

            CredentialValidatorController validator = new();
            var callResult = validator.ValidateUserCredentials(input);

            var expectedBadRequest = Assert.IsType<BadRequestObjectResult>(callResult);
            var result = Assert.IsType<CredentialValidatorModel>(expectedBadRequest.Value);

            Assert.False(result.ValidCredentials);
            Assert.Equal(3, result.Errors.Count);
            Assert.Contains("Username is required.", result.Errors);
            Assert.Contains("Password is required.", result.Errors);
            Assert.Contains("Malformed certificate, please contact your systems administrator.", result.Errors);
        }
    }
}