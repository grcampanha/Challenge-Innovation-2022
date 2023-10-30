using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Revosoft.Models
{
    [Table("Pecas")]
    public class Pecas
    {
        [Key]
        public int PecasId { get; set; }

        [MaxLength(3)]
        [Display(Name = "Score do Motor")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal MotorScore { get; set; }

        [MaxLength(3)]
        [Display(Name = "Score do Câmbio")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal CambioScore { get; set; }

        [MaxLength(3)]
        [Display(Name = "Score do Conjunto De Tração")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PneuScore { get; set; }

        [MaxLength(3)]
        [Display(Name = "Score da Bateria")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal BateriaScore { get; set; }


        [Display(Name = "Veículos")]
        public int VeiculosId { get; set; }
        public virtual Veiculos? Veiculos { get; set; }
    }
}
