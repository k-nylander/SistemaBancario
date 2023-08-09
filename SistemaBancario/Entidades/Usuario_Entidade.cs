using Org.BouncyCastle.Crypto.Generators;
using SistemaBancario.Resourses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SistemaBancario.Entidades
{
    internal class Usuario_Entidade
    {

        // -- Atributos Entidade --

        #region CPF Campo/Prop
        private string cpf;
        [Key]
        public string CPF {
            get { return cpf; }
            set
            {
                if (!string.IsNullOrEmpty(cpf))
                    throw new Exception("Não é possível alterar o CPF do usuário.");
                else
                {
                    if (ApenasDigitos(value).Length != 11)
                        throw new Exception("O CPF está fora do formato correto.");
                    cpf = ApenasDigitos(value);
                }
            }
        }
        #endregion

        #region Nome Campo/Prop
        private string nome;
        
        public string Nome { 
            get { return nome; }
            set
            {
                if(!String.IsNullOrEmpty(nome))
                    throw new UnauthorizedAccessException("Não é possível alterar o nome do usuario.");
                nome = value;
            } 
        }
        #endregion

        [EmailAddress]
        public string Email { get; set; }

        #region Endereço(NULO)
        public string? Endereco { get; set; }
        #endregion

        #region Telefone Campo/Prop (NULO)
        [RegularExpression(@"^\(?(\d{2})\)?[-. ]?(\d{4,5})[-. ]?(\d{4})$",ErrorMessage = "Numero de telefone fora do formato esperado")] // Expressão que regra o formato dos numero de telefone.
        public string? Telefone { get; set; }
        #endregion

        #region Senha Campo/Prop
        private string senha = null;
        public string Senha {
            get { return senha; }
            set 
            {
                if (!String.IsNullOrEmpty(senha)) // Se uma nova senha for inserida, o valor será criptografado e armazenado no atributo senha.
                    senha = CriptografaSenha(value);

                using (var contexto = new ConnDB_EF())
                {
                    if (contexto.Entry(this).IsKeySet) // Verifica se esse é um novo usuário ou se está sendo importado do banco. Caso seja novo criptografa a senha.
                        senha = CriptografaSenha(value);
                    else // Caso seja um já existente a senha só é armazenada.
                        senha = value;
                }
            }
        }
        #endregion

        string ApenasDigitos(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach(char c in input) 
            {
                if (!Char.IsDigit(c))
                    continue;
                sb.Append(c);
            }
            return sb.ToString();
        }

        string CriptografaSenha(string input)
        {
            if (!Requisitos_Senha(input))
                throw new Exception("A senha é fraca."); // Exemplo correto: 1234567890Aa
            return BCrypt.Net.BCrypt.HashPassword(input);
        }

        bool Requisitos_Senha(string senha)
        {
            bool flagLength = senha.Length > 7, flagTemMaiuscula = false, flagTemMinuscula = false, flagTemNumero = false;

            foreach(char c in senha)
            {
                if (Char.IsLower(c))
                    flagTemMinuscula = true;
                if (Char.IsUpper(c))
                    flagTemMaiuscula = true;
                if (Char.IsDigit(c))
                    flagTemNumero = true;
            }
            return flagTemMinuscula && flagTemMaiuscula && flagTemNumero && flagLength;
        }

        public bool ComparaSenha(string input)
        {
            return BCrypt.Net.BCrypt.Verify(input, senha);
        }
    }
}
