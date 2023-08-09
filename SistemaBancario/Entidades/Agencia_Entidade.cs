using System.ComponentModel.DataAnnotations;

namespace SistemaBancario.Entidades
{
    internal class Agencia_Entidade
    {
        [Key]
        public int Id { get; set; }
        public string Endereco { get; set; }
        public string Gerente { get; set; }
        public int ContasCadastradas { get; set; }
    }
}
