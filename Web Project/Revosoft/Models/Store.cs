using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Revosoft.Models
{
    [Table("Store")]
    public class Store
    {
        [Key]
        public int StoreId { get; set; }

        [Required(ErrorMessage = "Informe o nome do produto")]
        [Display(Name = "Nome do Produto")]
        [StringLength(80, MinimumLength = 5, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2} caracteres")]
        public string Produto { get; set; }

        [StringLength(100, ErrorMessage = "O tamanho máximo é 100 caracteres")]
        [Required(ErrorMessage = "Informe o nome da categoria")]
        [Display(Name = "Categoria")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "Informe o preço do produto")]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 99999.99, ErrorMessage = "O preço deve estar entre 1 e 99999,99")]
        public decimal Preco { get; set; }
    }
}
