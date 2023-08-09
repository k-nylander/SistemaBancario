using Microsoft.EntityFrameworkCore;
using SistemaBancario.Entidades;

namespace SistemaBancario.Resourses
{
    internal class ConnDB_EF : DbContext
    {
        // Tabelas do Banco
        private DbSet<Conta_Entidade> Conta { get; set; } 
        private DbSet<Usuario_Entidade> Usuario { get; set; }
        private DbSet<Agencia_Entidade> Agencia { get; set; }

        #region Usuário Métodos
        
        // --- Conexão Usuário ---
        public Usuario_Entidade? Login_Usuario(string incpf, string insenha)
        {
            Usuario_Entidade? login = Usuario.FirstOrDefault(u => u.CPF.Equals(incpf));
            if (login == null)
                throw new Exception("Usuário não cadastrado.");
            if (!login.ComparaSenha(insenha))
                throw new Exception("Senha incorreta.");
            return login;
        }
        
        // --- Criação Usuário ---
        
        #region Cadastro
        public Usuario_Entidade Cadastro_Usuario(string icpf, string isenha, string iemail, string inome)
        {
            if (Usuario.Any(u => u.CPF.Equals(icpf)))
                throw new Exception("Usuário já cadastrado.");
            var uCria = new Usuario_Entidade { CPF = icpf, Nome = inome, Email = iemail, Senha = isenha };
            Usuario.Add(uCria);
            SaveChanges();
            return uCria;
        }
        public Usuario_Entidade Cadastro_Usuario(string icpf, string isenha, string iemail, string inome, string itelefone)
        {
            var uCria = new Usuario_Entidade { CPF = icpf, Nome = inome, Email = iemail, Senha = isenha, Telefone = itelefone};
            Usuario.Add(uCria);
            SaveChanges();
            return uCria;
        }
        public Usuario_Entidade Cadastro_Usuario(string icpf, string isenha, string iemail, string inome, string itelefone, string iendereco)
        {
            var uCria = new Usuario_Entidade { CPF = icpf, Nome = inome, Email = iemail, Senha = isenha, Telefone = itelefone, Endereco = iendereco};
            Usuario.Add(uCria);
            SaveChanges();
            return uCria;
        }
        #endregion

        // --- Verificação Conta--- 
        public string[] ContasCadastradas(Usuario_Entidade dono)
        {
            List<string> lista = new List<string>();
            foreach (var acc in Conta.Where(u => u.Dono == dono.CPF).ToArray())
                lista.Add($"{acc.Numero}/{acc.AgenciaID}");

            return lista.ToArray();
        }

        #endregion

        #region Conta Métodos
        
        // --- Conexão Conta ---
        public Conta_Entidade Login_Conta(int inumero, int iagencia, string isenha, Usuario_Entidade idono)
        {
            Conta_Entidade login = Conta.FirstOrDefault(c => c.Dono == idono.CPF && c.Numero == inumero && c.AgenciaID == iagencia) ;
            if (login == null)
                throw new Exception("Conta não existente.");
            if (!login.ComparaSenha(isenha))
                throw new Exception("Senha incorreta.");
            return login;
        }

        // --- Criação Conta

        public Conta_Entidade Cadastro_Conta(Usuario_Entidade idono, string nova_senha) // Apenas criar a conta, sem nenhuma especificação.
        {

            Agencia_Entidade agMenor = Agencia.OrderBy(a => a.ContasCadastradas).FirstOrDefault();

            Random rd = new Random();
            int NovoNumero;
            do
            {
                NovoNumero = rd.Next(1000000, 9999999);
            }
            while (Conta.Any(c => c.Numero == NovoNumero && c.AgenciaID == agMenor.Id));

            var cCria = new Conta_Entidade { Numero = NovoNumero, AgenciaID = agMenor.Id, Dono = idono.CPF, Senha = nova_senha, DataCriacao = DateTime.Now };
            Conta.Add(cCria);
            SaveChanges();
            return cCria;
        }

        public Conta_Entidade Cadastro_Conta(Usuario_Entidade idono, int inumero, string nova_senha)
        {
            using (var contexto = new ConnDB_EF())
            {
                var agenciaDisponivel = contexto.Agencia.FirstOrDefault(ag => !contexto.Conta.Any(c => c.Numero == inumero && c.AgenciaID == ag.Id));

                if (agenciaDisponivel == null)
                    throw new Exception("Numero de conta indisponível.");

                var cCria = new Conta_Entidade { Numero = inumero, AgenciaID = agenciaDisponivel.Id, Dono = idono.CPF, Senha = nova_senha, DataCriacao = DateTime.Now };
                contexto.Conta.Add(cCria);
                contexto.SaveChanges();
                return cCria;
            }
        }

        public Conta_Entidade Cadastro_Conta(Usuario_Entidade idono, int inumero, int iagencia, string nova_senha) // Numero de conta independente da agência.
        {
            if(Conta.Any(c => c.Numero == inumero && c.AgenciaID == iagencia))
                throw new Exception("Numero de conta já cadastrado na agência selecionada.");

            var cCria = new Conta_Entidade { Numero = inumero, AgenciaID = iagencia, Dono = idono.CPF, Senha = nova_senha, DataCriacao = DateTime.Now };
            Conta.Add(cCria);
            SaveChanges();
            return cCria;
        }

        public Conta_Entidade Cadastro_Conta(Usuario_Entidade idono, int inumero, int iagencia, double irendimento, string nova_senha) // Numero de conta independente da agência.
        {
            if (Conta.Any(c => c.Numero == inumero && c.AgenciaID == iagencia))
                throw new Exception("Numero de conta já cadastrado na agência selecionada.");
            if(irendimento < 1 || irendimento > 1.2)
                throw new Exception("Rendimento fora do intervalo permitido.");
            var cCria = new Conta_Entidade { Numero = inumero, AgenciaID = iagencia, Dono = idono.CPF, Senha = nova_senha, DataCriacao = DateTime.Now, Rendimento = irendimento};
            Conta.Add(cCria);
            SaveChanges();
            return cCria;
        }

        public void Fechar_Conta(Usuario_Entidade dono, Conta_Entidade alvo, string confirmar_senha_conta)
        {
            if (dono.CPF != alvo.Dono || !alvo.ComparaSenha(confirmar_senha_conta))
                throw new Exception("Erro ao realizar a ação.");
            if (alvo.Saldo != 0)
                throw new Exception("Antes de fechar a conta é necessário sacar o dinheiro.");

            var novaAg = Agencia.Find(alvo.AgenciaID);
            novaAg.ContasCadastradas--;
            Conta.Remove(alvo);
            SaveChanges();
        }
        #endregion

        // --- Verficiação Geral ---
        public bool HasUsuario(string icpf)
        {
            return Usuario.Any(u => u.CPF.Equals(icpf));
        }
        public bool HasAgencia(int idagencia)
        {
            return Agencia.Any(a => a.Id.Equals(idagencia));
        }

        // --- Informações Publicas ---
        public Agencia_Entidade PesquisaAgencia(int idAg)
        {
            var agPesquisa = Agencia.First(a => a.Id == idAg);
            if (agPesquisa == null)
                throw new Exception("A agencia não exsite.");
            return agPesquisa;

        }

        // --- Conexão com DB ---
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=sistema_bancario;Uid=root;Pwd=sqlkaua;", new MySqlServerVersion(new Version(8, 0, 33)));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conta_Entidade>()
                .HasKey(c => new { c.Numero, c.AgenciaID });
            base.OnModelCreating(modelBuilder);
        }
    }
    // No futuro toda agência deverá ter um usuário dentro do DB para intermediar as trocas de informações entre o DB e o usuário, sendo assim,
    // apenas será possível fazer algumas ações como saque e depósito enquanto puder haver a verificação da agência. 
}
