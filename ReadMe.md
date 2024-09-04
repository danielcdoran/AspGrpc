This is from here -
https://www.codeproject.com/Articles/5382064/gRPC-NET-8-0-and-Kestrel-in-Easy-Samples
Server is on port 55003

Open terminal in VScode
dotnet run --verbosity diag --project .\GrpcServerProcess\GrpcServerProcess.csproj
Another terminal in Vscode

dotnet run --verbosity diag --project .\ConsoleTestClient\ConsoleTestClient.csproj

Might need https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-8.0&tabs=visual-studio%2Clinux-ubuntu#trust-the-aspnet-core-https-development-certificate-on-windows-and-macos
if certificate not allowed

https://github.com/dotnet/AspNetCore.Docs/issues/6199
https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-8.0&tabs=visual-studio%2Clinux-ubuntu#trust-the-aspnet-core-https-development-certificate-on-windows-and-macos

https://github.com/dotnet/dotnet-docker/blob/main/samples/run-aspnetcore-https-development.md
https://docs.servicestack.net/netcore-localhost-cert#accessing-from-native-applications
talks about appsettings.Development.json

https://andrewlock.net/8-ways-to-set-the-urls-for-an-aspnetcore-app/#:~:text=The%20ASPNETCORE_URLS%20and%20similar%20environment,the%20loopback%20(%20localhost%20address).
configuraing URLs in Kestrel

Create certificate directory on my PC
dotnet dev-certs https -ep $env:userprofile\.aspnet\https\aspnetapp.pfx -p password

dotnet dev-certs https --trust
Trusting the HTTPS development certificate was requested. A confirmation prompt will be displayed if the certificate was not previously trusted. Click yes on the prompt to trust the certificate.

cd certificates
copy certificate (aspnetapp.pfx) in directory
cp $env:userprofile\.aspnet\https\aspnetapp.pfx .

Client asaccess server on https://localhost:55003

server set in apsettings.json needs to be 
  "AllowedHosts": "*",
  "Kestrel": {
    "Endpoints": {
      "Grpc": {
        "Url": "https://*:55003",
        "Protocols": "Http2"
      }
    }
  }
}

to allow any item to call this service needs to be https://*:5503

docker-compose up --build

Problem
I dont seem to have the TLS configured correctly or the certificate is not correct. I have instituted a TLS certificate bypass to get this to work
https://learn.microsoft.com/en-us/answers/questions/693913/grpc-the-remote-certificate-is-invalid-because-of
