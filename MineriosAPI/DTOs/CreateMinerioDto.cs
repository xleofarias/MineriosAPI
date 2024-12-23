using System.ComponentModel.DataAnnotations;

namespace MineriosAPI.DTOs
{
    public class CreateMinerioDto
    {

        [Required]
        [StringLength(50, ErrorMessage = "O tamanho máximo é de 50 caracteres")]
        public required string nome { get; set; }
        
        [Required]
        [StringLength(35, ErrorMessage = "O tamanho máximo é de 35 caracteres")]
        public required string estado { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O tamanho máximo é de 100 caracteres")]
        public required string descricao { get; set; }
    }
}
