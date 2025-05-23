using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MottuNET.model;

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
