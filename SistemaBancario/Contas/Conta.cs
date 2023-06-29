using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SistemaBancario.Contas
{
    internal abstract class Conta
    {
        protected int numero;
        protected int agencia;

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
        public double projecaoFutura()
        {
            DateTime proximoAno = new DateTime(DateTime.Now.Year + 1, 1, 1);
            TimeSpan tempoFaltando = proximoAno - DateTime.Now;
            return Math.Pow(rendimento, Math.Floor((double)tempoFaltando.Days / 30)) * saldo;
        }
        public abstract double taxa();
    }
}
