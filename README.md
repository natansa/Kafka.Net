# Kafka.Net.Api 🚀

Este projeto contém dois serviços RESTful desenvolvidos em **.NET 9** para integração com o **Apache Kafka**:

- **Api.Publisher** – responsável por publicar mensagens no Kafka.
- **Api.Consumer** – responsável por consumir mensagens do Kafka.

A comunicação com o Kafka é feita utilizando a biblioteca oficial [Confluent.Kafka](https://www.nuget.org/packages/Confluent.Kafka).

---

## 🔧 Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/)
- Docker Compose

---

## 🐳 Subindo o Kafka com Docker

Use o `docker-compose.yml` abaixo para subir o Kafka localmente, com o comando "docker-compose up -d" :

```yaml
services:
  zookeeper:
    image: confluentinc/cp-zookeeper:7.5.0
    container_name: zookeeper
    ports:
      - "2181:2181"
    environment:
      ZOOKEEPER_CLIENT_PORT: 2181
      ZOOKEEPER_TICK_TIME: 2000

  kafka:
    image: confluentinc/cp-kafka:7.5.0
    container_name: kafka
    ports:
      - "9092:9092"
    environment:
      KAFKA_BROKER_ID: 1
      KAFKA_ZOOKEEPER_CONNECT: zookeeper:2181
      KAFKA_ADVERTISED_LISTENERS: PLAINTEXT://localhost:9092
      KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR: 1


## 🚀 Endpoints

### 🔹 Publisher

**Projeto:** `Api.Publisher`  
**Rota:** `POST /publisher`  
**Descrição:** Publica uma mensagem no tópico Kafka.

#### Exemplo de requisição `.http`

```http
@Api.Publisher_HostAddress = https://localhost:7247

POST {{Api.Publisher_HostAddress}}/publisher
Content-Type: application/json

"Mensagem publicada com sucesso"

### 🔹 Consumer

**Projeto:** `Api.Consumer`  
**Rota:** `POST /consumer`  
**Descrição:** Consome uma mensagem do tópico Kafka.

#### Exemplo de requisição `.http`

```http
@Api.Consumer_HostAddress = https://localhost:7042

GET {{Api.Consumer_HostAddress}}/Consumer/
Accept: application/json


## 🚀 Estrutura do Projeto

Kafka.Net.Api/
│
├── Api.Publisher/
│   ├── Controllers/
│   │   └── PublisherController.cs
│   ├── appsettings.json
│
├── Api.Consumer/
│   ├── Controllers/
│   │   └── ConsumerController.cs
│   ├── appsettings.json
│
└── docker-compose.yml

## 🙋‍♂️ Autor

Desenvolvido por [Natanael Sa Rodrigues](https://github.com/natansa)