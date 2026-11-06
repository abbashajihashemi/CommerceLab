# OrderManagement

Modular Monolith for learning Docker, Message Brokers (RabbitMQ + MassTransit) and Observability with .NET 8.

## Structure
- `src/Services/OrderManagement/`
  - `Api` (assembly: `OrderManagement.Api`)
  - `Application` (assembly: `OrderManagement.Application`)
  - `Domain` (assembly: `OrderManagement.Domain`)
  - `Infrastructure` (assembly: `OrderManagement.Infrastructure`)

## How to run
```bash
cd src/Services/OrderManagement/Api
dotnet run
```
