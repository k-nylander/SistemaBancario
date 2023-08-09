# Sistema Bancário - README

Este repositório contém um projeto de um Sistema Bancário desenvolvido em C# utilizando o Entity Framework Core para interagir com um banco de dados MySQL.

## Funcionalidades

1. **Entidades e Relacionamentos:** O sistema é modelado em torno de três entidades principais: `Usuario`, `Agencia` e `Conta`.

2. **Autenticação e Criptografia de Senhas:** O algoritmo do BCrypt é usado verificar as senhas dos usuários e contas de forma segura.

3. **Medidas de Segurança e Validações:** O código trata todas as excessões e foi projetado pensando na segurança das informações e de modo que no futuro possam ser feitas implementações de segurança de forma simples, como por exemplo uma validação de pagamento, transferência, email e etc.

4. **Manipulação de Dados:** O projeto utiliza o Entity Framework Core para realizar operações de CRUD (criação, leitura, atualização e exclusão) no banco de dados MySQL.

5. **Logging e Exceções:** (ainda não implementado)O código irá incluir tratamento de exceções para lidar com situações inesperadas e registra eventos importantes usando logs.

6. **Funcionalidades Futuras:** O projeto pode ser expandido com mais funcionalidades, como transferências entre contas, cálculo de juros e investimentos, relatórios financeiros, entre outros.

## Como Utilizar

1. Clone este repositório em sua máquina local.
2. Configure o banco de dados MySQL e atualize a string de conexão no arquivo `ConnDB_EF.cs`.
3. Instale os pacotes NuGet necessários, como o BCrypt.Net-Next e o Entity Framework Core.
4. Execute as Migrations para criar o esquema do banco de dados: `dotnet ef database update`.
5. Compile e execute o programa para interagir com o sistema bancário.

Este projeto foi feito apenas como um teste para portifólio e deve servir apenas como base para implementação de uma validação de contas em C#. Use o código como quiser 👍👍 Tmj
