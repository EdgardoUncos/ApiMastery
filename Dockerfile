# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar el archivo de proyecto
COPY ["ApiMastery/ApiMastery.csproj", "ApiMastery/"]
RUN dotnet restore "ApiMastery/ApiMastery.csproj"

# Copiar el resto del código fuente
COPY . .

# Publicar la aplicación
WORKDIR "/src/ApiMastery"
RUN dotnet publish "ApiMastery.csproj" -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "ApiMastery.dll"]
