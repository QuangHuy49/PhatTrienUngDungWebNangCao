using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Services.Extensions;

namespace TatBlog.Services.Blogs;

public class PostRepository : IPostRepository
{
    private readonly BlogDbContext _context;
    private readonly IMemoryCache _memoryCache;

    public PostRepository(BlogDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }

    public async Task<IPagedList<PostItem>> GetPagedPostsAsync(
        IPagingParams pagingParams,
        string name = null,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Post>()
            .Include(x => x.Author)
            .Include(x => x.Category)
            .Include(x => x.Tags)
            .AsNoTracking()
            .WhereIf(!string.IsNullOrWhiteSpace(name),
                x => x.Title.Contains(name))
            .Select(a => new PostItem()
            {
                Id = a.Id,
                Title = a.Title,
                UrlSlug = a.UrlSlug,
                ShortDescription = a.ShortDescription,
                Description = a.Description,
                ViewCount= a.ViewCount,
                AuthorID = a.AuthorId,
                AuthorName = a.Author.FullName,
                CategoryId = a.CategoryId,
                CategoryName = a.Category.Name,
                Tags = a.Tags
            })
            .ToPagedListAsync(pagingParams, cancellationToken);
    }

    public async Task<IList<PostItem>> GetPopularArticlesAsync(int numPosts,
        CancellationToken cancellationToken = default)
    {

        return await _context.Set<Post>()
            .Include(x => x.Author)
            .Include(x => x.Category)
            .Include(x => x.Tags)
            .OrderByDescending(p => p.ViewCount)
            .Take(numPosts)
            .AsNoTracking()
            .Select(a => new PostItem()
            {
                Id = a.Id,
                Title = a.Title,
                UrlSlug = a.UrlSlug,
                ShortDescription = a.ShortDescription,
                Description = a.Description,
                ViewCount = a.ViewCount,
                AuthorID = a.AuthorId,
                AuthorName = a.Author.FullName,
                CategoryId = a.CategoryId,
                CategoryName = a.Category.Name,
                Tags = a.Tags
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<IList<PostItem>> GetRandomPostAsync(int numPosts,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Post>()
            .OrderBy(x => Guid.NewGuid())
            .Include(x => x.Author)
            .Include(x => x.Category)
            .Include(x => x.Tags)
            .OrderByDescending(p => p.ViewCount)
            .Take(numPosts)
            .AsNoTracking()
            .Select(a => new PostItem()
            {
                Id = a.Id,
                Title = a.Title,
                UrlSlug = a.UrlSlug,
                ShortDescription = a.ShortDescription,
                Description = a.Description,
                ViewCount = a.ViewCount,
                AuthorID = a.AuthorId,
                AuthorName = a.Author.FullName,
                CategoryId = a.CategoryId,
                CategoryName = a.Category.Name,
                Tags = a.Tags
            })
            .ToListAsync(cancellationToken);
    }

    //

    public async Task<Post> GetPostByIdAsync(int id, bool includeDetails = false,
        CancellationToken cancellation = default)
    {
        return await _context.Set<Post>()
            .Include(x => x.Category)
            .Include(x => x.Author)
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == id, cancellation);
    }

    public async Task<bool> IsPostSlugExistedAsync(
        int postId,
        string slug,
        CancellationToken cancellationToken = default)
    {
        return await _context.Posts
            .AnyAsync(x => x.Id != postId && x.UrlSlug == slug, cancellationToken);
    }

    public async Task<bool> AddOrUpdateAsync(
        Post post, CancellationToken cancellationToken = default)
    {
        if (post.Id > 0)
        {
            _context.Posts.Update(post);
            _memoryCache.Remove($"category.by-id.{post.Id}");
        }
        else
        {
            _context.Posts.Add(post);
        }

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
