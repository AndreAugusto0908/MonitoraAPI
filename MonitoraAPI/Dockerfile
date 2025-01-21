# Base image para runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Adiciona um usuário não-root para segurança
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia o arquivo de projeto e restaura dependências
COPY ["MonitoraAPI/MonitoraAPI.csproj", "MonitoraAPI/"]
RUN dotnet restore "MonitoraAPI/MonitoraAPI.csproj"

# Copia o restante dos arquivos para o diretório de trabalho
COPY . .

# Define o diretório do projeto e executa a build
WORKDIR "/src/MonitoraAPI"
RUN dotnet build "MonitoraAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish image
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MonitoraAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image para execução
FROM base AS final
WORKDIR /app

# Copia os artefatos publicados do estágio de build
COPY --from=publish /app/publish .

# Comando de entrada para iniciar a aplicação
ENTRYPOINT ["dotnet", "MonitoraAPI.dll"]
