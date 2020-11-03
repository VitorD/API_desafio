using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using API_desafivo_v2.Data;
using API_desafivo_v2.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using API_desafivo_v2.Mappers;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.Remoting.Messaging;

namespace API_desafivo_v2.Controllers
{
    public class usuarioController : ApiController
    {
        //variavel estatica, para não resetar os dados quando nós fizermos as rotas
         private API_desafivo_v2Context dbUsuario = new API_desafivo_v2Context();
     
        //injecao de dependencia
        private static IMapper _mapper;
       

        //rota para adicionar um novo usuário, recebe um usuario serializado em JSON CADASTRO
        [HttpPost]
        public IHttpActionResult Adicionar([FromBody]  UsuarioCreateUpdateDTO usuario)
        {
            try { 
            //mapeamento do body pro Usuario
            _mapper = AutomapperConfig.RegisterMappings();
            var pronto = _mapper.Map<Usuario>(usuario);

            if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else {
                var verificacao = verifica_email_senha(usuario.Login, usuario.Email);
                    if (verificacao)
                    {
                    //gerando hash e incluindo armazenando no objeto que vai pro banco
                    pronto.Senha = CreateMD5(usuario.Senha);

                    //chama Dbcontext para adicionar o usuário
                    dbUsuario.Usuarios.Add(pronto);

                    var sucesso = dbUsuario.SaveChanges();

                    if (Convert.ToBoolean(sucesso)) return Ok("Usuário inserido com sucesso.");

                    else return BadRequest("Não conseguimos inserir o usuário. Por favor tente novamente");

                    }

                else return BadRequest("Email e Login ja existem");
            }
         }
            catch (InvalidOperationException ex)
            {
                return BadRequest("Ocorreu algum erro, por favor tente novamente." + ex.Message + ex.GetType().FullName);
            }
        }


        //rota para alterar um usuário existente, recebe um usuario serializado em JSON ATUALIZACAO
        [HttpPut]
        public IHttpActionResult Alterar([FromBody] UsuarioCreateUpdateDTO usuario)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                { 
                    

                    Usuario _usuario;
                    using (var consulta = new API_desafivo_v2Context())
                    {
                        //peguei o usuario
                        _usuario = consulta.Usuarios.Where(a => a.Id == usuario.Id).FirstOrDefault();
                    }
                    //Alterando usuario fora do contexto
                    if (_usuario!= null)
                    {
                        _mapper = AutomapperConfig.RegisterMappings();
                        var pronto = _mapper.Map<Usuario>(usuario);
                        using (var dbuser = new API_desafivo_v2Context())
                        {
                            //Marca a entidade como modificada
                            dbuser.Entry(pronto).State = System.Data.Entity.EntityState.Modified;
                            //chama o método SaveChanges
                            dbuser.SaveChanges();
                            return Ok( "conta alterada com sucesso" + StatusCode(HttpStatusCode.NoContent));
                        }    
                       
                    }
 
                    else return BadRequest("Nenhum usuário registrado. Por favor tente novamente");
                }   
            }
            catch (InvalidOperationException ex)
            {
                
                return BadRequest("Ocorreu algum erro, por favor tente novamente."+ ex.Message + ex.GetType().FullName);
            }
        }
      

        //LISTAGEM pega TODOS os usuário do banco
        [HttpGet]
        public IHttpActionResult ExibirTodos(int pagina = 1, int quantidadeRegistros = 5)
        {
            try
            {
                int totalPaginas = (int)Math.Ceiling(dbUsuario.Usuarios.Count() / Convert.ToDecimal(quantidadeRegistros));

                var user = dbUsuario.Usuarios.OrderBy(m => m.Id).Skip(quantidadeRegistros * (pagina - 1)).Take(quantidadeRegistros).
                           ProjectTo<UsuarioListDTO>(config).ToList();
                if (user.Count > 0) return Ok(user);
                else return Ok("nenhum usuario registrado no banco de dados");
            }
            catch (InvalidOperationException ex)
            {

                return BadRequest("Ocorreu algum erro, por favor tente novamente." + ex.Message + ex.GetType().FullName);
            }





        }

        static MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Usuario, UsuarioListDTO>();
            cfg.CreateMap<Usuario, UsuarioCreateUpdateDTO>();
        });

        //metodo de criptografia
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                //calcula o hash
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convertendo de byte para string
                StringBuilder result = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    result.Append(b.ToString("X2"));
                }
                return result.ToString();
            }
        }   

        
        //metodo de validacao
        private bool verifica_email_senha(string login, string email)
        {
            //consultas linq
            var loginvalidado = dbUsuario.Usuarios.Where(b => b.Login == login).FirstOrDefault();
            var emailvalidado = dbUsuario.Usuarios.Where(b => b.Email == email).FirstOrDefault();
            if (loginvalidado == null  || emailvalidado == null) return true;

            else return false;
        }
    }
}
