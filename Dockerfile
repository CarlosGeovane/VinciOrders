# Etapa 1: Build — compila a aplicação
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copia os arquivos de projeto e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia o restante do código e publica a aplicação
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime — imagem final menor, só para rodar
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Copia apenas os arquivos compilados da etapa anterior
COPY --from=build /app/publish .

# Define a porta que a aplicação vai usar dentro do container
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "VinciOrders.API.dll"]