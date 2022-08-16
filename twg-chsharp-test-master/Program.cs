
using ServiceContracts;
using IRepository;
using Repository;
using Services;
using Microsoft.EntityFrameworkCore;
using CSharpTest.Extentions;
using NLog;
using NLog.Web;

var logger = NLogBuilder.ConfigureNLog("NLog.Config").GetCurrentClassLogger();
//var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Init main");


var builder = WebApplication.CreateBuilder(args);


/******C Sandhu Changes ************/
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();
// Add services to the container.

builder.Services.AddScoped<ICommonRepository, CommonRepository>();
builder.Services.AddSingleton<Microsoft.Extensions.Logging.ILogger>(svc => svc.GetRequiredService<ILogger<ProductService>>());
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBillingService, BillingService>();

builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<ISearchRequestRepository, SearchRequestRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DB"), b => b.MigrationsAssembly("CSharpTest")));

builder.Services.AddHttpClient(name: "twg.azure-api.net", configureClient: client =>
{
    client.BaseAddress = new Uri("https://twg.azure-api.net/");
    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", builder.Configuration["SUB_KEY"]);

});
/***************C Sandhu Changes ends here*************/

builder.Services.AddControllers();


var app = builder.Build();


//app.UseSession();
//var logger = app.Services.GetRequiredService<ILogger>();
//app.ConfigureExceptionHandler(logger);
app.ConfigureCustomExceptionMiddleware();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment ()) {
    app.UseExceptionHandler ("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts ();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
