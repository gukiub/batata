using System.ComponentModel.DataAnnotations;

namespace Mercado.DTO
{
    public class PromocaoDTO
    {
        [Required]
        public int Id {get; set;}
        [StringLength(100)]
        [MinLength(3)]
        public string Nome {get; set;}
        [Required]
        public int ProdutoID {get; set;}
        
        [Required(ErrorMessage="Informe a porcentagem de desconto do produto.")]
        [Range(0, 100)]
        public float Porcentagem {get; set;}
    }
}