FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# RUN dotnet dev-certs https --trust doesnt work

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
# COPY ["GrpcServerProcess/GrpcServerProcess.csproj", "GrpcServerProcess/"]
RUN dotnet restore "GrpcServerProcess/GrpcServerProcess.csproj"
# COPY . .
# WORKDIR "/src/GrpcServerProcess"
RUN dotnet build "GrpcServerProcess/GrpcServerProcess.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "GrpcServerProcess/GrpcServerProcess.csproj" -c Release -o /app
# RUN dotnet dev-certs https --trust
# RUN dotnet dev-certs https
# RUN dotnet tool update -g linux-dev-certs doesnt worl
# RUN dotnet-linux-dev-certs install
RUN mkdir -p /app/certificates
RUN dotnet dev-certs https --check
COPY certificates/aspnetapp.pfx /app/certificates

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
# RUN dotnet dev-certs https --trust not useful

RUN dotnet dev-certs https --check

ENTRYPOINT ["dotnet", "GrpcServerProcess.dll"]