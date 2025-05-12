FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8000
#Sets Dotnet Envirement
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

#Copy Files From porjects
COPY ["src/Api/Api.csproj", "src/Api/"]
COPY ["src/Infratructue/Infratructue.csproj", "src/Infratructue/"]
RUN dotnet restore "src/Api/Api.csproj"

#Copy Files to Docker
COPY src/Api/ src/Api/
COPY src/Infratructue/ src/Infratructue/

WORKDIR "src/Api"
RUN dotnet build "Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]