## Funcionalidades: 
  - CRUD de clientes
  - Cada cliente possui um único endereço
  - Logs com Serilo
  - Documentação com Swagger

## Tecnologias Utilizadas

  - ASP.NET core
  - Entitty FrameworkCore (InMemory)
  - DDD (Domain, Application, Infrastructure, API)
  - AutoMapper
  - Serilog
  - Swagger

## Estrutura do Projeto

**API:**
    Contém os controllers e configurações da aplicação, como injeção de dependências, Swagger, Serilog e validações.

**Application:** 
    Inclui os DTOs, validadores com FluentValidation e serviços de aplicação que orquestram regras e interações entre camadas.

**Domain:**
    Define as entidades de negócio (como Cliente e Endereço), interfaces de repositório e quaisquer regras de domínio puras.

**Infrastructure:**
    Implementa os repositórios, o DbContext com EF Core (In-Memory) e a persistência de dados.

**Program.cs:**
    Ponto de entrada da aplicação, responsável pela configuração dos middlewares, injeção de dependência.

## Pré-requisítos

- [.NET 8](https://dotnet.microsoft.com/download)

##  Como executar o Projeto
##### 1. Restaurar dependencias
```bash
dotnet restore
```
##### 2. Compilar o projeto
```bash
dotnet build
```

##### 3. Executar a aplicação
```bash
dotnet run --project ClienteApi.API
```

##### 4. A API estará disponível em:
https://localhost:7296
http://localhost:5213



## Documentação com Swagger
Após executar o projeto, abre o navegador e acesse:
https://localhost:7296/swagger/index.html
ou
http://localhost:5213/swagger/index.html


### Autor -> **Julio Silva.**✌️
