# Étape 1 : Utiliser l'image .NET SDK pour build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copier les fichiers csproj et restaurer les dépendances
COPY *.sln .
COPY GestCirculation/*.csproj ./GestCirculation/
RUN dotnet restore

# Copier tout le reste du projet
COPY GestCirculation/. ./GestCirculation/

# Publier l'application dans le dossier /out
WORKDIR /app/GestCirculation
RUN dotnet publish -c Release -o /app/out

# Étape 2 : Image runtime pour exécuter l'application
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out ./

# Si tu as une DB SQLite, copie-la également
COPY GestCirculation/Data/GestCirculation.db ./Data/

# Exposer le port utilisé par ASP.NET
EXPOSE 5000

# Démarrer l'application
ENTRYPOINT ["dotnet", "GestCirculation.dll"]