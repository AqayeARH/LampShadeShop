using BlogManagement.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infra.EfCore;
using BlogManagement.Infra.EfCore.Repositories;
using Lampshade.Query.Contracts.Article;
using Lampshade.Query.Contracts.ArticleCategory;
using Lampshade.Query.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagement.Infra.Configuration
{
    public static class BlogManagementIoc
    {
        public static void Configure(IServiceCollection service,string connectionString)
        {
            #region Article Category

            service.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();
            service.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();

            #endregion

            #region Article

            service.AddTransient<IArticleRepository, ArticleRepository>();
            service.AddTransient<IArticleApplication, ArticleApplication>();

            #endregion

            #region Query

            service.AddTransient<IArticleQuery, ArticleQuery>();
            service.AddTransient<IArticleCategoryQuery, ArticleCategoryQuery>();

            #endregion

            service.AddDbContext<BlogContext>(options =>
            {
                options.UseSqlServer(connectionString);

                //Use it in ef core 6
                options.ConfigureWarnings(warnings =>
                    warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored));
            });
        }
    }
}