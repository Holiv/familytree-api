# FamilyTree API

## 📖 Visão Geral

O **FamilyTree API** é um projeto em .NET 8 que tem como objetivo construir uma plataforma para gerenciamento de árvores genealógicas, registros familiares, narrativas, assinaturas e marketplace de produtos/serviços relacionados.

Esta é a **primeira versão (MVP)**

## 🚀 Funcionalidades principais

- **Gerenciamento de Pessoas**
  - Criar, atualizar, obter e deletar pessoas.
  - Obter filhos, irmãos e subárvore de uma pessoa.
  - Vinculação de Pessoa ↔ Usuário via **ValidationToken**.

- **Gerenciamento de Usuários**
  - Criar usuários vinculados a Pessoas.
  - Obter informações completas de um usuário, incluindo registros e pessoas relacionadas.

- **Gerenciamento de Registros**
  - Criar registros (eventos, fotos, legendas).
  - Obter registros por ID.
  - Obter todos os registros.
  - 🔹 **Novo:** Obter registros vinculados a uma Pessoa específica (`GET /api/registros/pessoa/{id}`).

---

## 🔑 Fluxo de ValidationToken

- Quando uma **Pessoa** é criada sem Usuário, um **ValidationToken** é gerado automaticamente.
- Esse token pode ser usado posteriormente para vincular um **Usuário** à Pessoa existente.
- Após a vinculação, todos os registros associados à Pessoa passam a ser visíveis também pelo Usuário.

### Exemplo de fluxo
1. Criar Pessoa sem Usuário → gera ValidationToken.
2. Criar Usuário informando o token → vincula Usuário ↔ Pessoa.
3. Consultar Usuário → registros da Pessoa aparecem.
4. Consultar Pessoa → registros continuam acessíveis normalmente.

---

## 🌐 Endpoints principais

### Pessoas
- `POST /api/pessoas` → cria uma nova pessoa.
- `GET /api/pessoas/{id}` → obtém dados básicos da pessoa.
- `GET /api/pessoas/{id}/registros` → obtém pessoa com registros.
- `GET /api/pessoas/{id}/filhos` → lista filhos da pessoa.
- `GET /api/pessoas/{id}/irmaos` → lista irmãos da pessoa.
- `DELETE /api/pessoas/{id}` → exclui pessoa (cascade delete remove vínculos e tokens).

### Usuários
- `POST /api/usuarios` → cria usuário (pode informar ValidationToken).
- `GET /api/usuarios/{id}` → obtém dados completos do usuário, incluindo registros.

### Registros
- `POST /api/registros` → cria novo registro.
- `GET /api/registros/{id}` → obtém registro por ID.
- `GET /api/registros` → lista todos os registros.
- 🔹 `GET /api/registros/pessoa/{pessoaId}` → lista registros vinculados a uma pessoa.

---

## 📂 Exemplo de resposta do endpoint de registros por pessoa

**Requisição:**
`GET /api/registros/pessoa/3`


**Resposta:**
```json
[
  {
    "id": 1,
    "data": "2025-11-22",
    "legenda": "Aniversário da Laiz",
    "fotoPath": "/uploads/foto1.jpg",
    "criadorId": 2,
    "criadorEmail": "helton@email.com",
    "pessoasEnvolvidas": [
      { "id": 3, "nome": "Laiz" },
      { "id": 1, "nome": "Helton" }
    ]
  }
]
```
---

## 🚀 Tecnologias Utilizadas

- **.NET 8 / ASP.NET Core Web API**
- **Entity Framework Core** (SQL Server)
- **Dependency Injection** com Services e Interfaces
- **Swagger** para documentação e testes
- **Arquitetura em camadas** (Controllers → Services → Data → Models → DTOs)

---

## 📂 Estrutura do Projeto

FamilyTree/
│
├── Controllers/ # Endpoints da API
├── DTOs/ # Data Transfer Objects (entrada/saída)
├── Models/ # Entidades do domínio
├── Services/ # Implementações de regras de negócio
├── Services/Interfaces # Interfaces dos Services
├── Data/ # Contexto EF Core (AppDbContext)
└── Program.cs # Configuração principal da aplicação

---

## ⚙️ Configuração e Execução

### 1. Clonar o repositório

```bash
git clone https://github.com/seu-usuario/familytree-api.git
cd familytree-api
```

### 2. Configurar o banco de dados

No arquivo appsettings.json, configure sua connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=FamilyTreeDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 3. Criar as migrations e atualizar o banco

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Executar a aplicação

```
dotnet run
```

Por padrão, a aplicação será iniciada nas portas configuradas em launchSettings.json:

```
HTTP: http://localhost:5148
HTTPS: https://localhost:7103
```

A API estará disponível em:

```
http://localhost:5148/swagger
https://localhost:7103/swagger
```

## 🧪 Testes da API

### 🔹 Swagger

Acesse https://localhost:5001/swagger para explorar e testar os endpoints.

### 🔹 Postman

- Importar a collection (será disponibilizada futuramente).

- Testar os fluxos principais:
  - Criar usuário
  - Criar pessoa
  - Criar registro
  - Criar pedido
  - Criar assinatura

## 📌 Próximos Passos

### 🔨 Arquitetura

- Evoluir para Clean Architecture (separar camadas Domain, Application, Infrastructure, Presentation).
- Implementar CQRS + MediatR para comandos e queries.
- Adicionar Unit Tests e Integration Tests.
- Configurar CI/CD com GitHub Actions ou Azure DevOps.

### 🔐 Segurança

- Implementar JWT Authentication
- Adicionar roles e permissões (usuário comum, administrador)

### 💾 Persistência

- Revisar entidades e relacionamentos
- Adicionar Seed Data para testes iniciais

### 🌐 Documentação

- Criar Postman Collection oficial
- Expandir README com exemplos de requisições

### 📈 Roadmap Futuro

- Frontend em React/Next.js para visualização da árvore genealógica
- Upload de imagens para registros e narrativas
- Integração com meios de pagamento reais para assinaturas e marketplace
- Geração de relatórios e exportação de dados

### 👨‍💻 Contribuição

1. Faça um fork do projeto.
2. Crie uma branch para sua feature:

```
git checkout -b feature/nova-feature
```

3. Commit suas alterações:

```
git commit -m "feat: descrição da feature"
```

4. Push para sua branch:

```
git push origin feature/nova-feature
```

5. Abra um Pull Request.

### Licença

Este projeto está sob a licença MIT. Consulte o arquivo LICENSE para mais detalhes.
