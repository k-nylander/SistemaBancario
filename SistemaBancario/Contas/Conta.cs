using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using SistemaBancario.Resourses;

namespace SistemaBancario.Contas
{
    internal abstract class Conta
    {
        #region AtrubutoNumero
        private int numero;
        public int Numero {
            get { return numero; }
            protected set {
                // Verificação do numero da conta.
                if (Program.BancoDeDados.Any(conta => conta.agencia == this.agencia && conta.numero == value))
                    throw new Exception("Numero de conta já registrado nessa agência");
                else
                    numero = value;
            }
        }
        #endregion
        public int agencia { get; protected set; }
        #region campo_saldo
        private double saldo;//Atributo saldo
        protected double Saldo //Propriedade saldo
        {
            get { return saldo; }
            set{
                if (value < 0)
                    throw new ArgumentException("O saldo não pode ser negativo");
                else
                    saldo = value;
            }
        }
        #endregion
        protected double rendimento;

        public double projecaoFutura() // Simula quanto rendimento bruto haverá até o final do ano.
        {
            DateTime proximoAno = new DateTime(DateTime.Now.Year + 1, 1, 1);
            TimeSpan tempoFaltando = proximoAno - DateTime.Now;
            return Math.Pow(rendimento, Math.Floor((double)tempoFaltando.Days / 30)) * saldo;
        }

        public abstract double taxa(); // Calcula a taxa

        public virtual void InserirDados() // Modo de inserir os dados quando o construtor é vazio.
        {
            Console.Clear();
            ConsoleInterface iInput = new ConsoleInterface(" Cadastro de Conta ");
            while (true)
            {
                try
                {
                    Console.WriteLine(iInput.ToString());
                    Console.Write("Numero da Agencia:");
                    agencia = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Numero da Conta:");
                    Numero = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Saldo:");
                    Saldo = Convert.ToDouble(Console.ReadLine());
                    break;
                }catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($"Valor inválido. {ex.Message}");
                }
            }
            Console.Clear();
            Console.WriteLine("Conta " + Numero + " criada com sucesso!!");
        }
        
        public void Transferencia(Conta destino, double valor)
        {
            if(Saldo < valor){
                throw new Exception("Saldo insufcente!");
            }
            Saldo -= valor;
            destino.Saldo += valor;
            
            Console.Clear();
            Console.WriteLine(" Transação concluída com sucesso ");
        }

        public override string ToString()
        {
            ConsoleInterface ContaBox = new ConsoleInterface($"Conta {Numero}", $"Agência {agencia}", "%div", $"R$ {Saldo}");
            return ContaBox.ToString();
        }
        public void saque(double valor) => Saldo -= valor;
        public void deposito(double valor) => Saldo += valor;

    }
}