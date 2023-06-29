    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace SistemaBancario.Resourses
    {
        internal class ConsoleInterface
        {
            private List<string> interfaceString = new List<string>();
            public string InterfaceString { get {
                    return string.Join('\n',interfaceString);
                } 
            }
            public void geraInterface(params string[] linhas)
            {
                interfaceString.Clear(); // Limpa a 
                int maior = linhas.Max(linha => linha.Length);
                StringBuilder sb = new StringBuilder();

                foreach (var atual in linhas)
                {
                    sb.Append("|");
                    sb.Append(atual);
                    sb.Append(' ', maior - atual.Length);
                    sb.Append("|");
                    interfaceString.Add(sb.ToString());
                }

                sb.Clear();
                sb.Append("+");
                sb.Append('-', maior);
                sb.Append('+');

                interfaceString.Insert(0, sb.ToString());
                interfaceString.Add(sb.ToString());
            }
        }
    }
