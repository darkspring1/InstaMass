using System;
using System.Linq;
using System.Linq.Expressions;
using SM.Domain.Persistent.EF.State;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SM.Domain.Persistent.EF
{
    public class EFDataContext : DbContext, IEntityFrameworkDataContext
    {
        const string schema = "public";


        readonly string _connectionString;

        public EFDataContext(string connectionString)
        {
            _connectionString = connectionString;
            /*
            Database.SetInitializer<EFDataContext>(null);
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            */
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //"server=localhost;port=5432;database=SocialMass; user=postgres;password=postgres"
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=password");

            //optionsBuilder.UseNpgsql("server=localhost;port=5432;database=SocialMass; user=postgres;password=postgres");
            optionsBuilder.UseNpgsql(_connectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AuthTokenState>()
                .ToTable("AuthTokens", schema)
                .HasKey(c => c.Token);
            modelBuilder.Entity<UserState>().ToTable("Users", schema);
            modelBuilder.Entity<ApplicationState>().ToTable("Applications", schema);

            modelBuilder.Entity<ExternalAuthProviderState>()
                .ToTable("ExternalAuthProviders", schema)
                .HasKey(c => new { c.ExternalUserId, c.ExternalAuthProviderTypeId });

        modelBuilder.Entity<ExternalAuthProviderTypeState>().ToTable("ExternalAuthProviderTypes", schema);

            modelBuilder.Entity<AccountState>().ToTable("Accounts", schema);
            modelBuilder.Entity<TaskState>().ToTable("Tasks", schema);
            modelBuilder
                .Entity<TagTaskState>()
                .ToTable("TagTasks", schema)
                .HasOne(t => t.Task)
                .WithOne();
                
                //.HasRequired(t => t.Task)
                //.WithRequiredDependent();
        }
        /*
        public void ChangeObjectState(object entity, EntityState entityState)
        {
            var t = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entity);
            ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.ChangeObjectState(entity, entityState);
        }
        */
       

        public IEntityFrameworkDbSet<T> DbSet<T>() where T : class
        {
            return new EntityFrameworkDbSet<T>(Set<T>());
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
