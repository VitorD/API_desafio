# API_desafio
    API de listagem, cadastro e modificação de usuários
	Tentei seguir ao máximo os princípios dos patterns SOLID, DRY, tentando manter o mínimo de acoplamento e repetimento de código
	Como as opiniões a respeito de onde as regras de negócio ficam são indefinidas na internet, decidi me guiar pelo tutorial da Microsoft que deixa as lógicas no controller.
    A criação e funcionamento foi finalizada no dia 3, ocorreram atrasos devido a refatoração do codigo para injeção de dependencias, mapeamento das classes com automapper e etc.

## pacotes, libs  e componentes usados
	.NET framework 4.7.2
    Unity - mapea os containers onde o DI vai ser usada
    AutoMappper - mapeamento das classes DTO para a classe model de usuario
	Data Annotations para a validaçao de todos os campos
	Entity Framework que fica responsavel pelo mapeamento objeto-relacional do model usuario com o banco de dados.
	o acesso aos dados é feito somente usando o Dbcontext do Entity Framework


## critérios para executar a aplicação

    A API segue o padrao REST com três metodos, POST, PUT e GET, no controller usuarioController.
    metodo de Adição : POST localhost:{porta}/api/usuario/Adicionar
    metodo de Atualização : PUT localhost:{porta}/api/usuario/Alterar
    metodo de Listagem de todos os usuarios : GET localhost:{porta}/api/usuario/ExibirTodos
    A API foi testada usando o Postman.
### corpo da requisição

    Adição :
		no body em JSON ->  {
							"Nome":"joao",
							"Login" : "joao123",
							"Matricula":"0123456789",
							"Senha":"senha",
							"Email":"joao@gmail.com"
						  }

    Atualização :
    no body em JSON ->  {
						"Id" : "1",
						"Nome":"mudou2",
						"Login" : "mudou2",
						"Matricula":"9876543210",
						"Senha":"senha123",
						"Email":"mudou2@gmail.com"
					  }
					  Obs.: Deve-se informar Id para que os outros campos possam ser alterados
						
    Listagem :
    retorno em JSON -> exemplo :
					{
						"Nome": "1mudado1",
						"Login": "1mudado1",
						"Email": "m1udado2@gmail.com"
					}
					Obs.: Escolhi mostrar apenas três campos para demonstrar que a classe DTO funciona.