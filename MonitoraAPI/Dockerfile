# Use uma imagem base compatível com .NET 6.0 no runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use a imagem SDK do .NET 6.0 para build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copia o arquivo csproj e restaura dependências
COPY ["MonitoraAPI/MonitoraAPI.csproj", "MonitoraAPI/"]
WORKDIR /src/MonitoraAPI
RUN dotnet restore "MonitoraAPI.csproj"

# Copia o restante dos arquivos e executa a build
COPY . .
RUN dotnet build "MonitoraAPI.csproj" -c Release -o /app/build

# Publica a aplicação para o diretório publish
FROM build AS publish
RUN dotnet publish "MonitoraAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image para execução
FROM base AS final
WORKDIR /app

# Copia os artefatos publicados
COPY --from=publish /app/publish .

# Define o ponto de entrada
ENTRYPOINT ["dotnet", "MonitoraAPI.dll"]
