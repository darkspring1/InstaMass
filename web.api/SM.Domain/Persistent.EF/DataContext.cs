﻿using SM.Domain.Persistent.EF.State;
using Microsoft.EntityFrameworkCore;
using SM.Domain.Model;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                .HasColumn(c => c.Subject)
                .HasColumn(c => c.ExpiresAt)
                .ToTable("AuthTokens", schema)
                .HasKey(c => c.Token);


            modelBuilder.Entity<User>()
                .HasColumn(u => u.EmailStr, "Email")
                .ToTable("Users", schema);

            modelBuilder.Entity<ExternalAuthProvider>()
                .ToTable("ExternalAuthProviders", schema)
                .HasKey(c => new { c.ExternalUserId, c.ExternalAuthProviderTypeId });

            modelBuilder
                .Entity<ExternalAuthProviderTypeState>()
                .ToTable("ExternalAuthProviderTypes", schema);

            
            modelBuilder
                .Entity<Account>()
                .HasColumn(a => a.UserId)
                .ToTable("Accounts", schema);

            modelBuilder
                .Entity<SMTask>()
                .ToTable("Tasks", schema)
                .HasOne(t => t.Account)
                .WithMany()
                .HasForeignKey(t => t.AccountId);

            modelBuilder
                .Entity<TagTask>()
                .ToTable("TagTasks", schema)
                .HasColumn(t => t.TagsJson, "Tags")
                .HasColumn(t => t.LastPostJson, "LastPost")
                .HasColumn(t => t.PostsJson, "Posts")
                .HasColumn(t => t.FollowersJson, "Followers")
                .HasColumn(t => t.FollowingsJson, "Followings")
                .HasKey(t => t.TaskId);
        }

        public DataContext CreateNewInstance()
        {
            return new DataContext(_connectionString);
        }
    }

    static class Extensions
    {
        public static EntityTypeBuilder<TEntity> HasColumn<TEntity, TProperty>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, TProperty>> propertyExpression,
            string columnName = null)
        
            where TEntity : class
        {
            if (columnName == null)
            {
                var body = (MemberExpression)propertyExpression.Body;
                columnName = body.Member.Name;
            }
            
            builder.Property(propertyExpression).HasColumnName(columnName);
            return builder;
        }
    }
}
