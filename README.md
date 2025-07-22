# 📘 Agenda‑Backend

## 📄 Descrição

O **Agenda‑Backend** é uma API RESTful desenvolvida em **C# (.NET Core/6+)**, que gerencia operações de cadastro e manipulação de compromissos e contatos. Oferece endpoints seguros de CRUD, integrações com JSON e persistência via banco de dados.

---

## ✨ Funcionalidades Principais

### 🗓️ Gerenciamento de Contatos
- Endpoints para **criar**, **buscar**, **atualizar** e **deletar** contatos.
- Validações de campos obrigatórios (nome, e-mail, telefone, etc).

### 📅 Gestão de Compromissos
- Operações REST para adicionar, listar, editar e remover compromissos.
- Suporte a filtros por data, contato responsável e status do evento.

### 🔒 Autenticação e Autorização *(se configurado)*
- Uso de **JWT** para proteger rotas sensíveis.
- Permissões granulares por usuário/role (e.g. usuário padrão vs admin).

### 🧱 Arquitetura Modular
- Camadas claramente separadas: **Controllers**, **Services**, **Repositories**, **Models**.
- Cumprimento do princípio de Inversão de Dependências (DI) para facilitar testes e manutenção.

---

## 🛠️ Tecnologias Utilizadas

- **.NET Core / .NET 6+** (ou versão utilizada)
- **Entity Framework Core** (ou Dapper/Others) para acesso ao banco
- **SQL Server**, **SQLite** ou outro banco relacional
- **Swagger / OpenAPI** para documentação dos endpoints
- **AutoMapper** para mapeamento entre DTOs e entidades
- **xUnit**, **Moq** ou similar para testes unitários (opcional)

---

## 🏗️ Estrutura do Projeto

```text
Agenda-Backend/
├── Controllers/        # Endpoints HTTP (ex: CompromissosController)
├── Models/             # DTOs e entidades (ex: Compromisso, Usuario)
├── Services/           # Lógica de negócio (ex: CompromissoService)
├── Repositories/       # Comunicação com o banco de dados
├── Data/               # DbContext e migrações EF Core
├── Mappings/           # Perfis de mapeamento (AutoMapper)
├── Properties/         # Arquivos de metadados (ex: launchSettings.json)
├── appsettings.json    # Configurações da aplicação (DB, JWT, etc.)
└── Program.cs          # Setup da aplicação e middleware
```


---

## 🚀 Como Executar o Projeto

### 📥 1. Clone o repositório

git clone https://github.com/murilo-valentim-dev/agenda-backend.git
cd agenda-backend

🧩 **2. Abra no Visual Studio 2022 ou VS Code**

- **Para Visual Studio**:  
  `File → Open → Project/Solution → selecione a .sln`

- **Para VS Code**:  
  Abra o diretório e garanta que o SDK .NET esteja instalado (`dotnet --version`)

---

📦 **3. Restaure dependências**

dotnet restore

🛠️ **4. Configure o banco de dados no `appsettings.json`**

"ConnectionStrings": {
  "DefaultConnection": "Server=...;Database=Agenda;User Id=...;Password=...;"
}

▶️ **5. Aplique migrações e execute

dotnet ef database update
dotnet run

🕸️ **6. Acesse a documentação via Swagger

Abra no navegador:
http://localhost:5000/swagger

### 🧪 Testes

Crie testes unitários usando xUnit ou similar.

Exemplos de testes:

✅ Validação de DTOs

🧠 Regras de negócio (Services)

💾 Operações básicas do repositório

### ⚡ Contribuições

Contribuições são bem-vindas! Siga este fluxo:

git fork
git clone https://github.com/seu-usuario/agenda-backend.git
git checkout -b feature/minha-ajuda
git commit -m "Adiciona validação de data em compromisso"
git push origin feature/minha-ajuda
Depois, abra um Pull Request no repositório original.

### 📝 Documentação Recomendada

README.md: visão geral + setup

CHANGELOG.md: mudanças por versão

CONTRIBUTING.md: guia de contribuição e testes

LICENSE: tipo de licença do projeto

docs/: documentação técnica detalhada

Swagger UI: documentação interativa dos endpoints

### 📧 Contato

Para dúvidas ou sugestões, abra um Issue
ou entre em contato: @murilo-valentim-dev

### 🔧 Roadmap (Próximos Passos)

🧬 Versionamento de endpoints (ex: /api/v1/)

📈 Adicionar logs estruturados (ex: Serilog)

⚙️ Criar pipelines CI/CD (GitHub Actions)

🧪 Cobertura de testes (unitários e integração)
