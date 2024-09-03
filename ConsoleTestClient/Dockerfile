FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
# COPY ["ConsoleTestClient/ConsoleTestClient.csproj", "ConsoleTestClient/"]
RUN dotnet restore "ConsoleTestClient/ConsoleTestClient.csproj"
# COPY . .
# WORKDIR "/src/ConsoleTestClient"
RUN dotnet build "ConsoleTestClient/ConsoleTestClient.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ConsoleTestClient/ConsoleTestClient.csproj" -c Release -o /app
RUN dotnet dev-certs https --trust

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "ConsoleTestClient.dll"]