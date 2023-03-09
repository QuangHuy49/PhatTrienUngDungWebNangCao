using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TatBlog.Core.Entities;
using TatBlog.Data.Mappings;

namespace TatBlog.Data.Contexts;

public class BlogDbContext : DbContext
{
	public DbSet<Author> Authors { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Tag> Tags { get; set; }
	public DbSet<Post> Posts { get; set; }
	protected override void OnConfiguring(
		DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@"Server=QUANGHUY\MSSQLSERVER02;Database=TatBLog;
			Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False");
	}

	public BlogDbContext()
	{

	}

	public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
	{

	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(
			typeof(CategoryMap).Assembly);
	}
}
