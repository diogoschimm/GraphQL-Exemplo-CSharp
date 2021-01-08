# GraphQL-Exemplo-CSharp
Exemplo de GraphQL com c#, api para clientes e status de cliente


## Referências 

Projeto Web
```xml 
    <PackageReference Include="GraphQL.Server.Transports.AspNetCore.SystemTextJson" Version="4.2.0" />
    <PackageReference Include="GraphQL.Server.Ui.Playground" Version="4.2.0" />
```

Projeto GraphQL Core
```xml 
    <PackageReference Include="GraphQL" Version="3.1.3" />
```

## Programando

Vamos criar o AppSchema e fornecer a Classe de Query para a propriedade Query, essa classe herda de GraphQL.Types.Schema 

```csharp
  public class AppSchema: Schema
  {
      public AppSchema(IServiceProvider provider) :base(provider)
      {
          Query = provider.GetRequiredService<AppQuery>();
      }
  }
```
Vamos criar o AppQuery (onde fica concentrado os resolvers para os dados que iremos retornar)

```csharp
  public class AppQuery : ObjectGraphType<object>
  {
      public AppQuery(IServiceProvider provider)
      {
          Name = "Query";

          var clienteQuery = provider.GetRequiredService<IClienteQuery>();
          Field<ClienteType>(
              "cliente",
              arguments: new QueryArguments(
                  new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "idCliente" }
              ),
              resolve: ctx => clienteQuery.Get(ctx.GetArgument<int>("idCliente")));

          Field<ClienteType>(
              "clientesPorStatus",
              arguments: new QueryArguments(
                  new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "idStatusCliente" }
              ),
              resolve: ctx => clienteQuery.GetByStatus(ctx.GetArgument<int>("idStatusCliente")));

          Field<ListGraphType<ClienteType>>(
              "clienteList",
              resolve: ctx => clienteQuery.GetAll());

          var statusClienteQuery = provider.GetRequiredService<IStatusClienteQuery>();
          Field<ListGraphType<ClienteType>>(
              "statusClienteList",
              resolve: ctx => clienteQuery.GetAll());

          Field<StatusClienteType>(
              "statusCliente",
              arguments: new QueryArguments(
                  new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "idStatusCliente" }
              ),
              resolve: ctx => statusClienteQuery.Get(ctx.GetArgument<int>("idStatusCliente")));
      }
  }
```

Criamos também o nossos objetos mapeados (que serão utilizados nas consultas GraphQL)

```csharp
  public ClienteType(IStatusClienteQuery statusClienteQuery)
  {
      Name = "Cliente";
      Description = "Representa um cliente do sistema";

      Field(c => c.IdCliente).Description("Código que identifica o cliente no sistema");
      Field(c => c.NomeCliente).Description("Nome do Cliente");
      Field(c => c.DataNascimento, nullable: true).Description("Data de Nascimento do Cliente");
      Field(c => c.IdStatusCliente).Description("Status do Cliente");

      Field<CPFType>("cpf", resolve: ctx => ctx.Source.CPF);

      Field<StatusClienteType>("StatusCliente", 
            resolve: ctx => statusClienteQuery.Get(ctx.Source.IdStatusCliente));
  }
  public StatusClienteType(IClienteQuery clienteQuery)
  {
      Name = "StatusCliente";
      Description = "Representa os Status de Cliente no sistema";

      Field(s => s.IdStatusCliente).Description("Código que identifica o Status");
      Field(s => s.NomeStatusCliente).Description("Nome do Status");

      Field<ListGraphType<ClienteType>>("clientes", 
            resolve: ctx => clienteQuery.GetByStatus(ctx.Source.IdStatusCliente));

  }
  public CPFType()
  {
      Name = "CPF";
      Description = "Representa um documento CPF";

      Field(c => c.Number).Description("Número do Documento");
  }
```

E por último adicionamos ao pipeline do asp.net nossos objetos e métodos do GraphQL para tratar as requisições (no container de injeção de dependências).

```csharp
  public void ConfigureServices(IServiceCollection services)
  {
      services.AddDependencies();

      services.AddGraphQL(options =>
      {
          options.EnableMetrics = true;
      })
      .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
      .AddSystemTextJson();
  }

  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
      if (env.IsDevelopment())
          app.UseDeveloperExceptionPage();

      app.UseGraphQL<ISchema>(); 
      app.UseGraphQLPlayground();
  }
```

O Método de Extensão AddDependencies()

```csharp
  public static IServiceCollection AddDependencies(this IServiceCollection services)
  {
      services.AddSingleton<IClienteQuery, ClienteQuery>();
      services.AddSingleton<IStatusClienteQuery, StatusClienteQuery>();

      services.AddSingleton<ClienteType>();
      services.AddSingleton<StatusClienteType>();
      services.AddSingleton<CPFType>();

      services.AddSingleton<AppQuery>();
      services.AddSingleton<ISchema, AppSchema>();

      return services;
  }
```

## Exemplos de Querys

```graphQL
## Query 1 (Buscar statusCliente 1 e todos os clientes desse status)
{
  statusCliente(idStatusCliente: 1) {
    idStatusCliente
    nomeStatusCliente
    clientes {
      idCliente
      nomeCliente
    }
  }
}

## Query 2 (Buscar Cliente 2)
{
  cliente(idCliente: 2) {
    nomeCliente
    cpf {
      number
    }
  }
}

## Query 3 (Buscar listagem de Clientes)
{
  clienteList {
    idCliente
    nomeCliente
    statusCliente {
      idStatusCliente
      nomeStatusCliente
    }
    cpf {
      number
    }
  }
}

```
