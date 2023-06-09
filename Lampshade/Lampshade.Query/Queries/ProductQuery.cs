using Lampshade.Query.Contracts.Product;
using _0.Framework.Application;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infra.EfCore;
using DiscountManagement.Infra.EfCore;
using InventoryManagement.Infra.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infra.EfCore;

namespace Lampshade.Query.Queries
{
    public class ProductQuery : IProductQuery
    {
        #region Constractor Injection

        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        private readonly CommentingContext _commentingContext;
        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext, CommentingContext commentingContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
            _commentingContext = commentingContext;
        }

        #endregion
        public List<ProductQueryModel> GetLatestProducts()
        {
            var inventories = _inventoryContext.Inventories
                .Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            return _shopContext.Products
                .Include(x => x.Category)
                .OrderByDescending(x => x.Id)
                .Take(6)
                .AsEnumerable()
                .Select(x => new ProductQueryModel()
                {
                    CategoryName = x.Category.Name,
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    Price = inventories.FirstOrDefault(i => i.ProductId == x.Id) != null ? inventories.FirstOrDefault(i => i.ProductId == x.Id)?.UnitPrice.ToMoney() + "تومان" : "ناموجود در انبار",
                    HasDiscount = discounts.FirstOrDefault(d => d.ProductId == x.Id) != null,
                    DiscountRate = discounts.FirstOrDefault(d => d.ProductId == x.Id) != null ? discounts.FirstOrDefault(d => d.ProductId == x.Id)!.DiscountRate : 0,
                    PriceAfterDiscount = discounts.FirstOrDefault(d => d.ProductId == x.Id) != null ? (inventories.FirstOrDefault(i => i.ProductId == x.Id)!.UnitPrice - Math.Round(inventories.FirstOrDefault(i => i.ProductId == x.Id)!.UnitPrice) * (discounts.FirstOrDefault(d => d.ProductId == x.Id)!.DiscountRate) / 100).ToMoney() + "تومان" : "",
                    CategorySlug = x.Category.Slug
                }).ToList();
        }

        public List<ProductQueryModel> Search(string value)
        {
            var inventories = _inventoryContext.Inventories
                .Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();

            var query = _shopContext.Products
                .Include(x => x.Category)
                .AsEnumerable()
                .Select(x => new ProductQueryModel()
                {
                    CategoryName = x.Category.Name,
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    Price = inventories.FirstOrDefault(i => i.ProductId == x.Id) != null ? inventories.FirstOrDefault(i => i.ProductId == x.Id)?.UnitPrice.ToMoney() + "تومان" : "ناموجود در انبار",
                    HasDiscount = discounts.FirstOrDefault(d => d.ProductId == x.Id) != null,
                    DiscountRate = discounts.FirstOrDefault(d => d.ProductId == x.Id) != null ? discounts.FirstOrDefault(d => d.ProductId == x.Id)!.DiscountRate : 0,
                    PriceAfterDiscount = discounts.FirstOrDefault(d => d.ProductId == x.Id) != null ? (inventories.FirstOrDefault(i => i.ProductId == x.Id)!.UnitPrice - Math.Round(inventories.FirstOrDefault(i => i.ProductId == x.Id)!.UnitPrice) * (discounts.FirstOrDefault(d => d.ProductId == x.Id)!.DiscountRate) / 100).ToMoney() + "تومان" : "",
                    CategorySlug = x.Category.Slug,
                    ExpireDiscountDate = discounts.FirstOrDefault(d => d.ProductId == x.Id)?.EndDate.ToShortDateString(),
                    Tags = x.KayWords
                }).AsQueryable();

            if (!string.IsNullOrWhiteSpace(value))
            {
                query = query.Where(x => x.Name.Contains(value) || x.Tags.Contains(value));
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public ProductQueryModel GetDetailsBy(string slug)
        {
            var inventories = _inventoryContext.Inventories
                .Select(x => new { x.ProductId, x.UnitPrice, x.InStock }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();


            return _shopContext.Products.Include(x => x.Category)
                .Include(x => x.ProductPictures)
                .AsEnumerable()
                .Select(x => new ProductQueryModel()
                {
                    Id = x.Id,
                    CategoryName = x.Category.Name,
                    CategorySlug = x.Category.Slug,
                    DiscountRate = discounts.FirstOrDefault(d => d.ProductId == x.Id) != null ? discounts.FirstOrDefault(d => d.ProductId == x.Id)!.DiscountRate : 0,
                    ExpireDiscountDate = discounts.FirstOrDefault(d => d.ProductId == x.Id)?.EndDate.ToShortDateString(),
                    HasDiscount = discounts.FirstOrDefault(d => d.ProductId == x.Id) != null,
                    Name = x.Name,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    Price = inventories.FirstOrDefault(i => i.ProductId == x.Id) != null ? inventories.FirstOrDefault(i => i.ProductId == x.Id)?.UnitPrice.ToMoney() + "تومان" : "ناموجود در انبار",
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PriceAfterDiscount = discounts.FirstOrDefault(d => d.ProductId == x.Id) != null ? (inventories.FirstOrDefault(i => i.ProductId == x.Id)!.UnitPrice - Math.Round(inventories.FirstOrDefault(i => i.ProductId == x.Id)!.UnitPrice) * (discounts.FirstOrDefault(d => d.ProductId == x.Id)!.DiscountRate) / 100).ToMoney() + "تومان" : "",
                    Code = x.Code,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    Tags = x.KayWords,
                    MetaDescription = x.MetaDescription,
                    IsInStock = inventories.FirstOrDefault(i => i.ProductId == x.Id) != null && inventories.FirstOrDefault(i => i.ProductId == x.Id)!.InStock,
                    ProductPictures = x.ProductPictures
                        .Where(productPicture => productPicture.IsRemoved == false)
                        .AsEnumerable().Select(productPicture => new ProductPicturesQueryModel()
                        {
                            IsRemoved = productPicture.IsRemoved,
                            Picture = productPicture.Picture,
                            PictureAlt = productPicture.PictureAlt,
                            PictureTitle = productPicture.PictureTitle
                        }).ToList(),
                    ProductComments = _commentingContext.Comments
                        .Where(c=> c.Type == CommentTypes.Product && c.OwnerRecordId == x.Id && c.Status == CommentStatuses.Confirmed)
                        .Select(c=> new ProductCommentQueryModel()
                        {
                            Name = c.Name,
                            CreationDate = c.CreationDate.ToFarsi(),
                            Id = c.Id,
                            Text = c.Text
                        }).OrderByDescending(c=>c.Id).ToList(),
                }).FirstOrDefault(x => x.Slug == slug);
        }
    }
}
