using DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorPages.Data;
using RazorPages.IRepositories;
using RazorPages.Repositories;
using Microsoft.Extensions;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IDataRepository, DataRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IProducerRepository, ProducerRepository>();
builder.Services.AddScoped<ICationRepository, CationRepository>();
builder.Services.AddScoped<IAnionRepository, AnionRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IPalletRepository, PalletRepository>();
builder.Services.AddScoped<ISaleEntryRepository, SaleEntryRepository>();

builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("LoggedIn",
        policy => policy.RequireAuthenticatedUser());
});

// Only logged in users can use those pages
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/Products/Create", "LoggedIn");
    options.Conventions.AuthorizePage("/Products/Delete", "LoggedIn");
    options.Conventions.AuthorizePage("/Products/Edit", "LoggedIn");
    options.Conventions.AuthorizeFolder("/Producers", "LoggedIn");
    options.Conventions.AuthorizeFolder("/Anions", "LoggedIn");
    options.Conventions.AuthorizeFolder("/Cations", "LoggedIn");
    options.Conventions.AuthorizeFolder("/Deliveries", "LoggedIn");
    options.Conventions.AuthorizeFolder("/Pallets", "LoggedIn");
    options.Conventions.AuthorizeFolder("/SaleEntries", "LoggedIn");
    options.Conventions.AuthorizeFolder("/Sales", "LoggedIn");

});


var app = builder.Build();
app.UseSwaggerUI();

app.UseSwagger(c => c.SerializeAsV2 = true);

// Additional Api
app.MapGet("/storage", ([FromServices] IDataRepository db) => db.GetProducts());
app.MapGet("/watertypes", ([FromServices] IDataRepository db) => db.GetWaterTypes().ToList());
app.MapGet("/registeredusers", ([FromServices] IDataRepository db) => db.GetUserNames());

app.MapPost("/addsale", (Sale sale, [FromServices] IDataRepository db) =>
{
    try
    {
        db.AddSale(sale);
        return Results.Ok(sale);
    }
    catch (Exception)
    {
        return Results.NotFound();
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
