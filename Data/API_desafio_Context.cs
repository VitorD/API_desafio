using System.Data.Entity;
using API_desafio.Models;

namespace API_desafio.Data
{
    public class API_desafio_Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public API_desafio_Context() : base("name=banco_usuarios")
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }

    }
 
 
}
