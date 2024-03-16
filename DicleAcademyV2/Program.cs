using DicleAcademyV2;
using DicleAcademyV2.Extencion;
using DicleAcademyV2.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Repositories.AutoMapper;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services
    .AddRazorPages()
    .AddViewLocalization();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.ConfiguratioSQLContext(builder.Configuration);
builder.Services.AddSingleton<LocalizationMiddleware>();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
builder.Services.ConfiguerRepostoryManager();
builder.Services.ConfiguerServiceManager();
builder.Services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization();
builder.Services.AddControllersWithViews();
builder.Services.AddLocalization();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("tr-TR"),
        new CultureInfo("en-US")
    };

    //options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
    options.DefaultRequestCulture = new RequestCulture(new CultureInfo("tr-TR"));
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null; // Disable camelCase
        });
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var localizationOptions = services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
    app.UseRequestLocalization(localizationOptions);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

builder.Services.InitializeClientBaseAddress(builder.Configuration);
app.UseStaticFiles();
app.UseMiddleware<LocalizationMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "{controller=Admin}/{action=SignIn}/{id?}");

app.MapAreaControllerRoute(
    name: "Client",
    areaName: "Client",
    pattern: "{controller=Client}/{action=Index}/{id?}");

app.Run();
