using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq.Expressions;
using SistemaBancario.Contas;
using SistemaBancario.Resourses;

namespace SistemaBancario
{
    internal class Program
    {
        static void Main(string[] args) {
            //Declaração da interface
            ConsoleInterface iMenu = new ConsoleInterface("-- Escolha uma das opções --", "%div", "1. Criar conta corrente", "2. Criar conta poupança", "3. Verifcar taxas", "4. Fazer tansferência.", "%div","9. Sair");

            // Simula um banco de dados para armazenar as contas criadas.
            List<Conta> BancoDeDados = new List<Conta>();

            bool menuFlag = true;
            while (menuFlag) // Garante que o usuário entrará com um valor válido.
            {
                try
                {
                    Console.Write(iMenu.ToString() + "\nR: ");
                    switch (Convert.ToInt32(Console.ReadLine()))
                    {
                        case 1: // Criação de Conta Corrente.
                            Corrente c = new();
                            c.InserirDados();
                            BancoDeDados.Add(c);
                            break;

                        case 2: // Criação de Conta Poupança.
                            Poupanca p = new();
                            p.InserirDados();
                            BancoDeDados.Add(p);
                            break;

                        case 3: // Verificação de taxas
                            Console.Write("Digite o numero da conta:");
                            int numeroConta = Convert.ToInt32(Console.ReadLine());
                            Conta temp = BancoDeDados.Find(numero => numero.numero == numeroConta);
                            if (temp == null)
                            {
                                Console.Clear();
                                Console.WriteLine("Conta não encontrada...");
                                continue;
                            }
                            Console.Clear();
                            Console.WriteLine("Valor a ser pago: " + temp.taxa().ToString("N2"));
                            break;
                        case 4: // Depósito

                            break;
                        case 5: // Saque

                            break;
                        case 6: // Transferencia

                            break;
                        case 9:
                            menuFlag = false;
                            break;

                        default: // Recebe os inputs errados
                            Console.WriteLine(" - Resposta Inválida!! - ");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}