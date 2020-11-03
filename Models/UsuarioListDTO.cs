using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace API_desafivo_v2.Models
{
    public class UsuarioListDTO
    {
        public string Nome { get; set; }
        public string Login { get; set; }
     
        public string Email { get; set; }
    }
}