using GrpcServerProcess;
using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

// Add ASP.NET razor file generation service
builder.Services.AddRazorPages();

// add grpc service
builder.Services.AddGrpc();

// build grpc application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseDefaultFiles();

// add the content types that might be needed for 
// sending .wasm files back to the client. 
var contentTypeProvider = new FileExtensionContentTypeProvider();
var dict = new Dictionary<string, string>
    {
        {".pdb" , "application/octet-stream" },
        {".blat", "application/octet-stream" },
        {".bin", "application/octet-stream" },
        {".dll" , "application/octet-stream" },
        {".dat" , "application/octet-stream" },
        {".json", "application/json" },
        {".wasm", "application/wasm" },
        {".symbols", "application/octet-stream" }
    };
foreach (var kvp in dict)
{
    contentTypeProvider.Mappings[kvp.Key] = kvp.Value;
}
app.UseStaticFiles(new StaticFileOptions { ContentTypeProvider = contentTypeProvider });

app.UseGrpcWeb();

app.MapRazorPages();

app.MapGrpcService<GreeterImplementation>().EnableGrpcWeb();

app.Run();
