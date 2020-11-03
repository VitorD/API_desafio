
using System.ComponentModel.DataAnnotations.Schema;

namespace API_desafivo_v2.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        public int Id { get; set; }

        
        public string Nome { get; set; }

       
        public string Login { get; set; }

       
        public string Matricula { get; set; }

       
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        
        public string Email { get; set; }
    }
}