using System;
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
using System.Data.Entity;

namespace API_desafivo_v2.Controllers
{
    public class usuarioController : ApiController
    {

        //variavel estatica, para não resetar os dados quando nós fizermos as rotas
        
        private static API_desafivo_v2Context _context;
        private static IMapper _mapper;
        public usuarioController(API_desafivo_v2Context dbUsuario)
            {
                _context = dbUsuario;
            }

        IConfigurationProvider config = AutomapperConfig.Criar_Mapeamento();

        //injecao de dependencia
        
       

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
                    _context.Usuarios.Add(pronto);

                    var sucesso = _context.SaveChanges();

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

                    //peguei o usuario
                    var usuario_existe = _context.Usuarios.Count(a => a.Id == usuario.Id);

                    //Alterando usuario fora do contexto
                    if (usuario_existe > 0)
                    {
                        _mapper = AutomapperConfig.RegisterMappings();
                         var _usuario = _mapper.Map<Usuario>(usuario);
                        //senha criptografada
                        _usuario.Senha = CreateMD5(_usuario.Senha);
                        //Marca a entidade como modificada
                        _context.Entry(_usuario).State = EntityState.Modified;
                        //chama o método SaveChanges
                        _context.SaveChanges();
                        return Ok( "conta alterada com sucesso" + StatusCode(HttpStatusCode.NoContent));
                    }

                    else return BadRequest("Usuario inexistente. Por favor tente novamente");
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
                int totalPaginas = (int)Math.Ceiling(_context.Usuarios.Count() / Convert.ToDecimal(quantidadeRegistros));

                var user = _context.Usuarios.OrderBy(m => m.Id).Skip(quantidadeRegistros * (pagina - 1)).Take(quantidadeRegistros).
                           ProjectTo<UsuarioListDTO>(config).ToList();
                if (user.Count > 0) return Ok(user);
                else return Ok("nenhum usuario registrado no banco de dados");
            }
            catch (InvalidOperationException ex)
            {

                return BadRequest("Ocorreu algum erro, por favor tente novamente." + ex.Message + ex.GetType().FullName);
            }





        }

        

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
            var loginvalidado = _context.Usuarios.Where(b => b.Login == login).FirstOrDefault();
            var emailvalidado = _context.Usuarios.Where(b => b.Email == email).FirstOrDefault();
            if (loginvalidado == null  || emailvalidado == null) return true;

            else return false;
        }
    }
}
