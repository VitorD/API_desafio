using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web;
using API_desafivo_v2.Models;

namespace API_desafivo_v2.Data
{
    public class API_desafivo_v2Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public API_desafivo_v2Context() : base("name=banco_usuarios")
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }

    }
 
 
}
