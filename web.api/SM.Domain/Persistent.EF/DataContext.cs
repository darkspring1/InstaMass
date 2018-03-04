using SM.Domain.Persistent.EF.State;
using Microsoft.EntityFrameworkCore;
using SM.Domain.Model;

namespace SM.Domain.Persistent.EF
{
    public class DataContext : DbContext
    {
        const string schema = "public";

        readonly string _connectionString;

        public DataContext(string connectionString)
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

        public DataContext CreateNewInstance()
        {
            return new DataContext(_connectionString);
        }
    }
}
