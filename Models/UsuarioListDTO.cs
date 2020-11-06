using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace API_desafio.Models
{
    public class UsuarioListDTO
    {
        public string Nome { get; set; }
        public string Login { get; set; }
     
        public string Email { get; set; }
    }
}