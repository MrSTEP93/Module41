﻿using FinalBlog.Configurations;
using FinalBlog.DATA.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalBlog.DATA
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<BlogUser> BlogUsers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ArticleConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new TagConfiguration());
        }
    }
}
