FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# EXPOSE 443
# EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "GrpcServerProcess/GrpcServerProcess.csproj"
RUN dotnet build "GrpcServerProcess/GrpcServerProcess.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "GrpcServerProcess/GrpcServerProcess.csproj" -c Release -o /app
RUN mkdir -p /app/certificates
COPY certificates/aspnetapp.pfx /app/certificates/

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GrpcServerProcess.dll"]