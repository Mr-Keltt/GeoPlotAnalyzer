#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/API/GeoPlotAnalyzer.API/GeoPlotAnalyzer.API.csproj", "Systems/API/GeoPlotAnalyzer.API/"]
COPY ["Application/Models/GeoPlotAnalyzer.Models/GeoPlotAnalyzer.Models.csproj", "Application/Models/GeoPlotAnalyzer.Models/"]
COPY ["Application/Services/GeoPlotAnalyzer.Services.Logger/GeoPlotAnalyzer.Services.Logger.csproj", "Application/Services/GeoPlotAnalyzer.Services.Logger/"]
COPY ["Application/Services/GeoPlotAnalyzer.Services.Settings/GeoPlotAnalyzer.Services.Settings.csproj", "Application/Services/GeoPlotAnalyzer.Services.Settings/"]
COPY ["Shared/GeoPlotAnalyzer.Common/GeoPlotAnalyzer.Common.csproj", "Shared/GeoPlotAnalyzer.Common/"]
RUN dotnet restore "./Systems/API/GeoPlotAnalyzer.API/GeoPlotAnalyzer.API.csproj"
COPY . .
WORKDIR "/src/Systems/API/GeoPlotAnalyzer.API"
RUN dotnet build "./GeoPlotAnalyzer.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GeoPlotAnalyzer.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeoPlotAnalyzer.API.dll"]