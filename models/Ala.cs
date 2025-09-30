using System.ComponentModel.DataAnnotations;

namespace MottuNET.Models
{
    public class Ala
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        
        public List<Moto> Motos { get; set; }
    }
}
