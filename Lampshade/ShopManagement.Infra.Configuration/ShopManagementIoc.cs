using _0.Framework.Infrastructure;
using Lampshade.Query.Contracts.Product;
using Lampshade.Query.Contracts.ProductCategory;
using Lampshade.Query.Contracts.Slider;
using Lampshade.Query.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
using ShopManagement.Infra.Configuration.Permissions;
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

            #region Query

            service.AddTransient<ISliderQuery, SliderQuery>();
            service.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
            service.AddTransient<IProductQuery, ProductQuery>();

            #endregion

            service.AddTransient<IPermissionExposure, ShopPermissionExposure>();

            service.AddDbContext<ShopContext>(options =>
            {
                options.UseSqlServer(connectionString);

                //Use it in ef core 6
                options.ConfigureWarnings(warnings =>
                    warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored));
            });
        }
    }
}