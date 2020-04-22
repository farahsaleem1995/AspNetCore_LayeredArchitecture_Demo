using System.ComponentModel.DataAnnotations;

namespace LayeredArch.Api.Resources.Auth
{
    public class CreateTokenResourcecs
    {
        [Required]
        public int RefreshToken { get; set; }

        [Required]
        public string AccessToken { get; set; }
    }
}