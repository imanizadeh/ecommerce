FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS sdk
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ECommerce.ProductManagement/ECommerce.ProductManagement.csproj", "src/ECommerce.ProductManagement/"]
COPY ["src/ECommerce.ServiceDefaults/ECommerce.ServiceDefaults.csproj", "src/ECommerce.ServiceDefaults/"]
COPY ["src/ECommerce.SharedFramework/ECommerce.SharedFramework.csproj", "src/ECommerce.SharedFramework/"]
COPY ["src/ECommerce.Contracts/ECommerce.Contracts.csproj", "src/ECommerce.Contracts/"]
RUN dotnet restore "src/ECommerce.ProductManagement/ECommerce.ProductManagement.csproj"
COPY . .
WORKDIR "/src/src/ECommerce.ProductManagement"
RUN dotnet publish "ECommerce.ProductManagement.csproj" -c $BUILD_CONFIGURATION  -o /app/publish /p:UseAppHost=false --no-restore --nologo

FROM base AS final
WORKDIR /app
COPY --from=sdk /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.ProductManagement.dll"]
