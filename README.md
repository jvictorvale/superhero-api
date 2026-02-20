# 🦸‍♂️ SuperHero API

Esta é uma API robusta e escalável para o gerenciamento de Super-Heróis e seus respectivos superpoderes. O projeto foi desenvolvido utilizando as tecnologias mais recentes do ecossistema **.NET 8** e segue princípios de **Clean Architecture** e **DDD (Domain-Driven Design)**.

## 🚀 Tecnologias Utilizadas

* **Runtime:** .NET 8 com C# 12
* **Framework Web:** ASP.NET Core Web API
* **ORM:** Entity Framework Core (EF Core)
* **Banco de Dados:** MySQL
* **Validação:** FluentValidation (Validações de domínio)
* **Documentação:** Swagger (OpenAPI)
* **Arquitetura:** Camadas (Api, Application, Domain e Infrastructure)

## 🏗️ Estrutura do Projeto

A solução está organizada para garantir a separação de responsabilidades (SoC):

* **SuperHero.Api**: Ponto de entrada da aplicação, contendo os Controllers e a configuração de Dependency Injection.
* **SuperHero.Application**: Camada de serviço, DTOs (Data Transfer Objects) e lógica de orquestração.
* **SuperHero.Domain**: O "coração" da aplicação. Contém as Entidades (`Heroi`, `Superpoder`), Interfaces, Validators e a lógica de negócio principal.
* **SuperHero.Infrastructure**: Implementação técnica, acesso ao banco de dados (Context), Mappings (Fluent API) e Migrations.

## 🛡️ Entidades e Regras de Negócio

O sistema utiliza uma relação **Many-to-Many** entre Heróis e Superpoderes, gerenciada pela entidade de ligação `HeroisSuperpoderes`. As entidades de domínio são responsáveis por sua própria validação, garantindo que nenhum dado inválido seja persistido.

Para garantir a consistência dos dados, foram aplicadas as seguintes validações:
* **Unicidade de Herói**: Não é permitido o cadastro ou atualização de heróis com o mesmo `Nome` ou `NomeHeroi` já existentes no sistema.
* **Unicidade de Superpoder**: O sistema impede o cadastro de superpoderes duplicados.
* **Mensagens Personalizadas**: Retornos claros via API informando exatamente qual regra de negócio foi violada, utilizando o padrão de **Notifications**.

## ⚙️ Como Executar o Projeto

**Pré-requisitos**
* SDK do .NET 8
* Instância do MySQL instalada e em execução

**Passo a passo**

**Clone o repositório:**
```bash
git clone https://github.com/seu-usuario/superhero-api.git
cd super-hero-api
```

**Configure a String de Conexão:**

No arquivo appsettings.json dentro do projeto SuperHero.Api, ajuste a conexão com seu MySQL:
```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;user=root;password={senha};database=SuperHeroDB"
}
```

**Execute as Migrations:**
No terminal, dentro da pasta raiz:
```bash
dotnet ef database update --project src/SuperHero.Infrastructure --startup-project src/SuperHero.Api
```

**Rode a API:**
```bash
dotnet run --project src/SuperHero.Api
```

**Acesse o Swagger:**

A documentação estará disponível em: http://localhost:44388/swagger (ou na porta configurada).

**Interface do Swagger documentando os endpoints de Herói e Superpoder**

<img width="1511" height="879" alt="image" src="https://github.com/user-attachments/assets/b6c45094-462e-48fb-92e9-fe246257766d" />

## 🚀 Possíveis Melhorias (Backlog)

Embora o projeto cumpra todos os requisitos solicitados, em um cenário de produção real, eu implementaria:

1. **Testes Unitários e de Integração**: Implementação de testes usando xUnit e Moq para garantir a estabilidade das regras de negócio no Domain e Application.
2. **Autenticação e Autorização**: Implementação de login seguro utilizando JWT (JSON Web Tokens) e Refresh Tokens.
3. **Logs Estruturados**: Adição do Serilog para monitoramento de erros e métricas da aplicação em tempo real.
4. **Dockerização**: Criação de um `docker-compose.yml` para subir a API e o banco MySQL com um único comando.
5. **Caching**: Implementação de Redis para consultas de superpoderes, já que são dados que mudam com pouca frequência.
6. **Notificações em Real-time**: Uso de SignalR para atualizar a lista de heróis instantaneamente quando um novo for cadastrado por outro usuário.

