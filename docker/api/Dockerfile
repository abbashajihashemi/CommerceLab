# ======================
# Stage 1: Build
# ======================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copying project files for better restoration (cache layer)
COPY ["src/Services/OrderManagement/OrderManagement.Api/OrderManagement.Api.csproj", "src/Services/OrderManagement/OrderManagement.Api/"]
COPY ["src/Services/OrderManagement/OrderManagement.Application/OrderManagement.Application.csproj", "src/Services/OrderManagement/OrderManagement.Application/"]
COPY ["src/Services/OrderManagement/OrderManagement.Domain/OrderManagement.Domain.csproj", "src/Services/OrderManagement/OrderManagement.Domain/"]
COPY ["src/Services/OrderManagement/OrderManagement.Infrastructure/OrderManagement.Infrastructure.csproj", "src/Services/OrderManagement/OrderManagement.Infrastructure/"]

RUN dotnet restore "src/Services/OrderManagement/OrderManagement.Api/OrderManagement.Api.csproj"

# Copy other sources
COPY . .

WORKDIR "/src/src/Services/OrderManagement/OrderManagement.Api"
RUN dotnet build "OrderManagement.Api.csproj" -c Release -o /app/build

# ======================
# Stage 2: Publish
# ======================
FROM build AS publish
RUN dotnet publish "OrderManagement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

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

ENTRYPOINT ["dotnet", "OrderManagement.Api.dll"]