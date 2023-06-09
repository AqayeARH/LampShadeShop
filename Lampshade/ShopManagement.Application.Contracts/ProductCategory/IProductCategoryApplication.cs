﻿using _0.Framework.Application;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        OperationResult Create(CreateProductCategoryCommand command);
        EditProductCategoryCommand GetForEdit(long id);
        OperationResult Edit(EditProductCategoryCommand command);
        List<ProductCategoryViewModel> GetList(ProductCategorySearchModel searchModel);
        List<ProductCategoryViewModel> GetProductCategories();
    }
}
