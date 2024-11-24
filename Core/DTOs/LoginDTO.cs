using System.ComponentModel.DataAnnotations;

namespace VimaV2.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }

}