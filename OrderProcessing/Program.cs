using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOcelot(builder.Configuration)
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    });
//builder.Services.SetBasePath(HostingEnvironment.ContentRootPath);
builder.Configuration.AddJsonFile("appsettings.json", true, true);
//builder.Configuration.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
builder.Configuration.AddJsonFile("ocelot.json");
builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseOcelot();
app.MapGet("/", () => "Hello World!");

app.Run();
