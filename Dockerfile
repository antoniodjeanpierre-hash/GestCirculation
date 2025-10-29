# Étape 1 : Utiliser l'image SDK .NET pour build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copier les fichiers du projet et restaurer les dépendances
COPY *.sln ./
COPY GestCirculation/*.csproj ./GestCirculation/
RUN dotnet restore

# Copier tout le projet
COPY GestCirculation/. ./GestCirculation/
WORKDIR /app/GestCirculation

# Publier l'application en mode Release
RUN dotnet publish -c Release -o /app/out

# Étape 2 : Créer l'image runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copier les fichiers publiés
COPY --from=build /app/out ./

# Copier la base de données SQLite dans l'image
COPY gestionContraventions.db ./

# Exposer le port de l'application
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

# Lancer l'application
ENTRYPOINT ["dotnet", "GestCirculation.dll"]