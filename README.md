# MottuNET API

[![.NET](https://img.shields.io/badge/.NET-9-informational?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Oracle](https://img.shields.io/badge/Oracle-DB-orange?logo=oracle&logoColor=white)](https://www.oracle.com/)
[![Swagger](https://img.shields.io/badge/Swagger-API-blue?logo=swagger&logoColor=white)](http://localhost:5237/swagger)

API RESTful desenvolvida em **ASP.NET Core (.NET 9)** para gerenciamento de **Motos, Alas e Usuários**, com integração ao banco Oracle via **Entity Framework Core**.

---

## Integrantes

- Rafael Macoto

---

## Entidades principais e justificativa do domínio

O sistema possui três entidades principais:  

1. **Usuário** – controla quem realiza ações no sistema.  
2. **Ala** – organiza motos em setores, facilitando gerenciamento e localização.  
3. **Moto** – representa o objeto central, com atributos como modelo, status, posição, problema e placa.  

**Justificativa:** essas entidades refletem o domínio de **gerenciamento operacional de motos em alas**, permitindo organização clara, regras de negócio bem definidas e escalabilidade futura (ex.: histórico de manutenção, relatórios, notificações).

---

## Justificativa da Arquitetura

A aplicação foi desenvolvida seguindo **arquitetura em camadas**, separando responsabilidades:  

- **Controllers**: recebem requisições HTTP e retornam respostas.  
- **Services**: contêm lógica de negócio e validações.  
- **DTOs**: transferência de dados entre Controller e Service.  
- **Data (AppDbContext)**: persistência via Entity Framework Core com Oracle.  

Essa arquitetura facilita **manutenção, testes unitários e escalabilidade**, além de permitir integração fácil com Swagger/OpenAPI para documentação.

---

## Tecnologias usadas

- .NET 9 (ASP.NET Core Web API)  
- Entity Framework Core com Oracle (`Oracle.ManagedDataAccess.Core`)  
- Swagger / Swashbuckle para documentação da API  
- Swagger Examples (`Swashbuckle.AspNetCore.Filters`)  
- Banco de dados Oracle  
- Git para versionamento de código  

---

## Endpoints disponíveis

### Usuário

| Método | Endpoint            | Descrição                       | Parâmetros          |
|--------|-------------------|--------------------------------|-------------------|
| GET    | /api/usuario       | Lista todos os usuários         | -                 |
| GET    | /api/usuario/{id}  | Busca um usuário pelo ID        | PathParam `id`    |
| POST   | /api/usuario       | Cria um novo usuário            | JSON no Body      |
| PUT    | /api/usuario/{id}  | Atualiza um usuário existente   | PathParam `id`, JSON no Body |
| DELETE | /api/usuario/{id}  | Deleta um usuário pelo ID       | PathParam `id`    |

### Ala

| Método | Endpoint         | Descrição                       | Parâmetros        |
|--------|-----------------|---------------------------------|------------------|
| GET    | /api/alas       | Lista todas as alas             | -                |
| GET    | /api/alas/{id}  | Busca uma ala pelo ID           | PathParam `id`   |
| POST   | /api/alas       | Cria uma nova ala               | JSON no Body     |
| PUT    | /api/alas/{id}  | Atualiza uma ala existente      | PathParam `id`, JSON no Body |
| DELETE | /api/alas/{id}  | Deleta uma ala pelo ID          | PathParam `id`   |

### Moto

| Método | Endpoint          | Descrição                      | Parâmetros          |
|--------|------------------|--------------------------------|---------------------|
| GET    | /api/motos       | Lista todas as motos           | QueryParams (`modelo`, `status`, `alaId`) |
| GET    | /api/motos/{id}  | Busca uma moto pelo ID         | PathParam `id`      |
| POST   | /api/motos       | Cria uma nova moto             | JSON no Body        |
| PUT    | /api/motos/{id}  | Atualiza uma moto existente    | PathParam `id`, JSON no Body |
| DELETE | /api/motos/{id}  | Deleta uma moto pelo ID        | PathParam `id`      |

---

## Exemplos de uso dos endpoints

### Criar Usuário

```json
POST /api/usuario
{
  "nome": "Rafael Macoto",
  "email": "rafael@example.com",
  "senha": "senha123"
}
Criar Moto
json
Copiar código
POST /api/motos
{
  "modelo": "Honda CG",
  "status": "Disponivel",
  "posicao": "Estacionamento A",
  "problema": null,
  "placa": "ABC1234",
  "alaId": 1
}
Criar Ala
json
Copiar código
POST /api/alas
{
  "nome": "Ala Norte"
}
Para mais exemplos, utilize o Swagger UI disponível em /swagger.

Como rodar a API
Pré-requisitos
.NET 9 SDK instalado

Banco de dados Oracle acessível (configurar string de conexão no appsettings.json)

Visual Studio 2022, VS Code ou outro editor de preferência

Passos
Clone o repositório:

bash
Copiar código
git clone https://github.com/RafaMacoto/MottuNET.git
cd MottuNET
Atualize o banco de dados via Entity Framework:

bash
Copiar código
dotnet ef database update
Execute a aplicação:

bash
Copiar código
dotnet run
Acesse o Swagger UI:

bash
Copiar código
http://localhost:5237/swagger
