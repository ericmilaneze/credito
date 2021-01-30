# Crédito - ASP.NET Core 5.0 Boilerplate

## Como executar

### Local com docker

Passo 1 - Execute o seguinte comando (Linux ou **cmder** no Windows):

`sh docker-compose-up.sh`

Passo 2 - Para testar via **curl** (sem gravar no MongoDB):

```
curl --location --request POST 'http://localhost/api/credito/contratos/calculo' \
--header 'Content-Type: application/json' \
--data-raw '{
    "id": "ad801f92-1570-4d69-a2cb-b9aaf874e55b",
    "valorLiquido": 3000,
    "quantidadeDeParcelas": 24,
    "taxaAoMes": 5.00,
    "tac": 6.00,
    "iof": 10.00,
    "diasDeCarencia": 30
}'
```

Passo 3 - Para testar a gravação no MongoDB via **curl**:

```
curl --location --request POST 'http://localhost/api/credito/contratos' \
--header 'Content-Type: application/json' \
--data-raw '{
    "id": "ad801f92-1570-4d69-a2cb-b9aaf874e55e",
    "valorLiquido": 3000,
    "quantidadeDeParcelas": 24,
    "taxaAoMes": 5.00,
    "tac": 6.00,
    "iof": 10.00,
    "diasDeCarencia": 30
}'
```

### Debug no Visual Studio Code

Passo 1 - Execute o seguinte comando (Linux ou **cmder** no Windows):

`sh docker-compose-up-debug.sh`

Passo 2 - Aperte `F5` no teclado.

### Debug no Visual Studio Code com docker

Passo 1 - Execute o seguinte comando (Linux ou **cmder** no Windows):

`sh docker-compose-up-debug.sh`

Passo 2 - `Ctrl+Shift+D` para abrir a aba "Run" na side bar.

Passo 3 - No topo, ao lado de **RUN**, selecione **Docker .NET Core Attach (Preview)**.

Passo 4 - Aperte `F5` no teclado.

Passo 5 - Selecione "credito" e depois "creditowebapi".


### Efetuar chamadas em modo Debug

#### Testar via curl (sem gravar no MongoDB)

```
curl --location --request POST 'http://localhost:5000/api/credito/contratos/calculo' \
--header 'Content-Type: application/json' \
--data-raw '{
    "id": "ad801f92-1570-4d69-a2cb-b9aaf874e55b",
    "valorLiquido": 3000,
    "quantidadeDeParcelas": 24,
    "taxaAoMes": 5.00,
    "tac": 6.00,
    "iof": 10.00,
    "diasDeCarencia": 30
}'
```

#### Testar a gravação no MongoDB via curl:

```
curl --location --request POST 'http://localhost:5000/api/credito/contratos' \
--header 'Content-Type: application/json' \
--data-raw '{
    "id": "ad801f92-1570-4d69-a2cb-b9aaf874e55e",
    "valorLiquido": 3000,
    "quantidadeDeParcelas": 24,
    "taxaAoMes": 5.00,
    "tac": 6.00,
    "iof": 10.00,
    "diasDeCarencia": 30
}'
```


## Swagger

`http://localhost:5000/api/credito/swagger/index.html`


## MongoDB

### Visualizar os dados e gerenciar o MongoDB local pelo mongo-express

Passo 1 - Execute o seguinte comando (Linux ou **cmder** no Windows):

`sh docker-compose-up.sh`

Passo 2 - Acesse:

http://localhost:8081

User: `admin`

Pass: `admin`

### Acessar o MongoDB local

ConnectionString: mongodb://eric:pass@localhost:27017

Banco de dados: local


## xUnit - Tests

### Executar testes

`dotnet test`

### Code coverage com ReportGenerator

#### Instalação

`dotnet tool install -g dotnet-reportgenerator-globaltool`

#### Usando o ReportGenerator

##### Shell script linux (ou cmder no Windows)

`sh run-tests-with-code-coverage-local.sh`

##### Executar no Visual Studio Code

Passo 1 - Abra o arquivo **keybindings.json** e adicione essa key (ou a de sua preferência):

```
{
  "key": "ctrl+shift+alt+t",
  "command": "workbench.action.tasks.runTask",
  "args": "run-tests-with-code-coverage"
}
```

Passo 2 - Aperte `ctrl+shift+alt+t` no teclado.

**Alternativa** - Aperte `ctrl+shift+p` e depois digite `Tasks: Run Task`. Escolha **run-tests-with-code-coverage**.

Será criado um diretório chamado **coveragereport** com um arquivo **index.html** para ser aberto pelo navegador.


## Postman

Importe no postman o arquivo `./postman/Credito.postman_collection.json`


## Logs com Serilog

A configuração do serilog na API está a maior parte no arquivo `appsettings.json`, mas também existe tanto mais configuração quanto a própria chamada no arquivo `Program.cs`.

O template de saída do log da API (`appsettings.json`) tem o parâmetro `{TraceIdentifier}`, que poderia ser facilmente substituído por `{RequestId}`, mas é usado o `{TraceIdentifier}` para ter um exemplo de como "enriquecer" o log. Esse "enriquecimento" é feito no arquivo `Startup.cs`. Porém, repetindo: colocar `{RequestId}` teria o mesmo efeito prático, que é colocar o `TraceIdentifier` do `HttpContext`.


## O que tem nesse sample?

* CQRS
* ASP.NET Core 5.0
* Swagger
* C# 9 (record)
* AutoMapper
* MediatR
* Fluent Validation
* xUnit
* NSubstitute
* Fluent Assertions
* DataAttribute (é criado um JsonFileDataAttribute)
* Serilog
* MongoDB
* Docker
* Docker Compose

## O que pretendo adicionar nesse sample?

* LocalStack
* AWS CDK
* AWS ECS
* AWS Lambda
* AWS SNS
* AWS SQS
* AWS Systems Manager Parameter Store
* AWS Cognito
