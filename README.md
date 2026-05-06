## Desafio ASP.NET MVC (.NET Framework 4.6.2) — CRUD de Produtos - Versão 2.0

Aplicação ASP.NET MVC com SQL Server para cadastrar, listar, editar e excluir (delete lógico) produtos.

### Tecnologias
- ASP.NET MVC 5
- .NET Framework 4.6.2
- Entity Framework 6 (Code First)
- SQL Server (SQL Express) ou SQL Server em Docker

### Entidade `Produto`
Campos:
- `Id` (int)
- `Nome` (string)
- `Preco` (decimal)
- `Estoque` (int)
- `DataAtualizacao` (DateTime)
- `DataDelecao` (DateTime?, usado para soft delete)

### Regras principais
- **DataAtualizacao**: preenchida automaticamente na criação e atualização.
- **Delete lógico**: ao excluir um produto, o sistema preenche `DataDelecao` e **não remove** o registro do banco.
- **Listagem**: por padrão **não lista** produtos deletados (filtra `DataDelecao == null` no repositório).

### Validações
As validações são feitas via **DataAnnotations** na entidade (`Required`, `StringLength`, `Range`), refletindo no `ModelState` do MVC e nas mensagens nas views.

### Banco de dados
A connection string está em `Web.config` com o nome `DefaultConnection`.

Você pode rodar de **2 formas**:
- **Com Docker** (recomendado para avaliador: sobe o SQL Server sem instalar nada além do Docker)
- **Sem Docker** (usando SQL Server Express/Developer instalado)

#### Opção A — Com Docker (SQL Server em container)
Pré-requisitos:
- Docker Desktop instalado e funcionando

Passo a passo:
1. Na raiz do projeto, copie o arquivo `.env.example` para `.env` e defina a senha do `sa`:
   - `MSSQL_SA_PASSWORD=...`
2. Suba o banco:
   - `docker compose up -d`
3. Ajuste a connection string no `Web.config` para usar SQL Auth na porta 1433:

`Data Source=localhost,1433;Initial Catalog=TesteASPNET;User ID=sa;Password=<<SUA_SENHA>>;MultipleActiveResultSets=True;TrustServerCertificate=True`

4. Rode o projeto no Visual Studio (F5) e acesse `/Produto`.

Observação:
- Ao iniciar a aplicação, o EF cria/atualiza a tabela `Produtos` automaticamente.

Para parar/remover:
- Parar: `docker compose down`
- Remover também os dados: `docker compose down -v`

#### Opção B — Sem Docker (SQL Express/Developer instalado)
Exemplo (Windows Authentication):

`Data Source=.\\SQLEXPRESS;Initial Catalog=TesteASPNET;Integrated Security=True;MultipleActiveResultSets=True`

### EF Code First + Migrations
O projeto está configurado para aplicar migrations automaticamente ao iniciar a aplicação:
- `Migrations/Configuration.cs`: `AutomaticMigrationsEnabled = true`
- `Global.asax.cs`: `MigrateDatabaseToLatestVersion<...>()`

Na prática, ao rodar o projeto a primeira vez, o EF cria/atualiza a tabela `Produtos` conforme o modelo.

### Injeção de dependência
Foi configurado um `IDependencyResolver` simples (`Infrastructure/InjecaoDependencia/ResolvedorDependenciasSimples.cs`) para injetar:
`DataContext → ProdutoRepository → ProdutoService → ProdutoController`.

O `DataContext` é mantido por request e descartado no final (`Application_EndRequest`).

### Como rodar
1. Abra a solução no Visual Studio.
2. Ajuste a connection string (se necessário).
3. Execute (F5).
4. Acesse `/Produto` para testar o CRUD.

