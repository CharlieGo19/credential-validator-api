namespace CredentialValidator.Models
{
    public class CredentialValidatorModel
    {
        public bool ValidCredentials { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

    }
}
