FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NotificationsService/NotificationsService.csproj", "NotificationsService/"]
RUN dotnet restore "NotificationsService/NotificationsService.csproj"
COPY . .
WORKDIR "/src/NotificationsService"
RUN dotnet build "NotificationsService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationsService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationsService.dll"]
