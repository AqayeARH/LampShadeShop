using _0.Framework.Infrastructure;

namespace ShopManagement.Infra.Configuration.Permissions
{
    public class ShopPermissionExposure : IPermissionExposure
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "ProductCategory", new List<PermissionDto>()
                    {
                        new PermissionDto(ShopPermissionCode.ListProductCategories, "ListProductCategories"),
                        new PermissionDto(ShopPermissionCode.CreateProductCategory, "CreateProductCategory"),
                        new PermissionDto(ShopPermissionCode.EditProductCategory, "EditProductCategory")
                    }
                },
                {
                    "Product", new List<PermissionDto>()
                    {
                        new PermissionDto(ShopPermissionCode.ListProducts, "ListProducts"),
                        new PermissionDto(ShopPermissionCode.CreateProduct, "CreateProduct"),
                        new PermissionDto(ShopPermissionCode.EditProduct, "EditProduct")
                    }
                },
                {
                    "ProductPicture", new List<PermissionDto>()
                    {
                        new PermissionDto(ShopPermissionCode.ListProductPictures, "ListProductPictures"),
                        new PermissionDto(ShopPermissionCode.CreateProductPicture, "CreateProductPicture"),
                        new PermissionDto(ShopPermissionCode.EditProductPicture, "EditProductPicture"),
                        new PermissionDto(ShopPermissionCode.ChangeStatusProductPicture, "ChangeStatusProductPicture")
                    }
                },
                {
                    "Slider", new List<PermissionDto>()
                    {
                        new PermissionDto(ShopPermissionCode.ListSliders, "ListSliders"),
                        new PermissionDto(ShopPermissionCode.CreateSlider, "CreateSlider"),
                        new PermissionDto(ShopPermissionCode.EditSlider, "EditSlider"),
                        new PermissionDto(ShopPermissionCode.ChangeStatusSlider, "ChangeStatusSlider")
                    }
                },
            };
        }
    }
}
