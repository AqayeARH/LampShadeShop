﻿using Lampshade.Query.Contracts.Product;
using _0.Framework.Application;
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
        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
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
                    ExpireDiscountDate = discounts.FirstOrDefault(d => d.ProductId == x.Id)?.EndDate.ToShortDateString()
                }).AsQueryable();

            if (!string.IsNullOrWhiteSpace(value))
            {
                query = query.Where(x => x.Name.Contains(value));
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}