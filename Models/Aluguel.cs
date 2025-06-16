using System.ComponentModel.DataAnnotations;

namespace AluguelApi.Models
{
    public class Aluguel
    {
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Cpf { get; set; } = string.Empty;

        public double Valor { get; set; }
    }
}
