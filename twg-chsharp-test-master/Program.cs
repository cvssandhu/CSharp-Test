
using ServiceContracts;
using IRepository;
using Repository;
using Services;
using Microsoft.EntityFrameworkCore;
using CSharpTest.Extentions;

var builder = WebApplication.CreateBuilder(args);

/******C Sandhu Changes ************/

// Add services to the container.

builder.Services.AddScoped<ICommonRepository, CommonRepository>();

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



//var logger = app.Services.GetRequiredService<ILogger>();
//app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment ()) {
    app.UseExceptionHandler ("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts ();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
