//Importa o namespace do Entity Framework Core, necessário para configurar o DbContext
using Microsoft.EntityFrameworkCore;
//Importa o namespace onde seus modelos (POCOs) como Usuario e Lancamento estão definidos
using ProjetoGestao.Api.Models;

//Define o namespace para esta classe
namespace ProjetoGestao.Api.Data
{
    //Define a classe AppDbContext, que herda da classe base DbContext do Entity Framework
    //Esta classe é a "ponte" entre seus objetos C# e o banco de dados relacional
    public class AppDbContext : DbContext
    {
        //Este é o construtor da classe. 
        //Ele recebe as opções de configuração (DbContextOptions) que foram injetadas
        //lá no Program.cs
        //O "base(options)" passa essas opções para a classe pai (DbContext)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //Esta propriedade DbSet<Usuario> informa ao Entity Framework que queremos
        //uma tabela chamada "Usuarios" no banco de dados, e que ela será
        //modelada a partir da classe C# "Usuario"
        public DbSet<Usuario> Usuarios { get; set; }

        //Da mesma forma, isto cria uma tabela "Lancamentos"
        //baseada na classe C# "Lancamento"
        public DbSet<Lancamento> Lancamentos { get; set; }

        //Este método (OnModelCreating) é chamado pelo Entity Framework quando ele está
        //"construindo o modelo" (entendendo as tabelas e colunas) pela primeira vez
        //O sobrescrevemos (override) para definir configurações personalizadas,
        //como os relacionamentos (chaves estrangeiras)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Define o relacionamento entre as tabelas
            
            //Inicia a configuração da entidade (tabela) "Lancamento"
            modelBuilder.Entity<Lancamento>()
                //Define que um Lançamento (l) "Tem Um" (HasOne) Usuário
                .HasOne(l => l.Usuario) 
                //Um Usuário (u) "Tem Muitos" (WithMany) Lançamentos
                .WithMany(u => u.Lancamentos)
                //E a chave estrangeira (foreign key) na tabela Lancamento 
                //que aponta para o Usuário é a propriedade "UsuarioId"
                .HasForeignKey(l => l.UsuarioId);
        }
    }
}
