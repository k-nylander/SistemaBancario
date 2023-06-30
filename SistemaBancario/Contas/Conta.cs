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
        public int numero { get; protected set; }
        public int agencia { get; protected set; }

        /* Acho que tem um problema com o rendimento e as taxas.
         * Supondo que uma pessoa mudasse o rendimento no meio do ano, antes da cobraça dessa taxa.
         * Isso afetaria o lucro e consequentemente a taxa. Então seria bom fazer um atributo "lucroAnual" 
         * pra poder taxar sem dar nenhuma difereça?
         */
        protected double rendimento;

        #region campo_saldo
        /* Precisei fazer isso para evitar um loop no setter do saldo.
         * Ex: double saldo {get{return saldo} set{saldo=value}};
         * 
         * Fazendoisso o programa fica chamando o saldo pra sempre e eu não sabia arrumar kkk
         */

        private double saldo;//Atributo saldo
        protected double Saldo //Propriedade saldo
        {
            get { return saldo; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("O saldo deve ser não negativo");
                saldo = value;
            }
        }
        #endregion

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
                    Console.Write("Numero da Conta:");
                    numero = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Numero da Agencia:");
                    agencia = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Saldo:");
                    Saldo = Convert.ToDouble(Console.ReadLine());
                    break;
                }catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("Valor inválido. " + ex.ToString());
                }
            }
            Console.Clear();
            Console.WriteLine("Conta " + numero + " criada com sucesso!!");
        }
    }
}
