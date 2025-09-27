using MottuNET.model;
using System.ComponentModel.DataAnnotations;

namespace MottuNET.Models
{
    public class Usuario
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress] 
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string Senha { get; set; }

       
    }
}
