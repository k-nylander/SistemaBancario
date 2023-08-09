using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Security;
using SistemaBancario.Resourses;
using BCrypt.Net;


namespace SistemaBancario.Entidades
{
    internal class Conta_Entidade
    {
        // === Atrbutos ===

        #region Numero Campo/Prop
        private int numero = -1;

        [Key]
        [Column(Order = 1)]
        public int Numero { 
            get { return numero; }
            set 
            {
                if(numero != -1)
                    throw new Exception("Não é possível alterar o número da conta.");
                if (value < 1000000 || value > 9999999)
                    throw new Exception("Numero da conta fora formáto válido");
                numero = value;
            }
        }
        #endregion

        #region Agencia Campo/Prop/FK

        // Ficou feio? Sim, mas funciona 👍

        [Key]
        [Column(Order = 2)]
        [ForeignKey(nameof(Agencia))]
        public int AgenciaID { 
            get { return Agencia.Id; }
            set
            {
                if (Agencia != null)
                    throw new Exception("Não é possível alterar a agência assim.");
                using (var contexto = new ConnDB_EF())
                {
                    if (contexto.HasAgencia(value))
                        Agencia = contexto.PesquisaAgencia(value);
                    else
                        throw new Exception("A agência não existe");
                }
            }
        }

        public Agencia_Entidade Agencia = null;
        #endregion

        #region Dono(CPF) Campo/Prop

        private string dono;
        public string Dono { 
            get { return dono; } // Meio que manda no formato certo.(Split não serve)
            set 
            {
                // Falta fazer uma validação para saber se o usuário realmente existe.
                if (!string.IsNullOrEmpty(dono))
                    throw new Exception("Não é possível alterar diretamente o dono da conta.");
                else
                {
                    using (var contexto = new ConnDB_EF())
                    {
                        if (!contexto.HasUsuario(ApenasDigitos(value)))
                            throw new InvalidKeyException("O dono da conta não está cadastrado.");
                        dono = ApenasDigitos(value);
                    }
                }
            } 
        }
        #endregion

        public double? Saldo { get; set; } = 0;

        public double? ValorRendimento { get; set; } = 0;

        public double? Rendimento { get; set; } = 1;

        public double? Variacao { get; set; } = 0;

        #region DataCriação
        private DateTime dataCriacao;

        public DateTime DataCriacao
        {
            get { return dataCriacao; }
            set
            {
                using (var conn = new ConnDB_EF())
                {
                    if (dataCriacao != default(DateTime))
                        throw new Exception("A data de criação não pode ser mudada");
                }
            }
        }


        #endregion

        #region Senha Campo/Prop
        //Obs: Poderia criar uma classe separada para lidar com as senhas MAS não tenho certeza se isso é realmente seguro, preciso estuder mais sobre.

        private string senha;
        public string Senha
        {
            get { return senha; }
            set
            {
                if (!string.IsNullOrEmpty(value)) // Verifica se a nova senha não está vazia
                {
                    senha = CriptografaSenha(value); // Criptografa a nova senha
                }
            }
        }
        #endregion

        // === Métodos ===

        string CriptografaSenha(string input)
        {
            if (!Requisitos_Senha(input))
                throw new Exception("A senha é fraca."); // Exemplo correto: 1234567890Aa
            return BCrypt.Net.BCrypt.HashPassword(input);
        }

        bool Requisitos_Senha(string senha)
        {
            bool flagLength = senha.Length > 7, flagTemMaiuscula = false, flagTemMinuscula = false, flagTemNumero = false;

            foreach (char c in senha)
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


        string ApenasDigitos(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                if (!Char.IsDigit(c))
                    continue;
                sb.Append(c);
            }
            return sb.ToString();
        }

    }
}
