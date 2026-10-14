using KooliProjekt.BlazorWASM;
using KooliProjekt.BlazorWASM.Api;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("http://localhost:5086/") });
builder.Services.AddScoped<EventsApiClient>();
await builder.Build().RunAsync();
