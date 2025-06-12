# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copia o csproj e restaura as dependências
COPY AluguelApi.csproj ./
RUN dotnet restore

# Copia todo o código e faz o build da aplicação
COPY . ./
RUN dotnet publish -c Release -o out

# Etapa 2: Imagem final (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out ./

# Expõe a porta padrão
EXPOSE 80

# Inicia o app
ENTRYPOINT ["dotnet", "AluguelApi.dll"]
