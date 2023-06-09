using CommentManagement.Application;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infra.EfCore;
using CommentManagement.Infra.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace CommentManagement.Infra.Configuration
{
    public class CommentManagementIoc
    {
        public static void Configure(IServiceCollection service,string connectionString)
        {
            #region  Comment

            service.AddTransient<ICommentRepository, CommentRepository>();
            service.AddTransient<ICommentApplication, CommentApplication>();

            #endregion

            service.AddDbContext<CommentingContext>(options =>
            {
                options.UseSqlServer(connectionString);

                options.ConfigureWarnings(warnings =>
                    warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored));
            });
        }
    }
}