FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS sdk
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ECommerce.Inventory/ECommerce.Inventory.csproj", "src/ECommerce.Inventory/"]
COPY ["src/ECommerce.ServiceDefaults/ECommerce.ServiceDefaults.csproj", "src/ECommerce.ServiceDefaults/"]
COPY ["src/ECommerce.SharedFramework/ECommerce.SharedFramework.csproj", "src/ECommerce.SharedFramework/"]
RUN dotnet restore "./src/ECommerce.Inventory/ECommerce.Inventory.csproj"
COPY . .
WORKDIR "/src/src/ECommerce.Inventory"
RUN dotnet publish "ECommerce.Inventory.csproj" -c $BUILD_CONFIGURATION  -o /app/publish /p:UseAppHost=false --no-restore --nologo

FROM base AS final
WORKDIR /app
COPY --from=sdk /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Inventory.dll"]