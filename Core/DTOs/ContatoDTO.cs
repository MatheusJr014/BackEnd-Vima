using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VimaV2.DTOs
{
    public class ContatoDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Sobrenome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Assunto { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
    }
}
