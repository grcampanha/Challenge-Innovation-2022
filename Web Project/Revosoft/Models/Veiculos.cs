using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Revosoft.Models
{
    [Table("Veiculos")]
    public class Veiculos
    {
        [Key]
        public int VeiculosId { get; set; }

        [Required(ErrorMessage = "Informe a placa do veículo.")]
        [Display(Name = "Placa")]
        [MaxLength(20, ErrorMessage = "A placa do veículo pode exceder {1} caracteres.")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "Informe o modelo do veículo.")]
        [Display(Name = "Modelo")]
        [MaxLength(30, ErrorMessage = "O modelo do veículo pode exceder {1} caracteres.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Informe o ano do veículo.")]
        [Display(Name = "Ano")]
        [MaxLength(20, ErrorMessage = "O ano do veículo pode exceder {1} caracteres.")]
        public string Ano { get; set; }


        [Display(Name = "Usuários")]
        public int UsuariosId { get; set; }
        public virtual Usuarios? Usuarios { get; set; }

        public List<Pecas>? Pecas { get; set; }
    }
}
