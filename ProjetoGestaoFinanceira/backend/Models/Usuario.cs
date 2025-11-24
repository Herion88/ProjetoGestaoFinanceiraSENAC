using System.Collections.Generic;

namespace ProjetoGestao.Api.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        // Nome do usuário, usado para identificação dentro do sistema
        public string Nome { get; set; }

        // E-mail usado no cadastro simples
        public string Email { get; set; }

        // Relação: um usuário pode ter vários lançamentos financeiros
        public ICollection<Lancamento> Lancamentos { get; set; } = new List<Lancamento>();
    }
}
