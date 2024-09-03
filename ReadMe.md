This is from here -
https://www.codeproject.com/Articles/5382064/gRPC-NET-8-0-and-Kestrel-in-Easy-Samples

Open terminal in VScode
dotnet run --verbosity diag --project .\GrpcServerProcess\GrpcServerProcess.csproj
Another terminal in Vscode

dotnet run --verbosity diag --project .\ConsoleTestClient\ConsoleTestClient.csproj

Might need https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-8.0&tabs=visual-studio%2Clinux-ubuntu#trust-the-aspnet-core-https-development-certificate-on-windows-and-macos
if certificate not allowed
