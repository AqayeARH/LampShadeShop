using System.Text.Encodings.Web;
using System.Text.Unicode;
using _0.Framework.Application;
using _0.Framework.Application.Authentication;
using _0.Framework.Application.PasswordHasher;
using AccountManagement.Infra.Configuration;
using BlogManagement.Infra.Configuration;
using CommentManagement.Infra.Configuration;
using DiscountManagement.Infra.Configuration;
using InventoryManagement.Infra.Configuration;
using Lampshade.WebApp;
using Microsoft.AspNetCore.Authentication.Cookies;
using ShopManagement.Infra.Configuration;

var builder = WebApplication.CreateBuilder(args);

#region Services
var service = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("LampshadeConnection");

ShopManagementIoc.Configure(service, connectionString);
DiscountManagementIoc.Configure(service,connectionString);
InventoryManagementIoc.Configure(service,connectionString);
BlogManagementIoc.Configure(service,connectionString);
CommentManagementIoc.Configure(service,connectionString);
AccountManagementIoc.Configure(service,connectionString);

service.AddTransient<IFileUploader, FileUploader>();
service.AddTransient<IAuthenticationHelper, AuthenticationHelper>();
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
service.AddRazorPages();
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
app.UseCookiePolicy();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
