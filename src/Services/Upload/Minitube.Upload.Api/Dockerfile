# ЭТАП 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# 1. Копируем проекты (пути должны соответствовать структуре в репозитории)
COPY ["src/Services/Upload/Minitube.Upload.Api/Minitube.Upload.Api.csproj", "src/Services/Upload/Minitube.Upload.Api/"]
COPY ["src/Services/Upload/Minitube.Upload.Application/Minitube.Upload.Application.csproj", "src/Services/Upload/Minitube.Upload.Application/"]
COPY ["src/Services/Upload/Minitube.Upload.Infrastructure/Minitube.Upload.Infrastructure.csproj", "src/Services/Upload/Minitube.Upload.Infrastructure/"]
COPY ["src/Services/Upload/Minitube.Upload.Domain/Minitube.Upload.Domain.csproj", "src/Services/Upload/Minitube.Upload.Domain/"]
COPY ["src/Shared/Minitube.Shared/Minitube.Shared.csproj", "src/Shared/Minitube.Shared/"]

# 2. Восстанавливаем зависимости
RUN dotnet restore "src/Services/Upload/Minitube.Upload.Api/Minitube.Upload.Api.csproj"

# 3. Копируем ВЕСЬ код (из корня, где лежит .sln)
COPY . .

# 4. Публикуем
WORKDIR "/app/src/Services/Upload/Minitube.Upload.Api"
RUN dotnet publish "Minitube.Upload.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ЭТАП 2: Final (Runtime) - БЕЗ ЭТОГО НИЧЕГО НЕ ЗАПУСТИТСЯ
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Копируем только готовые бинарники из этапа сборки
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Minitube.Upload.Api.dll"]