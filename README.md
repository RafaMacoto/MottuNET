# MottuNET API

API RESTful desenvolvida em **ASP.NET Core (.NET 9)** para gerenciamento de **Motos, Alas e Usuários**, com integração ao banco Oracle via **Entity Framework Core**.

---

## Integrantes

- Rafael Macoto - 554992
- Fernando Henrique Aguiar - 557525
- Gabrielly Macedo - 558962

---

## Justificativa da Arquitetura

A aplicação foi desenvolvida seguindo **arquitetura em camadas**, separando responsabilidades:  

- **Controllers**: recebem as requisições HTTP e retornam respostas.  
- **Services**: contêm a lógica de negócio, validações e regras do sistema.  
- **DTOs**: objetos de transferência de dados entre a camada de entrada (Controller) e saída (Service).  
- **Data (AppDbContext)**: camada de persistência utilizando Entity Framework Core para comunicação com o banco Oracle.  

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
