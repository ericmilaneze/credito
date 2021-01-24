# Crédito - ASP.NET Core 5.0 Boilerplate

## Como executar

### Local com docker

Passo 1 - Execute o seguinte comando (Linux ou **cmder** no Windows):

`sh docker-compose-up.sh`

Passo 2 - Para testar via **curl** (sem gravar no MongoDB):

```
curl --location --request POST 'http://localhost/api/contratos/calculo' \
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
curl --location --request POST 'http://localhost/api/contratos' \
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
curl --location --request POST 'http://localhost:5000/api/contratos/calculo' \
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
curl --location --request POST 'http://localhost:5000/api/contratos' \
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


## O que tem nesse sample?

* ASP.NET Core 5.0
* C# 9 (record)
* xUnit
* MediatR
* Serilog
* AutoMapper
* MongoDB
* Fluent Validation
* Fluent Assertions
* Docker
* Docker Compose

## O que pretendo adicionar nesse sample?

* Swagger
* NSubstitute
* AWS CDK
* AWS ECS
* AWS Lambda
* AWS SNS
* AWS SQS
* AWS Systems Manager Parameter Store
* AWS Cognito
