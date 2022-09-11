using System.ComponentModel.DataAnnotations;

namespace ControleDeUsuarios.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome de usuário é obrigatório")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "O campo Telefone é obrigatório")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Insira um número de telefone válido")]
        public long Tell { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "O campo não está em um formato de E-mail válido")]
        public string Email { get; set; }
    }
}
