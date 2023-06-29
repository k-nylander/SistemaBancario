using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using SistemaBancario.Contas;
using SistemaBancario.Resourses;

namespace SistemaBancario
{
    internal class Program
    {
        static void Main(string[] args) {
            Poupanca p1 = new Poupanca(123123, 777, 850.0 * 6, 0.01);
            
            ConsoleInterface i = new ConsoleInterface();
            i.geraInterface("-- Escolha uma das opções --", "1. Criar conta corrente", "2. Criar conta poupança");
            
            try{
                Console.WriteLine(i.InterfaceString + "\nR: ");
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        
                        break;
                    case 2:

                        break;
                    default:
                        Console.WriteLine("Resposta");
                        break;
                }
            }
            catch (Exception ex){
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Valor no final do ano: " + p1.projecaoFutura());
            Console.WriteLine(p1.taxa());
        }
    }
}