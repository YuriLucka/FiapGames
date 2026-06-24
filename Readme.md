# FIAP Cloud Games — FCG API

API REST desenvolvida em **.NET 8** para a plataforma **FIAP Cloud Games (FCG)**, uma plataforma de venda de jogos digitais e gestão de servidores para partidas online. Este projeto é o **Tech Challenge — Fase 1** da pós-graduação em Desenvolvimento de Sistemas .NET na FIAP.

---

## 🎯 Objetivo

Desenvolver um MVP de uma API REST para gerenciamento de usuários e biblioteca de jogos, servindo de base para as próximas fases do projeto. O sistema garante persistência de dados, qualidade de software e boas práticas de desenvolvimento com DDD.

---

## 🏗️ Arquitetura

O projeto segue os princípios de **Domain-Driven Design (DDD)** em uma arquitetura **monolítica**, organizada em 4 camadas:

```
FiapCloudGames.sln
├── FiapCloudGames.Domain          # Entidades, Value Objects, Interfaces, Exceções
├── FiapCloudGames.Application     # DTOs, Services, Interfaces de serviço
├── FiapCloudGames.Infrastructure  # EF Core, Repositórios, JWT, Migrations
└── FiapCloudGames.API             # Controllers, Middleware, Program.cs, Swagger
```

### Regra de dependência
```
API → Application + Infrastructure
Application → Domain
Infrastructure → Domain + Application
Domain → (nenhuma dependência externa)
```

---

## 🚀 Tecnologias

| Tecnologia | Versão | Uso |
|---|---|---|
| .NET | 8.0 | Framework principal |
| ASP.NET Core | 8.0 | Web API (Controllers MVC) |
| Entity Framework Core | 8.0 | ORM e Migrations |
| SQL Server | - | Banco de dados relacional |
| JWT Bearer | 8.0 | Autenticação e Autorização |
| BCrypt.Net-Next | 4.1.0 | Hash de senhas |
| Swagger / Swashbuckle | 6.6.2 | Documentação da API |
| xUnit | 2.9.0 | Framework de testes unitários |
| Moq | 4.20.72 | Mock de dependências nos testes |

---

## ⚙️ Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (ou SQL Server Express / LocalDB)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

---

## 🔧 Como rodar o projeto

### 1. Clone o repositório

```bash
git clone https://github.com/YuriLucka/FiapGames.git
cd FiapGames
```

### 2. Configure o banco de dados

Abra o arquivo `FiapCloudGames.API/appsettings.json` e ajuste a `ConnectionString` conforme sua instância do SQL Server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=FiapCloudGames;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "JwtSettings": {
    "SecretKey": "fiap-cloud-games-secret-key-minimo-32-chars!",
    "Issuer": "FiapCloudGames.API",
    "Audience": "FiapCloudGames.Client",
    "ExpirationMinutes": "60"
  }
}
```

> **Exemplos de `Server` por ambiente:**
> - SQL Server Express: `localhost\\SQLEXPRESS`
> - LocalDB: `(localdb)\\MSSQLLocalDB`
> - SQL Server padrão: `localhost`

### 3. Aplique as Migrations

No **Package Manager Console** do Visual Studio:

```powershell
Update-Database -Project FiapCloudGames.Infrastructure -StartupProject FiapCloudGames.API
```

Ou via CLI:

```bash
dotnet ef database update --project FiapCloudGames.Infrastructure --startup-project FiapCloudGames.API
```

### 4. Execute a API

Pressione **F5** no Visual Studio com o projeto `FiapCloudGames.API` como startup, ou via CLI:

```bash
dotnet run --project FiapCloudGames.API
```

A API será iniciada e o **Swagger UI** abrirá automaticamente em:

```
https://localhost:7126/
```

---

## 👤 Usuário Administrador padrão (Seed)

Ao iniciar a aplicação pela primeira vez, um usuário administrador é criado automaticamente:

| Campo | Valor |
|---|---|
| E-mail | `admin@fcg.com` |
| Senha | `Admin@123` |
| Role | `Admin` |

---

## 🔐 Autenticação

A API utiliza **JWT Bearer Token**. Para acessar endpoints protegidos:

1. Faça login em `POST /api/auth/login` com as credenciais
2. Copie o `accessToken` retornado
3. No Swagger, clique em **Authorize 🔒**
4. Cole **apenas o token** (sem o prefixo `Bearer`)
5. Clique em **Authorize**

### Níveis de acesso

| Role | Permissões |
|---|---|
| `User` | Acesso à plataforma e biblioteca de jogos |
| `Admin` | Cadastrar jogos, administrar usuários e criar promoções |

---

## 📋 Endpoints

### Auth
| Método | Rota | Acesso | Descrição |
|---|---|---|---|
| POST | `/api/auth/login` | Público | Realiza login e retorna JWT |

### Usuários
| Método | Rota | Acesso | Descrição |
|---|---|---|---|
| POST | `/api/user` | Público | Cadastra novo usuário |
| GET | `/api/user/{id}` | Autenticado | Busca usuário por Id |
| GET | `/api/user` | Admin | Lista todos os usuários |
| PUT | `/api/user/{id}` | Autenticado | Atualiza dados do usuário |
| DELETE | `/api/user/{id}` | Admin | Remove usuário |
| PATCH | `/api/user/{id}/promote` | Admin | Promove usuário para Admin |

### Jogos
| Método | Rota | Acesso | Descrição |
|---|---|---|---|
| GET | `/api/game` | Autenticado | Lista todos os jogos |
| GET | `/api/game/{id}` | Autenticado | Busca jogo por Id |
| POST | `/api/game` | Admin | Cadastra novo jogo |
| PUT | `/api/game/{id}` | Admin | Atualiza jogo |
| DELETE | `/api/game/{id}` | Admin | Remove jogo |
| POST | `/api/game/{gameId}/acquire/{userId}` | Autenticado | Adiciona jogo à biblioteca do usuário |

---

## 🧪 Testes

O projeto possui **45 testes unitários** cobrindo as principais regras de negócio, organizados em:

```
FiapCloudGames.Tests/
├── Domain/
│   ├── UserEntityTests.cs   # Testes TDD da entidade User (13 testes)
│   └── GameEntityTests.cs   # Testes TDD da entidade Game (7 testes)
└── Application/
    ├── UserServiceTests.cs  # Testes do UserService com Moq (8 testes)
    └── GameServiceTests.cs  # Testes do GameService com Moq (7 testes)
```

### Executar os testes

No Visual Studio: **Test → Run All Tests** (`Ctrl+R, A`)

Ou via CLI:

```bash
dotnet test
```

### Resultado esperado
```
Testes em grupo: 45
Duração total: ~2.6s
Resultados: 45 Aprovado
```

---

## 🗂️ Regras de negócio implementadas

### Usuário
- Nome obrigatório
- E-mail com formato válido e único no sistema
- Senha com mínimo 8 caracteres, contendo letras, números e caracteres especiais
- Dois níveis de acesso: `User` e `Admin`
- Biblioteca de jogos adquiridos — não permite duplicatas

### Jogo
- Título obrigatório
- Preço não pode ser negativo
- Controle de ativo/inativo pelo Administrador
- Apenas jogos ativos podem ser adquiridos por usuários

---

## 🗃️ Estrutura do banco de dados

| Tabela | Descrição |
|---|---|
| `Users` | Dados dos usuários (Id, Name, Email, PasswordHash, Role, CreatedAt) |
| `Games` | Catálogo de jogos (Id, Title, Description, Price, IsActive, CreatedAt) |
| `UserGames` | Tabela intermediária — biblioteca de jogos adquiridos por usuário |

---

## 📁 Estrutura do projeto

```
FiapCloudGames/
├── FiapCloudGames.Domain/
│   ├── Entities/
│   │   ├── User.cs
│   │   └── Game.cs
│   ├── Enums/
│   │   └── UserRole.cs
│   ├── ValueObjects/
│   │   └── Email.cs
│   ├── Interfaces/
│   │   ├── IUserRepository.cs
│   │   └── IGameRepository.cs
│   └── Exceptions/
│       └── DomainException.cs
├── FiapCloudGames.Application/
│   ├── DTOs/
│   ├── Interfaces/
│   └── Services/
├── FiapCloudGames.Infrastructure/
│   ├── Data/
│   │   ├── AppDbContext.cs
│   │   ├── Mappings/
│   │   └── Migrations/
│   ├── Repositories/
│   ├── Security/
│   └── InfrastructureExtensions.cs
├── FiapCloudGames.API/
│   ├── Controllers/
│   ├── Middleware/
│   ├── Program.cs
│   └── appsettings.json
└── FiapCloudGames.Tests/
    ├── Domain/
    └── Application/
```

---

## 👥 Grupo

| Nome   | Username Discord            |
|--------|-----------------------------|
| Yuri   | Yuri Lucka - RM371938       |
| Rafael | Rafael Gagliard - RM371921  |
| Carlos | CarlosEM_ - RM371683        |
| Pedro  | Pedro Custodio - RM373420   |


---

## 🔗 Links

- 📁 Repositório: *https://github.com/YuriLucka/FiapGames*
- 📊 Documentação DDD (Event Storming): *https://miro.com/app/board/uXjVHfBBYHk=/*
- 🎥 Vídeo de apresentação: *https://www.youtube.com/watch?v=6V_RFjVrsLA*
