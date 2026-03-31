FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Zafiria.API/Zafiria.API.csproj", "Zafiria.API/"]
COPY ["Zafiria.Application/Zafiria.Application.csproj", "Zafiria.Application/"]
COPY ["Zafiria.Infrastructure/Zafiria.Infrastructure.csproj", "Zafiria.Infrastructure/"]
COPY ["Zafiria.Core/Zafiria.Core.csproj", "Zafiria.Core/"]

RUN dotnet restore "Zafiria.API/Zafiria.API.csproj"

COPY . .

WORKDIR "/src/Zafiria.API"
RUN dotnet build "Zafiria.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zafiria.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zafiria.API.dll"]