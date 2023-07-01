using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq.Expressions;
using SistemaBancario.Contas;
using SistemaBancario.Resourses;

namespace SistemaBancario
{
    internal class Program
    {
        // Simula um banco de dados para armazenar as contas criadas.
        public static List<Conta> BancoDeDados = new List<Conta>();
        static void Main(string[] args) {
            //Declaração da interface
            ConsoleInterface iMenu = new ConsoleInterface("-- Escolha uma das opções --", "%div", "1. Criar conta corrente", "2. Criar conta poupança", "3. Verifcar taxas", "4. Depósito.", "5. Saque", "6. Transferencia", "7. Listar Contas", "%div","9. Sair");

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
                            //Verificação dos dados.
                            Conta temp = achaConta();
                            if (temp == null)
                                throw new Exception("Conta não encontrada...");
                            Console.Clear();
                            Console.WriteLine($"Valor a ser pago: R${temp.taxa().ToString("N2")}");
                            break;
                        case 4: // Depósito
                            Console.Write("Valor do Depósito: ");
                            double valorDeposito = Convert.ToDouble(Console.ReadLine());
                            achaConta().deposito(valorDeposito);
                            break;
                        case 5: // Saque
                            Console.Write("Valor do Saque: ");
                            double valorSaque = Convert.ToDouble(Console.ReadLine());
                            achaConta().saque(valorSaque); 
                            break;
                        case 6: // Transferencia
                            Conta fonte = achaConta();
                            Console.WriteLine("\n-- Conta do Destinatário -- ");
                            Conta destino = achaConta();
                            if (destino.Equals(fonte))
                                throw new Exception(" - Erro na transferência!! -");
                            Console.Write("\nValor da transferenecia: ");
                            fonte.Transferencia(destino,Convert.ToDouble(Console.ReadLine()));
                            break;
                        case 7:
                            Console.Clear();
                            foreach (Conta x in BancoDeDados)
                            {
                                Console.WriteLine(x.ToString() + "\n");
                            }
                            break;
                        case 9: // Encerra o loop
                            menuFlag = false;
                            break;
                        default: // Recebe os inputs errados
                            throw new Exception(" - Resposta Inválida!! - ");
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
            }
        }
        static Conta achaConta() // Encontra uma conta dentro do "Banco de Dados"
        {
            Console.Write("Digite o numero da conta:");
            int numeroConta = Convert.ToInt32(Console.ReadLine());
            Console.Write("Digite o numero da agencia:");
            int numeroAgencia = Convert.ToInt32(Console.ReadLine());
            return BancoDeDados.Find(conta => conta.Numero == numeroConta && conta.agencia == numeroAgencia);
        }
    }
}