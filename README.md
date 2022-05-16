# Description
[Legacy code](https://github.com/murariuroger/adfenix-interview-challenge/blob/master/src/Adfenix.MetricsScraper/LegacyCodeToRefactor.cs) was refactored to __Adfenix.MetricsScraper__ console application.
Three Web APIs were additionally created to mock the upstream services:
 - ZendeskMock
 - CampaignServerMock
 - VisualiserMock

## Running locally
Prerequisites:
- [NET6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).
- [Docker](https://docs.docker.com/desktop/windows/install/) 

Steps:
- cd to __\src\Docker__ and run ```docker compose up -d``` or run docker-compose project from Visual Studio
- add the following to hosts file(__C:\Windows\System32\drivers\etc\hosts__ or __/etc/hosts__)
```
127.0.0.1 1.localhost
127.0.0.1 2.localhost
127.0.0.1 3.localhost
127.0.0.1 4.localhost
127.0.0.1 5.localhost
127.0.0.1 6.localhost
127.0.0.1 7.localhost
127.0.0.1 8.localhost
127.0.0.1 9.localhost
127.0.0.1 10.localhost
127.0.0.1 11.localhost
```

- run Adfenix.MetricsScraper from Visual Studio or from cli ```dotnet Adfenix.MetricsScraper.dll```

After running the Adfenix.MetricsScraper you can check if results were being sent to VisualiserMock
by navigating to [Visualiser](http://localhost:8998/api/v1/series).

## Tests
Unit tests were added only for __Adfenix.MetricsScraper__ project(see __/tests__ folder).
