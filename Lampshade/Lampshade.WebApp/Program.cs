using System.Text.Encodings.Web;
using System.Text.Unicode;
using _0.Framework.Application;
using _0.Framework.Application.Authentication;
using _0.Framework.Application.PasswordHasher;
using _0.Framework.Application.ZarinPal;
using _0.Framework.Infrastructure;
using AccountManagement.Infra.Configuration;
using BlogManagement.Infra.Configuration;
using CommentManagement.Infra.Configuration;
using DiscountManagement.Infra.Configuration;
using InventoryManagement.Infra.Configuration;
using InventoryManagement.Presentation.Api.Controllers;
using Lampshade.Query.Contracts;
using Lampshade.Query.Queries;
using Lampshade.WebApp;
using Microsoft.AspNetCore.Authentication.Cookies;
using ShopManagement.Infra.Configuration;

var builder = WebApplication.CreateBuilder(args);

#region Services
var service = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("LampshadeConnection");

ShopManagementIoc.Configure(service, connectionString);
DiscountManagementIoc.Configure(service, connectionString);
InventoryManagementIoc.Configure(service, connectionString);
BlogManagementIoc.Configure(service, connectionString);
CommentManagementIoc.Configure(service, connectionString);
AccountManagementIoc.Configure(service, connectionString);

service.AddTransient<IFileUploader, FileUploader>();
service.AddTransient<IAuthenticationHelper, AuthenticationHelper>();
service.AddTransient<ICartCalculateService, CartCalculateService>();
service.AddTransient<IZarinPalFactory, ZarinPalFactory>();
service.AddSingleton<IPasswordHasher, PasswordHasher>();
service.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

service.Configure<CookiePolicyOptions>(options =>
{
	options.CheckConsentNeeded = context => true;
	options.MinimumSameSitePolicy = SameSiteMode.Lax;
});
service.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
    {
        o.LoginPath = new PathString("/Account");
        o.LogoutPath = new PathString("/Account");
        o.AccessDeniedPath = new PathString("/AccessDenied");
    });
service.AddHttpContextAccessor();
service.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", configurePolicy =>
    {
        configurePolicy.RequireRole(new List<string> { Roles.Administrator.ToString(), Roles.Operator.ToString() });
    });

    options.AddPolicy("DiscountsManagement", configurePolicy =>
    {
        configurePolicy.RequireRole(new List<string> { Roles.Administrator.ToString() });
    });

    options.AddPolicy("AccountsManagement", configurePolicy =>
    {
        configurePolicy.RequireRole(new List<string> { Roles.Administrator.ToString() });
    });

    options.AddPolicy("InventoryManagement", configurePolicy =>
    {
        configurePolicy.RequireRole(new List<string> { Roles.Administrator.ToString() });
    });
});

service.AddCors(options => options.AddPolicy("CORS", corsPolicyBuilder =>
{
    corsPolicyBuilder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
}));

service.AddRazorPages()
    .AddMvcOptions(options =>
    {
        options.Filters.Add<SecurityPageFilter>();
    })
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeAreaFolder("Administrator", "/", "Administrator");
        options.Conventions.AuthorizeAreaFolder("Administrator", "/DiscountsManagement", "DiscountsManagement");
        options.Conventions.AuthorizeAreaFolder("Administrator", "/AccountsManagement", "AccountsManagement");
        options.Conventions.AuthorizeAreaFolder("Administrator", "/InventoryManagement", "InventoryManagement");
    })
    .AddApplicationPart(typeof(InventoryController).Assembly)
    .AddNewtonsoftJson();  //In the new .net, due to json formatting, this piece of code should be used and the package related to asp.net core mvc json should be installed.

service.AddControllers();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.UseCors("CORS");
app.Run();
