//Importa o namespace do Entity Framework Core, necessário para configurar o DbContext
using Microsoft.EntityFrameworkCore;
//Importa o namespace onde a classe AppDbContext está definida
using ProjetoGestao.Api.Data;

//Cria um construtor de aplicação web (WebApplicationBuilder), que é usado para configurar os serviços e o pipeline da aplicação
var builder = WebApplication.CreateBuilder(args);

//Adiciona e configura os serviços de CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    //Define uma nova política de CORS chamada "AllowFrontend"
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            //Especifica qual origem (URL do frontend) tem permissão para acessar esta API
            policy.WithOrigins("http://127.0.0.1:5500") 
                  //Permite que o frontend envie quaisquer tipos de cabeçalhos HTTP (ex: Content-Type, Authorization)
                  .AllowAnyHeader()
                  //Permite que o frontend use quaisquer métodos HTTP (ex: GET, POST, PUT, DELETE)
                  .AllowAnyMethod();
        });
});

//Adiciona o contexto do banco de dados (AppDbContext) ao contêiner de injeção de dependência
builder.Services.AddDbContext<AppDbContext>(options =>
    //Configura o DbContext para usar o SQLite como provedor de banco de dados
    //A string de conexão "DefaultConnection" é lida do arquivo de configuração (ex: appsettings.json)
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

//Adiciona os serviços necessários para usar controladores (classes que definem os endpoints da API)
builder.Services.AddControllers()
    //Adiciona opções personalizadas para a serialização de JSON
    .AddJsonOptions(options =>
    {
        // Configura o serializador para ignorar referências cíclicas ao converter objetos para JSON
        // por exemplo, um objeto A referencia B, e B referencia A
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

//Adiciona os serviços do "API Explorer", que ajuda a descobrir os endpoints da API (usado pelo Swagger)
builder.Services.AddEndpointsApiExplorer();
//Adiciona o gerador do Swagger, que cria a documentação da API automaticamente
builder.Services.AddSwaggerGen();

//Constrói a aplicação (WebApplication) com todos os serviços configurados acima
var app = builder.Build();

//Verifica se o ambiente atual é o de "Desenvolvimento"
if (app.Environment.IsDevelopment())
{
    //Habilita o middleware do Swagger, que gera o arquivo JSON da documentação
    app.UseSwagger();
    //Habilita o middleware do Swagger UI, que fornece uma interface gráfica no navegador para visualizar e testar a API
    app.UseSwaggerUI();
}

//Aplica a política de CORS "AllowFrontend" a todas as requisições que chegam na API
app.UseCors("AllowFrontend");

//Habilita o middleware de autorização, que verifica permissões de acesso aos endpoints
app.UseAuthorization();

//Mapeia as rotas definidas nos seus arquivos de Controlador para que a API saiba qual método executar para cada URL
app.MapControllers();

//Inicia a aplicação
app.Run();
