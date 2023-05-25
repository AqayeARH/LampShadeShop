using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slider;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SliderAgg;
using ShopManagement.Infra.EfCore;
using ShopManagement.Infra.EfCore.Repositories;

namespace ShopManagement.Infra.Configuration
{
    public static class ShopManagementIoc
    {
        public static void Configure(IServiceCollection service, string connectionString)
        {
            #region Product Category

            service.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            service.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();

            #endregion

            #region Product

            service.AddTransient<IProductRepository, ProductRepository>();
            service.AddTransient<IProductApplication,ProductApplication>();

            #endregion

            #region Product Picture

            service.AddTransient<IProductPictureRepository, ProductPictureRepository>();
            service.AddTransient<IProductPictureApplication, ProductPictureApplication>();

            #endregion

            #region Slider

            service.AddTransient<ISliderRepository, SliderRepository>();
            service.AddTransient<ISliderApplication, SliderApplication>();

            #endregion

            service.AddDbContext<ShopContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}