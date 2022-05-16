#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8998

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Adfenix.VisualiserMock/Adfenix.VisualiserMock.csproj", "Adfenix.VisualiserMock/"]
RUN dotnet restore "Adfenix.VisualiserMock/Adfenix.VisualiserMock.csproj"
COPY . .
WORKDIR "/src/Adfenix.VisualiserMock"
RUN dotnet build "Adfenix.VisualiserMock.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Adfenix.VisualiserMock.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adfenix.VisualiserMock.dll"]