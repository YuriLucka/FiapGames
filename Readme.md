# FIAP Cloud Games - API de Usuários e Biblioteca de Jogos

## Sobre o Projeto

Este projeto foi desenvolvido como parte do Tech Challenge da FIAP (Fase 1), com o objetivo de construir a base inicial da plataforma **FIAP Cloud Games (FCG)**.

A FCG será uma plataforma voltada à venda de jogos digitais e gerenciamento de servidores para partidas online. Nesta fase, o foco está na criação de uma API REST responsável pelo gerenciamento de usuários e suas bibliotecas de jogos adquiridos.

O sistema foi projetado como um MVP (Minimum Viable Product), garantindo escalabilidade e preparação para futuras evoluções do projeto.

---

## Objetivo da Fase

Desenvolver uma API REST em .NET 8 capaz de:

* Gerenciar usuários
* Controlar biblioteca de jogos adquiridos
* Implementar autenticação e autorização
* Garantir persistência de dados
* Aplicar boas práticas de desenvolvimento

---

## Arquitetura

O sistema segue o modelo de **arquitetura monolítica**, conforme definido para esta fase, visando maior agilidade no desenvolvimento.

A organização do projeto segue princípios de separação de responsabilidades e boas práticas de desenvolvimento.

---

## Funcionalidades

### Cadastro de Usuários

* Cadastro com nome, e-mail e senha
* Validação de e-mail
* Senha com no mínimo 8 caracteres, incluindo:

  * Letras
  * Números
  * Caracteres especiais

### Autenticação e Autorização

* Autenticação via token JWT
* Dois níveis de acesso:

  * Usuário: acesso à plataforma e biblioteca
  * Administrador: gerenciamento de usuários, jogos e promoções

### Biblioteca de Jogos

* Associação de jogos aos usuários
* Consulta de jogos adquiridos

---

## Requisitos Técnicos

### API

* Desenvolvida em .NET 8
* Implementação utilizando:

  * Controllers (MVC) ou Minimal API
* Documentação com Swagger
* Middleware para tratamento de erros e logs

### Persistência de Dados

* Entity Framework Core
* Migrations para controle de banco de dados

### (Opcional)

* MongoDB como alternativa de persistência
* Dapper para consultas otimizadas
* GraphQL para consultas avançadas

---

## Qualidade de Software

* Implementação de testes unitários
* Aplicação de TDD ou BDD em pelo menos um módulo

---

## Domain-Driven Design (DDD)

* Modelagem de domínio baseada em:

  * Event Storming
* Organização das entidades seguindo princípios de DDD

---

## Estrutura do Projeto

/Projeto
│
├── Domain
├── Application
├── Infrastructure
├── API
├── Tests
└── README.md

---

## Como Executar o Projeto

### Pré-requisitos

* .NET 8 instalado
* Banco de dados configurado (SQL Server ou outro)

### Passos

git clone https://github.com/seu-usuario/seu-repositorio.git
cd nome-do-projeto
dotnet ef database update
dotnet run

---

## Documentação

A API possui documentação interativa via Swagger, disponível ao executar o projeto.

---

## Entregáveis da Fase

* API funcional com os requisitos implementados
* Testes unitários
* Documentação DDD (Event Storming e diagramas)
* README completo
* Vídeo demonstrativo (até 15 minutos)
* Relatório de entrega contendo:

  * Integrantes do grupo
  * Links do repositório
  * Link da documentação
  * Link do vídeo

---

## Evolução do Projeto

Este projeto servirá como base para as próximas fases, podendo evoluir para:

* Sistema de matchmaking
* Gerenciamento de servidores de jogos
* Plataforma completa de distribuição digital

---

## Autor

Yuri
Rafael
Carlos
Pedro
Gustavo

---

## Licença

Projeto desenvolvido para fins acadêmicos (FIAP).
