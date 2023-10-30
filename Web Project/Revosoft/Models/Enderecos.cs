using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Revosoft.Models
{
    [Table("Enderecos")]
    public class Enderecos
    {
        [Key]
        public int EnderecosId { get; set; }

        [Required(ErrorMessage = "Informe o seu Endereço.")]
        [StringLength(100)]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Informe a Cidade.")]
        [StringLength(100)]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Informe o Estado.")]
        [StringLength(100)]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Informe o Bairro.")]
        [StringLength(100)]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Informe o Número.")]
        [StringLength(20)]
        [Display(Name = "Número")]
        public string Numero { get; set; }

        [StringLength(100)]
        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Informe o seu CEP.")]
        [Display(Name = "CEP")]
        [StringLength(10, MinimumLength = 8)]
        public string Cep { get; set; }


        [Display(Name = "Usuários")]
        public int UsuariosId { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
