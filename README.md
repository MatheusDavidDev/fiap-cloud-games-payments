# FIAP Cloud Games - Payments API

## Sobre

Microsserviço responsável pelo processamento e simulação dos pagamentos realizados na plataforma.

---

## Responsabilidades

- Receber solicitações de pagamento;
- Processar pagamentos;
- Simular aprovação ou reprovação;
- Informar o resultado do pagamento.

---

## Tecnologias

- .NET 8
- ASP.NET Core
- RabbitMQ
- MassTransit
- Docker

---

## Mensageria

Consome:

### OrderPlacedEvent

Recebe uma solicitação de pagamento enviada pela Catalog API.

---

Publica:

### PaymentProcessedEvent

Envia o resultado do pagamento para os serviços interessados.

Consumidores:

- Catalog API
- Notifications API

---

## Executando

```bash
docker compose up -d
