# ======================
# Stage 1: Build
# ======================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copying project files for better restoration (cache layer)
COPY ["src/Services/Orders/Api/Orders.Api.csproj", "src/Services/Orders/Api/"]
COPY ["src/Services/Orders/Application/Orders.Application.csproj", "src/Services/Orders/Application/"]
COPY ["src/Services/Orders/Domain/Orders.Domain.csproj", "src/Services/Orders/Domain/"]
COPY ["src/Services/Orders/Infrastructure/Orders.Infrastructure.csproj", "src/Services/Orders/Infrastructure/"]

RUN dotnet restore "src/Services/Orders/Api/Orders.Api.csproj"

# Copy other sources
COPY . .

WORKDIR "/src/src/Services/Orders/Api"
RUN dotnet build "Orders.Api.csproj" -c Release -o /app/build

# ======================
# Stage 2: Publish
# ======================
FROM build AS publish
RUN dotnet publish "Orders.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ======================
# Stage 3: Runtime
# ======================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# for non-root environment (better)
RUN adduser --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8010
EXPOSE 8010

USER root
RUN apt-get update && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*
USER appuser

ENTRYPOINT ["dotnet", "Orders.Api.dll"]
