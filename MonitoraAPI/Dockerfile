# Base image para runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia apenas o arquivo csproj e restaura dependências
COPY ["MonitoraAPI/MonitoraAPI.csproj", "MonitoraAPI/"]
RUN dotnet restore "MonitoraAPI/MonitoraAPI.csproj"

# Copia o restante do código e executa a build
COPY . .
WORKDIR "/src/MonitoraAPI"
RUN dotnet build "MonitoraAPI.csproj" -c Release -o /app/build

# Publish image
FROM build AS publish
RUN dotnet publish "MonitoraAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image para execução
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MonitoraAPI.dll"]
