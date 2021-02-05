# Cadastro Produto
Aplicação ASP.NET Core+ Angular utilizando Command Pattern e Repository Pattern.

O projeto consiste na criação de um website composto por uma tela de login (autenticando via api) e um CRUD de produtos.

## Technologias e Patterns:
* Angular 8
* ASP.NET Core 3.1
* Entity Framework Core 3.1
* Command Pattern
* Repository Pattern
* MediatR
* Fluent Validation
* AutoMapper

# Detalhes

## Login
A tela de autenticação deve ser apenas composta pelos campos login e senha. Quando o usuário informar esses dados você deverá verificar se os dados são válidos realizando um post para a API "https://dev.sitemercado.com.br/api/login" e o retorno deverá ser tratado conforme o JSON na imagem abaixo. Se o usário e senha forem válidos, você deverá redirecionar o usuário para a tela de cadastro de produtos, caso contrário, retornar a mensagem retornada pela API para o usuário.

API de login:
 - Verbo: POST
 - Encoding: UTF8
 - Autenticação: HTTP Basic Authentication
 - Exemplo de retorno: 

Retorno válido:
{ "sucess" : true }

Retorno inválido:
{ "sucess" : false, "error" : "username or password incorrect" }

## Produtos
A funcionalidade de produtos possui a manutenção do cadastro (CRUD), inserção, edição, exclusão e listagem.
Para o cadastro do produto apenas os seguintes campos são necessários: Nome do Produto, Valor de Venda e Imagem.

## Author

The CadastroProduto was developed by [Josué Monteiro] under the [MIT license](LICENSE).
