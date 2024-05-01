using Microsoft.Extensions.Configuration;
using BlazorServer.Servicios;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);


//configuration
var configuration = builder.Configuration;
var api = configuration.GetValue<string>("RutaApi");

// Add services to the container.
//Tamaño de imagenes
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 2428800; // 50 MB
});
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient<IServicioAlumnos,ServicioAlumnos>(
    cliente=>cliente.BaseAddress=new Uri(api)
    );
builder.Services.AddHttpClient<IServicioCursos, ServicioCurso>(
    cliente => cliente.BaseAddress = new Uri(api)
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
