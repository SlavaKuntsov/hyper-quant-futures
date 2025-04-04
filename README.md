# HyperQuant Futures Test Task

### Architecture
Архитектура представлена главным сервисом `Futures` и отдельной папкой с общими конфигурациями для микросервисов 
`Shared`.

Используется чистая архитектура которая разделяет наш проект на 5 слоев:
 - API
 - Application
 - Domain
 - Infrastructure
 - Persistence

### Docker

 - futures-service
 - futures-postgres
 - redis

### Implementation

**По итогу...**

Есть контроллер `FutureController` для получения _price difference_ между двумя квартальными фьючерсами. 
_Api/Controllers_

Есть `Hangfire Jobs` для получения _price difference_ каждый час в ..:00 и каждый день в 23:59.
_Application/Extensions_

Внутри них с помощью `MediatR` вызывается обработчик `FuturesDifferenceCommandHandler`.
_Application/Handlers/Commands_

Кроме того есть сервис для `http client` который обращается к `https://fapi.binance.com` на нужные нам роуты. 
Повторные попытки отправки запросов на апи и т.д настроенно с помощью `Polly`.
_Infrastructure/ApiClients_

### Shared

Представляет собой проекты с конфигурациями самого web api или каких либо технологий

Для Postgresql в проекте Database настроен пулл для лучшей скорости бд.