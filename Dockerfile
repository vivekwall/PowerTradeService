FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["./PowerTradeService/PowerTradeService.csproj", "PowerTradeService/"]
COPY ["./PowerTradeService/lib/PowerService.dll", "PowerTradeService/lib/"]
RUN dotnet restore "PowerTradeService/PowerTradeService.csproj"

# Copy the entire project and build the application
COPY . .
WORKDIR "/src/PowerTradeService"
RUN dotnet build "./PowerTradeService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PowerTradeService.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Final stage to set up the runtime environment
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PowerTradeService.dll"]