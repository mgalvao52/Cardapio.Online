using Cardapio.Web.BlazorApp.Configurations;
using Cardapio.Web.BlazorApp.Helper;
using Cardapio.Web.BlazorApp.Services;
using Microsoft.JSInterop;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.Configure<Configuration>(builder.Configuration.GetSection("Configuration"));
var url = builder.Configuration["Configuration:BaseUrl"];
builder.Services.AddHttpClient(nameof(MenuService),s=> s.BaseAddress = new Uri(url));
builder.Services.AddSingleton<MenuService>();
builder.Services.AddSingleton<OrderMenuService>();
builder.Services.AddSingleton<OrderPaymentService>();
builder.Services.AddScoped<LocalStorageAccessor>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
