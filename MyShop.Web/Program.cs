using Microsoft.EntityFrameworkCore;
using MyShop.Application.Services;
using MyShop.DataAccess.Data;
using MyShop.DataAccess.Implementation;
using MyShop.Entities.Repositories;
using MyShop.Web.Services;
using Microsoft.AspNetCore.Identity;
using MyShop.Entities.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using MyShop.Web.Helper;
using System.Configuration;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.Configure<StripeKeys>(builder.Configuration.GetSection("stripe"));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ICategoryService,CategoryService>();
builder.Services.AddTransient<IProductService, MyShop.Application.Services.ProductService>();
builder.Services.AddTransient<IImageService,ImageService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<IShoppingCartService,ShoppingCartService>();
builder.Services.AddTransient<IApplicationUserService,ApplicationUserService>();
builder.Services.AddTransient<IOrderHeaderService, OrderHeaderService>();
builder.Services.AddTransient<IOrderDetailService, OrderDetailService>();
builder.Services.AddTransient<IOrderService, OrderService>();


builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
StripeConfiguration.ApiKey= builder.Configuration.GetSection("stripe:Secretkey").Get<string>();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapStaticAssets();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
