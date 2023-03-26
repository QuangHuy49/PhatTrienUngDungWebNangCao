using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Data.Seeders;

namespace TatBlog.Data.Seeders;

public class DataSeeder : IDataSeeder
{
	private readonly BlogDbContext _dbContext;
	public DataSeeder(BlogDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	public void Initialize()
	{
		_dbContext.Database.EnsureCreated();
		if (_dbContext.Posts.Any()) return;
		var authors = AddAuthors();
		var categories = AddCategories();
		var tags = AddTags();
		var posts = AddPosts(authors, categories, tags);
	}
	private IList<Author> AddAuthors() {
		var authors = new List<Author>()
		{
			new()
			{
				FullName = "Jason Mouth",
				UrlSlug = "json-mouth",
				Email = "json@gmail.com",
				JoinedDate = new DateTime(2022, 10,22)
			},
			new()
			{
				FullName="Jessica Wonder",
				UrlSlug = "jessica-wonder",
				Email = "jessica665@motip.com",
				JoinedDate = new DateTime(2020, 4, 19)
			},
            new()
            {
                FullName="Thomas",
                UrlSlug = "thomas",
                Email = "thomas@motip.com",
                JoinedDate = new DateTime(2019, 3, 2)
            },
            new()
            {
                FullName="Marry",
                UrlSlug = "marry",
                Email = "marry@motip.com",
                JoinedDate = new DateTime(2018, 10, 16)
            },
            new()
            {
                FullName="Linda",
                UrlSlug = "linda",
                Email = "linda@motip.com",
                JoinedDate = new DateTime(1999, 10, 10)
            }
        };
		_dbContext.Authors.AddRange(authors);
		_dbContext.SaveChanges();
		return authors;
	}
	private IList<Category> AddCategories() {
		var categories = new List<Category>()
		{
			new() {Name = ".NET Core", Description = ".NET Core", UrlSlug = ".NET core", ShowOnMenu = true},
			new() {Name = "Architecture", Description = "Architecture", UrlSlug = "Architecture", ShowOnMenu = true},
			new() {Name = "Messaging", Description = "Messaging", UrlSlug = "Messaging", ShowOnMenu = true},
			new() {Name = "Design Patterns", Description = "Design Patterns", UrlSlug = "Design Patterns", ShowOnMenu = true},
            new() {Name = "Blazor", Description = "Blazor", UrlSlug = "Blazor", ShowOnMenu = true},
            new() {Name = "Python", Description = "Python", UrlSlug = "Python", ShowOnMenu = true}
        };
		_dbContext.AddRange(categories);
		_dbContext.SaveChanges();
		return categories;
	}
	private IList<Tag> AddTags() 
	{
		var tags = new List<Tag>()
		{
			new() {Name = "Google", Description = "Google", UrlSlug = "Google"},
			new() {Name = "ASP .NET MVC", Description = "ASP .NET MVC", UrlSlug = "ASP .NET MVC"},
			new() {Name = "Razor Page", Description = "Razor Page", UrlSlug = "Razor Page"},
			new() {Name = "Blazor", Description = "Blazor", UrlSlug = "Blazor"},
			new() {Name = "Deep Learning", Description = "Deep Learning", UrlSlug = "Deep Learning"},
            new() {Name = "Design Pattern", Description = "Design Pattern", UrlSlug = "Design Pattern"},
			new() {Name = "Architecture", Description = "Architecture", UrlSlug = "Architecture"},
            new() {Name = "Neural Network", Description = "Neural Network", UrlSlug = "Neural Network"},
            new() {Name = "Python", Description = "Python", UrlSlug = "Python"},
        };
		_dbContext.AddRange(tags);
		_dbContext.SaveChanges();
		return tags;
	}
	private IList<Post> AddPosts(
		IList<Author> authors,
		IList<Category> category,
		IList<Tag> tags) 
	{
		var posts = new List<Post>()
		{
			new()
			{
				Title = "ASP.NET Core Diagnostic Scenarios",
				ShortDescription = "David and friends has a great repos...",
				Description = "Here's a few great DON'T and DO examples...",
				Meta = "David and friends has a great repository filled...",
				UrlSlug = "aspnet-core-diagnostic-scenarios",
				Published = true,
				PostedDate = new DateTime(2021, 9, 30, 10, 20, 0),
				ModifiedDate = null,
				ViewCount = 10,
				Author = authors[0],
				Category = category[0],
				Tags = new List<Tag>()
				{
					tags[0]
				}
			},
            new()
            {
                Title = "Introduction to Design Patterns",
                ShortDescription = "Design patterns are a fundamental part of software development.",
                Description = "Design patterns are a fundamental part of software development, " +
				"as they provide typical solutions to commonly recurring problems in software design. " +
				"Rather than providing specific pieces of software, design patterns are merely concepts that can be used to handle recurring themes in an optimized way.",
                Meta = "David and friends has a great repository filled...",
                UrlSlug = "design-pattern",
                Published = true,
                PostedDate = new DateTime(2023, 3, 9, 10, 20, 0),
                ModifiedDate = null,
                ViewCount = 10,
                Author = authors[1],
                Category = category[3],
                Tags = new List<Tag>()
                {
                    tags[0], tags[5]
                }
            },
            new()
            {
                Title = "Introduction to Architecture",
                ShortDescription = "Architecture is the art and technique of designing and building.",
                Description = "Architecture is the art and technique of designing and building, " +
				"as distinguished from the skills associated with construction. It is both the process and the product of " +
				"sketching, conceiving, planning, designing, and constructing buildings or other structures.",
                Meta = "David and friends has a great repository filled...",
                UrlSlug = "Architecture",
                Published = true,
                PostedDate = new DateTime(2023, 3, 10, 10, 20, 0),
                ModifiedDate = null,
                ViewCount = 10,
                Author = authors[1],
                Category = category[1],
                Tags = new List<Tag>()
                {
                    tags[0], tags[6]
                }
            },
            new()
            {
                Title = "Introduction to Blazor",
                ShortDescription = "Build beautiful, web apps with Blazor.",
                Description = "Blazor is a hot topic amongst the .NET Technical Community, " +
				"but what is it and why should I be interested in it? Let's learn what Blazor " +
				"is and how you can use it to make your web applications.",
                Meta = "David and friends has a great repository filled...",
                UrlSlug = "Blazor",
                Published = true,
                PostedDate = new DateTime(2023, 3, 20, 10, 20, 0),
                ModifiedDate = null,
                ViewCount = 10,
                Author = authors[1],
                Category = category[4],
                Tags = new List<Tag>()
                {
                    tags[0], tags[3]
                }
            },
            new()
            {
                Title = "Introduction to Blazor",
                ShortDescription = "Build beautiful, web apps with Blazor.",
                Description = "Blazor is a hot topic amongst the .NET Technical Community, " +
                "but what is it and why should I be interested in it? Let's learn what Blazor " +
                "is and how you can use it to make your web applications.",
                Meta = "David and friends has a great repository filled...",
                UrlSlug = "Blazor",
                Published = true,
                PostedDate = new DateTime(2023, 4, 23, 20, 10, 1),
                ModifiedDate = null,
                ViewCount = 20,
                Author = authors[2],
                Category = category[4],
                Tags = new List<Tag>()
                {
                    tags[0], tags[3]
                }
            },
            new()
            {
                Title = "Introduction to Python",
                ShortDescription = "Build beautiful, web apps with Python.",
                Description = "Python is a hot topic amongst the .NET Technical Community, " +
                "but what is it and why should I be interested in it? Let's learn what Python " +
                "is and how you can use it to make your web applications.",
                Meta = "David and friends has a great repository filled...",
                UrlSlug = "Python",
                Published = true,
                PostedDate = new DateTime(2023, 5, 20, 19, 20, 1),
                ModifiedDate = null,
                ViewCount = 20,
                Author = authors[3],
                Category = category[5],
                Tags = new List<Tag>()
                {
                    tags[8]
                }
            },
            new()
            {
                Title = "Introduction to Python",
                ShortDescription = "Build beautiful, web apps with Python.",
                Description = "Python is a hot topic amongst the .NET Technical Community, " +
                "but what is it and why should I be interested in it? Let's learn what Python " +
                "is and how you can use it to make your web applications.",
                Meta = "David and friends has a great repository filled...",
                UrlSlug = "Python",
                Published = true,
                PostedDate = new DateTime(2023, 5, 25, 18, 50, 10),
                ModifiedDate = null,
                ViewCount = 20,
                Author = authors[3],
                Category = category[5],
                Tags = new List<Tag>()
                {
                    tags[8]
                }
            },
            new()
            {
                Title = "Introduction to Python",
                ShortDescription = "Build beautiful, web apps with Python.",
                Description = "Python is a hot topic amongst the .NET Technical Community, " +
                "but what is it and why should I be interested in it? Let's learn what Python " +
                "is and how you can use it to make your web applications.",
                Meta = "David and friends has a great repository filled...",
                UrlSlug = "Python",
                Published = true,
                PostedDate = new DateTime(2023, 6, 30, 6, 10, 10),
                ModifiedDate = null,
                ViewCount = 20,
                Author = authors[3],
                Category = category[5],
                Tags = new List<Tag>()
                {
                    tags[8]
                }
            },
             new()
            {
                Title = "Introduction to Python",
                ShortDescription = "Build beautiful, web apps with Python.",
                Description = "Python is a hot topic amongst the .NET Technical Community, " +
                "but what is it and why should I be interested in it? Let's learn what Python " +
                "is and how you can use it to make your web applications.",
                Meta = "David and friends has a great repository filled...",
                UrlSlug = "Python",
                Published = true,
                PostedDate = new DateTime(2023, 6, 20, 7, 10, 10),
                ModifiedDate = null,
                ViewCount = 20,
                Author = authors[3],
                Category = category[5],
                Tags = new List<Tag>()
                {
                    tags[8]
                }
            },
              new()
            {
                Title = "Introduction to Python",
                ShortDescription = "Build beautiful, web apps with Python.",
                Description = "Python is a hot topic amongst the .NET Technical Community, " +
                "but what is it and why should I be interested in it? Let's learn what Python " +
                "is and how you can use it to make your web applications.",
                Meta = "David and friends has a great repository filled...",
                UrlSlug = "Python",
                Published = true,
                PostedDate = new DateTime(2023, 6, 30, 6, 10, 10),
                ModifiedDate = null,
                ViewCount = 20,
                Author = authors[3],
                Category = category[5],
                Tags = new List<Tag>()
                {
                    tags[8]
                }
            },
               new()
            {
                Title = "Introduction to Python",
                ShortDescription = "Build beautiful, web apps with Python.",
                Description = "Python is a hot topic amongst the .NET Technical Community, " +
                "but what is it and why should I be interested in it? Let's learn what Python " +
                "is and how you can use it to make your web applications.",
                Meta = "David and friends has a great repository filled...",
                UrlSlug = "Python",
                Published = true,
                PostedDate = new DateTime(2023, 6, 30, 6, 10, 10),
                ModifiedDate = null,
                ViewCount = 20,
                Author = authors[3],
                Category = category[5],
                Tags = new List<Tag>()
                {
                    tags[8]
                }
            },
                new()
            {
                Title = "Introduction to Python",
                ShortDescription = "Build beautiful, web apps with Python.",
                Description = "Python is a hot topic amongst the .NET Technical Community, " +
                "but what is it and why should I be interested in it? Let's learn what Python " +
                "is and how you can use it to make your web applications.",
                Meta = "David and friends has a great repository filled...",
                UrlSlug = "Python",
                Published = true,
                PostedDate = new DateTime(2023, 6, 30, 6, 10, 10),
                ModifiedDate = null,
                ViewCount = 20,
                Author = authors[3],
                Category = category[5],
                Tags = new List<Tag>()
                {
                    tags[8]
                }
            }
        };
		_dbContext.AddRange(posts); 
		_dbContext.SaveChanges();
		return posts;
	}
}
