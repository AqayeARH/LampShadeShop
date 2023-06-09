using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infra.EfCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infra.EfCore
{
    public class CommentingContext : DbContext
    {
        public CommentingContext(DbContextOptions<CommentingContext> options) : base(options)
        {

        }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CommentMapping).Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}