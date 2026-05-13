# VinciOrders API

API REST para cadastro e consulta de pedidos, desenvolvida como parte do processo seletivo da Vinci Energies.

## 🚀 Tecnologias Utilizadas

- [.NET 10](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [SQLite](https://www.sqlite.org/)
- [Swagger / Swashbuckle](https://swagger.io/)
- [xUnit](https://xunit.net/) — testes unitários
- [Moq](https://github.com/moq/moq4) — simulação de dependências nos testes

## 📁 Estrutura do Projeto

```
VinciOrders.API/
├── Controllers/     # Recebe as requisições HTTP e devolve respostas
├── Services/        # Regras de negócio e validações
├── Repositories/    # Acesso ao banco de dados
├── Models/          # Representação das entidades do banco
├── DTOs/            # Objetos de entrada e saída da API
├── Data/            # Configuração do Entity Framework
└── Migrations/      # Histórico de versões do banco de dados

VinciOrders.Tests/
└── OrderServiceTests.cs  # Testes unitários do serviço
```
## ⚙️ Como Rodar o Projeto

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Git

### Passo a passo

1. Clone o repositório:
```bash
git clone https://github.com/seu-usuario/VinciOrders.git
cd VinciOrders/VinciOrders.API
```

2. Restaure as dependências:
```bash
dotnet restore
```

3. Rode a aplicação:
```bash
dotnet run
```

4. Acesse o Swagger no navegador:
http://localhost:5000/swagger

> O banco de dados SQLite é criado e as migrations são aplicadas automaticamente na primeira execução. Nenhuma configuração adicional é necessária.

## 🧪 Como Rodar os Testes

```bash
cd VinciOrders.Tests
dotnet test
```

## 📌 Endpoints

### POST /orders
Cria um novo pedido.

**Body:**
```json
{
  "customerName": "Carlos Silva",
  "value": 150.00
}
```

**Resposta de sucesso:** `201 Created`

---

### GET /orders
Retorna a lista de todos os pedidos.

**Resposta de sucesso:** `200 OK`

---

### GET /orders/{id}
Retorna um pedido específico pelo Id.

**Resposta de sucesso:** `200 OK`

**Pedido não encontrado:** `404 Not Found`

## ✅ Regras de Negócio

- O nome do cliente é obrigatório
- O valor do pedido deve ser maior que zero
- O Id e a data são gerados automaticamente pelo sistema

## 🏗️ Arquitetura

O projeto segue o padrão de camadas:

- **Controller** → recebe a requisição HTTP e devolve a resposta
- **Service** → aplica as regras de negócio e validações
- **Repository** → acessa o banco de dados via Entity Framework

## 🐳 Docker

Para rodar com Docker:

```bash
docker build -t vinciorders .
docker run -p 5000:5000 vinciorders
```