# Projeto Integrador: Plataforma de Gestão de Despesas Pessoais (Prova de Conceito)

Este repositório contém o código-fonte da Prova de Conceito (PoC) para o Projeto Integrador do curso de Desenvolvimento de Sistemas Orientado a Dispositivos Móveis e Baseados na Web.
O objetivo do projeto é desenvolver uma plataforma de gestão de despesas pessoais que apoie os usuários no processo de controle e planejamento financeiro individual. Esta PoC foca nas funcionalidades essenciais de cadastro/login, registro de receitas/despesas, e visualização de saldo e gráficos.

## Autores do Projeto
* Alexandre Gomes Pinto 
* Felipe Santos Souza 
* Hudson Godoy Verdum Carrazzoni
* Leonardo Gonçalves Leal 
* Letícia Camperoni 
* Nícolas Vogt da Rosa Almeida de Oliveira 
* Sidnei Félix Lopes Vieira 

## Tecnologias Utilizadas

* **Backend:** C#, ASP.NET Core, Entity Framework Core
* **Frontend:** HTML5, CSS3, JavaScript (ES6+)
* **Banco de Dados:** SQLite
* **Ferramentas:** Visual Studio Code, Git, .NET SDK

## Pré-requisitos

Antes de começar, garanta que você tenha as seguintes ferramentas instaladas:

* [.NET SDK](https://dotnet.microsoft.com/download) (versão 6.0 ou superior)
* [Visual Studio Code](https://code.visualstudio.com/)
* A extensão [Live Server](https://marketplace.visualstudio.com/items?itemName=ritwickdey.LiveServer) para o VS Code.
* [Git](https://git-scm.com/downloads)

## Como Executar o Projeto

Siga estes passos para configurar e executar a aplicação em seu ambiente local.

### 1. Clonar o Repositório

Abra seu terminal, navegue até o diretório onde deseja salvar o projeto e execute:

#### Clone o repositório
> git clone <url-do-seu-repositorio-github>

#### Entre na pasta do projeto
> cd ProjetoGestaoFinanceira

### 2. Configuração do Backend (Carga de Scripts)
O backend é responsável por toda a lógica de negócio e pela comunicação com o banco de dados.
#### Navegue até a pasta da API
> cd backend/ProjetoGestao.Api

#### Restaure os pacotes do .NET
> dotnet restore

#### Instale a ferramenta de linha de comando do Entity Framework (se ainda não tiver)
> dotnet tool install --global dotnet-ef

#### Execute as "migrations" para criar o banco de dados SQLite (o arquivo gestao.db). Este é o passo de "carga de scripts" do banco.
> dotnet ef database update

### 3. Executando o Backend (Servidor 1)
Após a configuração, ainda no diretório backend/ProjetoGestao.Api, inicie o servidor da API:

#### Inicie o servidor
> dotnet run

O terminal mostrará a URL que o servidor está usando. Anote esta URL (ex: http://localhost:5085). Você precisará dela no próximo passo.

### 4. Configuração do Frontend
O frontend precisa saber onde o backend está rodando.
Abra a pasta ProjetoGestaoFinanceira no Visual Studio Code.
Abra o arquivo frontend/js/main.js.
Verifique se a constante API_URL no topo do arquivo bate com a URL do seu backend.

// Exemplo:
const API_URL = 'http://localhost:5085'; // Garanta que esta porta está correta

Faça a mesma verificação no arquivo frontend/js/dashboard.js.

### 5. Executando o Frontend (Servidor 2)
Com o backend já rodando, vamos iniciar o servidor do frontend:
No VS Code, clique com o botão direito no arquivo frontend/index.html.

Selecione a opção "Open with Live Server".

Isso abrirá automaticamente o seu navegador na página de cadastro (ex: http://127.0.0.1:5500), e a aplicação estará pronta para uso.
