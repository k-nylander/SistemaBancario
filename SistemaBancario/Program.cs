using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq.Expressions;
using Org.BouncyCastle.Pkix;
using SistemaBancario.Resourses;

namespace SistemaBancario
{
    internal class Program
    {
        static void Main(string[] args) {
            using (var conn = new ConnDB_EF())
            {
                //Exemplo de Utilização da "API" de banco.
                
                var us = conn.Login_Usuario("00000000000", "senhaFr4ca");
                var conta = conn.Cadastro_Conta(us, "SenhaSup3rF0rte");

                //Mostrando no Console.
                var iPrincipal = new ConsoleInterface($" ~ Dados do Dono","%div", $"Nome: {us.Nome}", $"CPF: {us.CPF}");
                Console.WriteLine(iPrincipal.ToString() + "\n\n");
                iPrincipal.geraInterface($"~ Dados da Conta", "%div", $"{conta.Numero}/{conta.Agencia.Id}", $"Saldo: {conta.Saldo}", $"Rendimentos: {(conta.Rendimento - 1)*100}%/mês | Var: {conta.Variacao*100}%rend/mês");
                Console.WriteLine(iPrincipal.ToString());
                
                //Deletando Conta da amostra.
                conn.Fechar_Conta(us, conta, "SenhaSup3rF0rte");
            }
        }
    }
}