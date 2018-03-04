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

            var userBuilder = modelBuilder.Entity<User>()
                .ToTable("Users", schema);

            userBuilder.Property(u => u.EmailStr).HasColumnName("Email");

            modelBuilder.Entity<ExternalAuthProvider>()
                .ToTable("ExternalAuthProviders", schema)
                .HasKey(c => new { c.ExternalUserId, c.ExternalAuthProviderTypeId });

        modelBuilder.Entity<ExternalAuthProviderTypeState>().ToTable("ExternalAuthProviderTypes", schema);

            modelBuilder
                .Entity<Account>()
                .ToTable("Accounts", schema)
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId);
                

            modelBuilder
                .Entity<SMTask>()
                .ToTable("Tasks", schema)
                .HasOne(t => t.Account)
                .WithMany()
                .HasForeignKey(t => t.AccountId);

            var tagTaskBuilder = modelBuilder
                .Entity<TagTask>()
                .ToTable("TagTasks", schema);

            tagTaskBuilder.HasKey(t => t.TaskId);
            tagTaskBuilder.Property(t => t.TagsJson).HasColumnName("Tags");
            tagTaskBuilder.Property(t => t.LastPostJson).HasColumnName("LastPost");
            tagTaskBuilder.Property(t => t.PostsJson).HasColumnName("Posts");
            tagTaskBuilder.Property(t => t.FollowersJson).HasColumnName("Followers");
            tagTaskBuilder.Property(t => t.FollowingsJson).HasColumnName("Followings");
        }

        public DataContext CreateNewInstance()
        {
            return new DataContext(_connectionString);
        }
    }
}
