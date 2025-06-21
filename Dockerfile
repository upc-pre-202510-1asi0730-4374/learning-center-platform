﻿FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ACME.LearningCenterPlatform.API/ACME.LearningCenterPlatform.API.csproj", "ACME.LearningCenterPlatform.API/"]
RUN dotnet restore "ACME.LearningCenterPlatform.API/ACME.LearningCenterPlatform.API.csproj"
COPY . .
WORKDIR "/src/ACME.LearningCenterPlatform.API"
RUN dotnet build "./ACME.LearningCenterPlatform.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ACME.LearningCenterPlatform.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Setea el entorno como Development
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "ACME.LearningCenterPlatform.API.dll"]
