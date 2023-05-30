using DiscountManagement.Infra.Configuration;
using InventoryManagement.Infra.Configuration;
using ShopManagement.Infra.Configuration;

var builder = WebApplication.CreateBuilder(args);

#region Services
var service = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("LampshadeConnection");

ShopManagementIoc.Configure(service, connectionString);
DiscountManagementIoc.Configure(service,connectionString);
InventoryManagementIoc.Configure(service,connectionString);


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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
