using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBancario.Contas
{
    internal class Poupanca : Conta
    {
        private double variacao;
        public double Variacao {
            get { return this.variacao; }
            set { 
                this.rendimento += value;
                variacao = value;
            }
        }
        public Poupanca(int _numero, int _agencia, double _saldo, double _variacao)
        {
            this.numero = _numero;
            this.agencia = _agencia;
            this.rendimento = 1.03;
            this.Saldo = _saldo;
            this.variacao = _variacao;
        }
        public override double taxa() => (projecaoFutura() - this.Saldo) * 0.1;
    }
}
