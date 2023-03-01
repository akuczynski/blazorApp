using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ElectronNET.API;
using BlazorApp;
using System.Reflection.PortableExecutable;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddElectron();
builder.WebHost.UseElectron(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Host.UseAutofac();  //Add this line

await builder.AddApplicationAsync<AppModule>();
var app = builder.Build();

await app.InitializeApplicationAsync();
await app.RunAsync();

if (HybridSupport.IsElectronActive)
{
    // Open the Electron-Window here
    Task.Run(async () =>
    {
        var window = await Electron.WindowManager.CreateWindowAsync();
        window.OnClosed += () =>
        {
            Electron.App.Quit();
        };
    });
}
