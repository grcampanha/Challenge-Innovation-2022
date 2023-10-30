using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Revosoft.Models
{
    [Table("Usuarios")]
    public class Usuarios
    {
        [Key]
        public int UsuariosId { get; set; }

        [Required(ErrorMessage = "Informe o nome.")]
        [Display(Name = "Nome")]
        [MaxLength(80, ErrorMessage = "O nome pode exceder {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o sobrenome.")]
        [Display(Name = "Sobrenome")]
        [MaxLength(80, ErrorMessage = "O sobrenome pode exceder {1} caracteres.")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Informe a senha.")]
        [Display(Name = "Senha")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2} caracteres.")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Informe o email.")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
    ErrorMessage = "O email não possui um formato correto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o seu telefone.")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }


        public List<Veiculos>? Veiculos { get; set; }
        public List<Enderecos>? Enderecos { get; set; }
    }
}
