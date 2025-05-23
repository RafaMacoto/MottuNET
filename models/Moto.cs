using MottuNET.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MottuNET.model
{
    public enum StatusMoto
    {
        DISPONIVEL,
        RECUPERACAO,
        MANUTENCAO
    }

    public class Moto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Modelo { get; set; }

        [Required]
        public StatusMoto Status { get; set; }

        [Required]
        public string Posicao { get; set; }

        public string Problema { get; set; }

        public string Placa { get; set; }

        
        [ForeignKey("Ala")]
        public int AlaId { get; set; }

        public Ala Ala { get; set; }
    }
}
