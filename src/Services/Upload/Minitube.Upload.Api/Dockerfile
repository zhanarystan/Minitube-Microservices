# Слой сборки (Build)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Копируем файлы проектов для восстановления зависимостей
# Это позволяет Docker кэшировать слои и собирать быстрее в следующий раз
COPY ["Services/Upload/Minitube.Upload.Api/Minitube.Upload.Api.csproj", "Services/Upload/Minitube.Upload.Api/Minitube.Upload.Api/"]
COPY ["Services/Upload/Minitube.Upload.Application/Minitube.Upload.Application.csproj", "Services/Upload/Minitube.Upload.Application/"]
COPY ["Services/Upload/Minitube.Upload.Infrastructure/Minitube.Upload.Infrastructure.csproj", "Services/Upload/Minitube.Upload.Infrastructure/"]
COPY ["Services/Upload/Minitube.Upload.Domain/Minitube.Upload.Domain.csproj", "Services/Upload/Minitube.Upload.Domain/"]
COPY ["Shared/Minitube.Shared/Minitube.Shared.csproj", "Shared/Minitube.Shared/"]

# Восстанавливаем пакеты
RUN dotnet restore "Services/Upload/Minitube.Upload.Api/Minitube.Upload.Api/Minitube.Upload.Api.csproj"

# Копируем весь исходный код (из папки src)
COPY . .

# Собираем и публикуем
WORKDIR "/src/Services/Upload/Minitube.Upload.Api"
RUN dotnet publish "Minitube.Upload.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Слой запуска (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Minitube.Upload.Api.dll"]