using SistemaBancario.Resourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBancario.Contas
{
    internal class Poupanca : Conta
    {
        #region Variacao
        private double variacao;
        public double Variacao {
            get { return variacao; }
            set { 
                rendimento += value;
                variacao = value;
            }
        }
        #endregion
        public Poupanca(int _numero, int _agencia, double _saldo, double _variacao) // Construtor com parametros
        {
            numero = _numero;
            agencia = _agencia;
            rendimento = 1.03;
            Saldo = _saldo;
            Variacao = _variacao;
        }
        public Poupanca(){ // Construtor vazio
            rendimento = 1.03;
        }
        public override double taxa() => (projecaoFutura() - Saldo) * 0.1; // Simulação da taxa até o fim do ano
        public override void InserirDados() // No caso do construtor ser vazio, assim que se passam os parametros.
        {
            Console.Clear();
            ConsoleInterface iInput = new ConsoleInterface(" Cadastro de Conta Poupança ");
            while (true)
            {
                try
                {
                    Console.WriteLine(iInput.ToString());
                    Console.Write("Numero da Conta: ");
                    numero = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Numero da Agencia: ");
                    agencia = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Saldo: ");
                    Saldo = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Viariação do rendimento(%/mês): ");
                    Variacao = Convert.ToDouble(Console.ReadLine())/100;
                    break;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("Valor inválido. " + ex.ToString());
                }
            }
            Console.Clear(); // Limpa a tela e retorna sucesso.
            Console.WriteLine("Conta " + numero + " criada com sucesso!!");
        }
    }
}
