using Microsoft.AspNetCore.Mvc;

namespace VimaV2.DTOs
{
    public class UsuarioDTO 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
    }
}
