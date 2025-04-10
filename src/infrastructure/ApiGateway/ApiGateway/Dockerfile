# Base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy local NuGet packages
COPY ./Packages /Packages
RUN dotnet nuget add source /Packages --name LocalPackages


# Projeyi doğru yerden kopyala
COPY ./src/infrastructure/ApiGateway/ApiGateway/ApiGateway.csproj ./ApiGateway/

# Restore yap
RUN dotnet restore ./ApiGateway/ApiGateway.csproj

# Diğer tüm dosyaları kopyala
COPY ./src/infrastructure/ApiGateway/ApiGateway ./ApiGateway/

# Build
WORKDIR /src/ApiGateway
RUN dotnet build "ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish aşaması
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/ApiGateway
RUN dotnet publish "ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.dll"]
