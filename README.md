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

> Алгоритм действий в обработчике
> 
>    1. Получаем актуальные контракты через еxchangeInfo. (возможно можно кэшировать)
>    2. Отбираем нужные нам по префиксу имени символа.
>    3. Получаем 2 квартальных фьючерса.
>    4. Сортируем по дате.
>    5. По названию фьючерса получаем сущность `FutureContract` из бд, либо создаём новую если такой не существует.
>    6. Создаем новые `PricePoint` и сохраняем в бд.
>    7. Считаем _price difference_, создаем аналогичную сущность и сохраняем в бд.
>    8. Возвращаем результат.

Кроме того есть сервис для `http client` который обращается к `https://fapi.binance.com` на нужные нам роуты. 
Повторные попытки отправки запросов на апи и т.д настроенно с помощью `Polly`.
_Infrastructure/ApiClients_

### Shared

Представляет собой проекты с конфигурациями самого web api или каких либо технологий

Для Postgresql в проекте Database настроен пулл для лучшей скорости бд.
