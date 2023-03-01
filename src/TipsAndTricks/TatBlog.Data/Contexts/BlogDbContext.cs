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
	public DbSet<Author> Author { get; set; }
	public DbSet<Category> Category { get; set; }
	public DbSet<Tag> Tags { get; set; }
	public DbSet<Post> Posts { get; set; }
	protected override void OnConfiguring(
		DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@"Server=QUANGHUY\MSSQLSERVER02;Database=TatBLog;
			Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False") ;
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(
			typeof(CategoryMap).Assembly);
	}
}
