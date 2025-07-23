# FiotecProva - API de Gerenciamento de Consultas Médicas

API RESTful desenvolvida em .NET 9 com arquitetura em camadas, responsável pelo gerenciamento de usuários, médicos, pacientes e consultas médicas. Conta com logs estruturados com Serilog, integração com ViaCEP e persistência de dados em SQL Server via Docker.

---

## Tecnologias Utilizadas

- ASP.NET Core 9.0
- Entity Framework Core
- SQL Server (via Docker)
- Serilog para logging estruturado
- FluentValidation
- Swagger (Swashbuckle)
- ViaCEP (integração externa)

---

## Como executar localmente

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

### 1. Subir o ambiente com Docker Compose

O projeto já inclui um arquivo `docker-compose.yml` para subir o SQL Server automaticamente:
docker-compose up -d

### 2. Restaurar pacotes e aplicar Migration

dotnet restore
dotnet ef database update --project FiotecProva.Infra.Data --startup-project FiotecProva.API

### 3. Executar API

cd FiotecProva.API
dotnet run


---


### Importante

É necessário executar o arquivo SQL localizado no caminho: FiotecProva.Infra.Data/Script, antes de utilizar os endpoints. Em seguida, será possível os testes dos métodos POST.
insertPerfis.sql: Criação manual de perfis.


