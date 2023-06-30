    using System;
    using System.Collections.Generic;
using System.Data;
using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace SistemaBancario.Resourses
    {
        internal class ConsoleInterface
        {
            private List<string> temp = new List<string>(); // Armazena os parametros iniciais
            private List<string> interfaceString = new List<string>(); // Armazena as linhas formatadas

            public ConsoleInterface(params string[] linhas)
            { // Método Construtor
                temp.AddRange(linhas);
                geraInterface(linhas);                
            }
            public void geraInterface(params string[] linhas) // Cria a interface
            {
                interfaceString.Clear(); // Limpa a linhas da interface.
                int maior = linhas.Max(linha => linha.Length); // O método max retorna o maior valor de uma função seletora, no caso, qual é o maior parametro.
                StringBuilder sb = new StringBuilder(); // Manipula strings
                
                // Gera a divisória para a interface.
                StringBuilder division = new StringBuilder();
                division.Append("+");
                division.Append('-', maior);
                division.Append('+');

                foreach (var atual in linhas) // Formata cada linha corretamente e armazena na em uma lista.
                {
                    if (String.Compare(atual,"%div") == 0)
                    {
                        interfaceString.Add(division.ToString());
                        continue;
                    }
                    sb.Append("|");
                    sb.Append(atual);
                    sb.Append(' ', maior - atual.Length);
                    sb.Append("|");
                    interfaceString.Add(sb.ToString());
                    sb.Clear();
                }

                //Adiciona na lista de linhas.
                interfaceString.Insert(0, division.ToString());
                interfaceString.Add(division.ToString());
            }
            public void AddLinha(string linha) // Adiciona uma nova linha no final
            {
                temp.Add(linha);
                geraInterface(temp.ToArray());
            }
            public override string ToString() // Retorna a inferface pronta
            {
                return string.Join('\n', interfaceString); ;
            }
        }
    }
