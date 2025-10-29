# Étape 1 : utiliser l'image SDK pour publier l'application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier tous les fichiers du projet
COPY . .

# Publier l'application en Release dans le dossier /app/out
RUN dotnet publish "GestCirculation.csproj" -c Release -o /app/out

# Étape 2 : créer l'image runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copier le résultat du build
COPY --from=build /app/out ./

# Copier le fichier SQLite
COPY ./gestionContraventions.db ./gestionContraventions.db

# Exposer le port
EXPOSE 5000

# Lancer l'application
ENTRYPOINT ["dotnet", "GestCirculation.dll"]