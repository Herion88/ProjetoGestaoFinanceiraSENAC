namespace backend.Models
{
    public class Lancamento
    {
        public int Id { get; set; }

        // Tipo do lançamento: "Receita" ou "Despesa"
        public string Tipo { get; set; }

        // Valor do lançamento financeiro
        public decimal Valor { get; set; }

        // Categoria simples, como alimentação, transporte ou lazer
        public string Categoria { get; set; }

        // Data em que o lançamento foi registrado
        public DateTime Data { get; set; }

        // Texto opcional para descrever o gasto ou a receita
        public string Descricao { get; set; }

        // Chave estrangeira ligando o lançamento ao usuário dono dele
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
