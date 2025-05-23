# MottuNET API

API RESTful desenvolvida em ASP.NET Core (.NET 9) para gerenciamento de Motos e Alas, com integração ao banco Oracle via Entity Framework Core.

## Descrição

Esta API permite criar, consultar, atualizar e deletar motos e alas.  
A entidade `Moto` possui atributos como modelo, status, posição, problema, placa e referência para uma `Ala`.  
A entidade `Ala` contém um nome e uma lista de motos associadas.

## Tecnologias usadas

- .NET 9 (ASP.NET Core Web API com Controllers)  
- Entity Framework Core com Oracle (Oracle.ManagedDataAccess.Core)  
- Swagger para documentação da API  
- Banco de dados Oracle (oracle.fiap.com.br, ORCL, porta 1521)

## Endpoints disponíveis

### Ala

| Método | Endpoint         | Descrição                       | Parâmetros        |
|--------|------------------|--------------------------------|-------------------|
| GET    | /api/alas        | Lista todas as alas             | QueryParams       |
| GET    | /api/alas/{id}   | Busca uma ala pelo ID           | PathParam `id`    |
| POST   | /api/alas        | Cria uma nova ala               | JSON no Body      |
| PUT    | /api/alas/{id}   | Atualiza uma ala existente      | PathParam `id`, JSON no Body |
| DELETE | /api/alas/{id}   | Deleta uma ala pelo ID          | PathParam `id`    |

### Moto

| Método | Endpoint          | Descrição                      | Parâmetros          |
|--------|-------------------|-------------------------------|---------------------|
| GET    | /api/motos        | Lista todas as motos           | QueryParams         |
| GET    | /api/motos/{id}   | Busca uma moto pelo ID         | PathParam `id`      |
| POST   | /api/motos        | Cria uma nova moto             | JSON no Body        |
| PUT    | /api/motos/{id}   | Atualiza uma moto existente    | PathParam `id`, JSON no Body |
| DELETE | /api/motos/{id}   | Deleta uma moto pelo ID        | PathParam `id`      |

## Como rodar o projeto

### Pré-requisitos

- .NET 9 SDK instalado  
- Banco de dados Oracle acessível (configurar string de conexão no `appsettings.json`)  
- Visual Studio 2022 ou outro editor de sua preferência

### Passos

1. Clone o repositório:
   ```bash
   git clone https://github.com/RafaMacoto/MottuNET.git

   cd MottuNet

   dotnet ef database update

   dotnet run

   https://localhost:5001/swagger
