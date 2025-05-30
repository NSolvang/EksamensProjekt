using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using EksamensProjekt;
using EksamensProjekt.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
   BaseAddress = new Uri("https://itaapi8.azurewebsites.net")
   // BaseAddress = new Uri("http://localhost:5094")

}); 
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<ISubgoal, SubgoalService>();
builder.Services.AddScoped<ILogin, LoginService>();
builder.Services.AddScoped<IComment, CommentService>();
builder.Services.AddScoped<ILocation, LocationService>();
builder.Services.AddScoped<AuthStateService>();
builder.Services.AddScoped<IExport, ExportService>();


await builder.Build().RunAsync();