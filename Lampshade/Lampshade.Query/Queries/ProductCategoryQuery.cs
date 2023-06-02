using _0.Framework.Application;
using DiscountManagement.Infra.EfCore;
using InventoryManagement.Infra.EfCore;
using Lampshade.Query.Contracts.Product;
using Lampshade.Query.Contracts.ProductCategory;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infra.EfCore;

namespace Lampshade.Query.Queries
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        #region Constractor Injection

        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        #endregion
        public List<ProductCategoryQueryModel> ShowCategoriesInIndex()
        {
            return _shopContext.ProductCategories
                .Where(x => x.Products.Any())
                .Take(3)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug
                }).ToList();
        }

        public List<ProductCategoryQueryModel> GetCategoriesWithProductsInIndex()
        {
            var inventories = _inventoryContext.Inventories
                .Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            return _shopContext.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Where(x => x.Products.Any())
                .OrderByDescending(x => x.Id)
                .Take(4)
                .AsEnumerable()
                .Select(x => new ProductCategoryQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CategoryProducts = x.Products.OrderByDescending(p => p.Id)
                        .Take(6)
                        .Select(p => new ProductQueryModel()
                        {
                            Id = p.Id,
                            Name = p.Name,
                            CategoryName = p.Category.Name,
                            Picture = p.Picture,
                            PictureAlt = p.PictureAlt,
                            Slug = p.Slug,
                            PictureTitle = p.PictureTitle,
                            Price = inventories.FirstOrDefault(i => i.ProductId == p.Id) != null ? inventories.FirstOrDefault(i => i.ProductId == p.Id)?.UnitPrice.ToMoney() + "تومان" : "ناموجود در انبار",
                            DiscountRate = discounts.FirstOrDefault(d => d.ProductId == p.Id) == null ? 0 : discounts.FirstOrDefault(d => d.ProductId == p.Id)!.DiscountRate,
                            HasDiscount = discounts.FirstOrDefault(d => d.ProductId == p.Id) != null,
                            PriceAfterDiscount = discounts.FirstOrDefault(d => d.ProductId == p.Id) != null ? (inventories.FirstOrDefault(i => i.ProductId == p.Id)!.UnitPrice - Math.Round(inventories.FirstOrDefault(i => i.ProductId == p.Id)!.UnitPrice) * (discounts.FirstOrDefault(d => d.ProductId == p.Id)!.DiscountRate) / 100).ToMoney() + "تومان" : ""
                        }).ToList(),
                    Slug = x.Slug
                }).ToList();
        }

        public ProductCategoryQueryModel GetCategoryWithProducts(string slug)
        {
            var inventories = _inventoryContext.Inventories
                .Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();

            return _shopContext.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .AsEnumerable()
                .Select(x => new ProductCategoryQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    CategoryProducts = x.Products.
                        OrderByDescending(p => p.Id)
                        .AsEnumerable()
                        .Select(p => new ProductQueryModel()
                        {
                            Id = p.Id,
                            CategorySlug = p.Category.Slug,
                            Name = p.Name,
                            CategoryName = p.Category.Name,
                            Picture = p.Picture,
                            PictureAlt = p.PictureAlt,
                            Slug = p.Slug,
                            PictureTitle = p.PictureTitle,
                            Price = inventories.FirstOrDefault(i => i.ProductId == p.Id) != null ? inventories.FirstOrDefault(i => i.ProductId == p.Id)?.UnitPrice.ToMoney() + "تومان" : "ناموجود در انبار",
                            DiscountRate = discounts.FirstOrDefault(d => d.ProductId == p.Id) == null ? 0 : discounts.FirstOrDefault(d => d.ProductId == p.Id)!.DiscountRate,
                            HasDiscount = discounts.FirstOrDefault(d => d.ProductId == p.Id) != null,
                            PriceAfterDiscount = discounts.FirstOrDefault(d => d.ProductId == p.Id) != null ? (inventories.FirstOrDefault(i => i.ProductId == p.Id)!.UnitPrice - Math.Round(inventories.FirstOrDefault(i => i.ProductId == p.Id)!.UnitPrice) * (discounts.FirstOrDefault(d => d.ProductId == p.Id)!.DiscountRate) / 100).ToMoney() + "تومان" : "",
                            ExpireDiscountDate = discounts.FirstOrDefault(d => d.ProductId == p.Id)?.EndDate.ToShortDateString()
                        }).ToList(),
                    KeyWords = x.KeyWords,
                    MetaDescription = x.MetaDescription,
                }).FirstOrDefault(x => x.Slug == slug);
        }
    }
}
