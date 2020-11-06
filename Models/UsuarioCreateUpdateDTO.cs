
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_desafio.Models
{
    public  class UsuarioCreateUpdateDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe seu Nome.")]
        [MaxLength(30, ErrorMessage = "Nome muito grande")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o Login.")]
        [MaxLength(20, ErrorMessage = "Login de no máximo 20 caracteres.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe a Matricula")]
        [MaxLength(10, ErrorMessage = "A Matricula não deve exceder 10 caracteres.")]

        public string Matricula { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório.")]

        public string Senha { get; set; }

        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O endereço de email não é válido")]
        public string Email { get; set; }
    }
}