using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SM.Domain.Persistent.EF.State;

namespace SM.Domain.Persistent.EF
{
    public class EFDataContext : DbContext, IEntityFrameworkDataContext
    {
        const string schema = "public";

        public EFDataContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<EFDataContext>(null);
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            //this.Database.Log = q => logger.Info(q);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserState>().ToTable("Users", schema);
            modelBuilder.Entity<ApplicationState>().ToTable("Applications", schema);

            modelBuilder.Entity<ExternalAuthProviderState>().ToTable("ExternalAuthProviders", schema);
            modelBuilder.Entity<ExternalAuthProviderTypeState>().ToTable("ExternalAuthProviderTypes", schema);
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

        public IQueryable<T> Include<T>(IQueryable<T> source, params Expression<Func<T, object>>[] path)
        {
            foreach (var p in path) { source = source.Include(p); }
            return source;
        }
    }
}
