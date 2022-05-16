#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8999

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Adfenix.CampaignServersMock/Adfenix.CampaignServersMock.csproj", "Adfenix.CampaignServersMock/"]
RUN dotnet restore "Adfenix.CampaignServersMock/Adfenix.CampaignServersMock.csproj"
COPY . .
WORKDIR "/src/Adfenix.CampaignServersMock"
RUN dotnet build "Adfenix.CampaignServersMock.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Adfenix.CampaignServersMock.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adfenix.CampaignServersMock.dll"]