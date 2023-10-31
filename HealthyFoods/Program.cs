using HealthyFoods.Application;
using HealthyFoods;
using HealthyFoods.Persistence;
using Serilog;
using CurrieTechnologies.Razor.SweetAlert2;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.
builder.Services
    .AddPersistence(configuration)
    .RegisDenpendencies(configuration)
    .AddApplication();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSweetAlert2();

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseExceptionHandler("/Error");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
