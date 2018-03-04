using System;
using System.Linq;
using System.Linq.Expressions;
using SM.Domain.Persistent.EF.State;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SM.Domain.Model;

namespace SM.Domain.Persistent.EF
{
    public class EFDataContext : DbContext, IEntityFrameworkDataContext
    {
        const string schema = "public";


        readonly string _connectionString;

        public EFDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AuthToken>()
                .ToTable("AuthTokens", schema)
                .HasKey(c => c.Token);
            modelBuilder.Entity<User>().ToTable("Users", schema);

            modelBuilder.Entity<ExternalAuthProvider>()
                .ToTable("ExternalAuthProviders", schema)
                .HasKey(c => new { c.ExternalUserId, c.ExternalAuthProviderTypeId });

        modelBuilder.Entity<ExternalAuthProviderTypeState>().ToTable("ExternalAuthProviderTypes", schema);

            modelBuilder
                .Entity<Account>()
                .ToTable("Accounts", schema);
                

            modelBuilder
                .Entity<SMTask>()
                .ToTable("Tasks", schema)
                .HasOne(t => t.Account)
                .WithMany()
                .HasForeignKey(t => t.AccountId);

            var tt = modelBuilder
                .Entity<TagTask>()
                .ToTable("TagTasks", schema);
                //.HasOne(t => t.Task)
                //.WithOne();
            tt.Property(t => t.TagsJson).HasColumnName("Tags");
            tt.Property(t => t.LastPostJson).HasColumnName("LastPost");
            tt.Property(t => t.PostsJson).HasColumnName("Posts");
            tt.Property(t => t.FollowersJson).HasColumnName("Followers");
            tt.Property(t => t.FollowingsJson).HasColumnName("Followings");
        }

        int IEntityFrameworkDataContext.SaveChanges()
        {
            return this.SaveChanges();
        }

        public IQueryable<T> Include<T>(IQueryable<T> source, params Expression<Func<T, object>>[] path) where T : class
        {
            foreach (var p in path) { source = source.Include(p); }
            return source;
        }

        Task<int> IEntityFrameworkDataContext.SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public IEntityFrameworkDataContext CreateNewInstance()
        {
            return new EFDataContext(_connectionString);
        }
    }
}
