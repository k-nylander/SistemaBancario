﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBancario.Contas
{
    internal class Corrente : Conta
    {
        private DateTime dataCriacao;
        public Corrente(int _numero, int _agencia, double _saldo)
        {
            numero = _numero;
            agencia = _agencia;
            rendimento = 1.01;
            Saldo = _saldo;
            dataCriacao = DateTime.Now;
        }
        // Eu achei isso interessante kkk. Declarar o método quase que como um atributo
        // o nome é "expressão lambda".
        public override double taxa() => (projecaoFutura() - Saldo) * 0.07;
    }
}