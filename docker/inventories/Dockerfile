# ======================
# Stage 1: Build
# ======================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /build

# Copying project files for better restoration (cache layer)
COPY ["src/Services/Inventories/Api/Inventories.Api.csproj", "src/Services/Inventories/Api/"]

RUN dotnet restore "src/Services/Inventories/Api/Inventories.Api.csproj"

# Copy other sources
COPY . .

WORKDIR /build/src/Services/Inventories/Api
RUN dotnet build "Inventories.Api.csproj" -c Release -o /app/build

# ======================
# Stage 2: Publish
# ======================
FROM build AS publish
RUN dotnet publish "Inventories.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ======================
# Stage 3: Runtime
# ======================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# for non-root environment (better)
RUN adduser --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8011
EXPOSE 8011

USER root
RUN apt-get update && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*
USER appuser

ENTRYPOINT ["dotnet", "Inventories.Api.dll"]
