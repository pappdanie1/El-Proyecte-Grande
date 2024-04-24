FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["El-Proyecte-Grande/El-Proyecte-Grande.csproj", "El-Proyecte-Grande/"]
RUN dotnet restore "El-Proyecte-Grande/El-Proyecte-Grande.csproj"
COPY . .
WORKDIR "/src/El-Proyecte-Grande"
RUN dotnet build "El-Proyecte-Grande.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "El-Proyecte-Grande.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "El-Proyecte-Grande.dll"]
