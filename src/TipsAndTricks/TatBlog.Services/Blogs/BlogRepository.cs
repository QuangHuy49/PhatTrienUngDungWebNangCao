using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Core.DTO;
using TatBlog.Core.Contracts;
using System.ComponentModel;
using TatBlog.Data.Mappings;
using TatBlog.Services.Extensions;
using System.Net.Http.Headers;

namespace TatBlog.Services.Blogs;

public class BlogRepository : IBlogRepository
{
    private readonly BlogDbContext _context;
    public BlogRepository(BlogDbContext context)
    {
        _context = context;
    }
    //Tìm bài viết có tên định danh là 'slug' và được đăng vào month và year
    public async Task<Post> GetPostAsync(int year, int month, string slug,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Post> postsQuery = _context.Set<Post>()
            .Include(x => x.Category)
            .Include(x => x.Author);
        if (year > 0)
        {
            postsQuery = postsQuery.Where(x => x.PostedDate.Year == year);
        }
        if (month > 0)
        {
            postsQuery = postsQuery.Where(x => x.PostedDate.Month == month);
        }
        if (!string.IsNullOrWhiteSpace(slug))
        {
            postsQuery = postsQuery.Where(x => x.UrlSlug == slug);
        }
        return await postsQuery.FirstOrDefaultAsync(cancellationToken);
    }
    //Tìm top n bải viết phổ được nhiều người xem nhất
    public async Task<IList<Post>> GetPopularArticlesAsync(int numPosts,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Post>()
            .Include(x => x.Author)
            .Include(x => x.Category)
            .OrderByDescending(p => p.ViewCount)
            .Take(numPosts)
            .ToListAsync(cancellationToken);
    }
    //Kiểm tra tên định danh của bài viết đã có hay chưa
    public async Task<bool> IsPostSlugExistedAsync(int postId, string slug,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Post>()
            .AnyAsync(x => x.Id != postId && x.UrlSlug == slug, cancellationToken);
    }
    //Tăng số lượt xem của một bài viết
    public async Task IncreaseViewCountAsync(int postId,
        CancellationToken cancellationToken = default)
    {
        await _context.Set<Post>()
            .Where(x => x.Id == postId)
            .ExecuteUpdateAsync(p => p.SetProperty(x => x.ViewCount, x => x.ViewCount + 1),
            cancellationToken);
    }
    //Lấy danh sách chuyên mục và số lượng bài viết nằm thuộc từng chuyên mục
    public async Task<IList<CategoryItem>> GetCategoryAsync(bool showOnMenu = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Category> categories = _context.Set<Category>();
        if (showOnMenu)
        {
            categories = categories.Where(x => x.ShowOnMenu);
        }
        return await categories
            .OrderBy(x => x.Name)
            .Select(x => new CategoryItem()
            {
                Id = x.Id,
                Name = x.Name,
                UrlSlug = x.UrlSlug,
                Description = x.Description,
                ShowOnMenu = x.ShowOnMenu,
                PostCount = x.Posts.Count(p => p.Published)
            }).ToListAsync(cancellationToken);
    }
    //Lấy danh sách từ khóa và phân trang theo các tham số pagingParams
    public async Task<IPagedList<TagItem>> GetPagedTagsAsync(
        IPagingParams pagingParams,
        CancellationToken cancellationToken = default)
    {
        var tagQuery = _context.Set<Tag>()
            .Select(x => new TagItem()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                PostCount = x.Posts.Count(p => p.Published)
            });
        return await tagQuery
            .ToPagedListAsync(pagingParams, cancellationToken);
    }
    //Tìm một thẻ (Tag) theo tên định danh (slug)
    public async Task<Tag> GetOneTag(string slug,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Tag>()
            .Where(x => x.UrlSlug == slug)
            .FirstOrDefaultAsync(cancellationToken);
    }
    //Lấy danh sách tất cả các thẻ (Tag) kèm theo số bài viết chứa thẻ đó. Kết
    //quả trả về kiểu IList<TagItem>
    public async Task<IList<TagItem>> GetAllTag(CancellationToken cancellation = default)
    {
        IQueryable<Tag> tagItems = _context.Set<Tag>();
        return await tagItems
            .OrderBy(x => x.Name)
            .Select(x => new TagItem()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                PostCount = x.Posts.Count(x => x.Published)
            }).ToListAsync(cancellation);
    }
    // Xóa một thẻ theo mã cho trước
    /*public class NotFoundException : Exception
	{
		public NotFoundException(string entityName, int entityId)
			: base($"Entity '{entityName}' with ID '{entityId}' was not found.")
		{
		}
	}*/
    public async Task DeleteTag(int id, CancellationToken cancellationToken = default)
    {
        /*var tag = await _context.Set<Tag>().FindAsync(id);
		if (tag == null)
		{
			throw new NotFoundException(nameof(Tag), id);
		}
		_context.Set<Tag>().Remove(tag);
		await _context.SaveChangesAsync(cancellationToken);*/
        var tagDelete = _context.Set<Tag>().SingleOrDefault(x => x.Id == id);
        if (tagDelete != null)
        {
            _context.Tags.Remove(tagDelete);
            await _context.SaveChangesAsync();
            Console.WriteLine("Xoa the thanh cong!");
        }
        else
        {
            Console.WriteLine("Khong tim thay the can xoa!");
        }

    }
    //Tìm một chuyên mục (Category) theo tên định danh (slug)
    public async Task<Category> GetOneCategory(string slug,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Category>()
            .Where(x => x.UrlSlug == slug)
            .FirstOrDefaultAsync(cancellationToken);
    }
    //Tìm một chuyên mục theo mã số cho trước
    public async Task<CategoryItem> FindCategoryById(int id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Category>()
            .Where(x => x.Id == id)
            .Select(x => new CategoryItem()
            {
                Id = x.Id,
                Name = x.Name,
                UrlSlug = x.UrlSlug,
                Description = x.Description,
                PostCount = x.Posts.Count(x => x.Published)
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
    //Thêm hoặc cập nhật một chuyên mục/chủ đề
    public async Task AddCategory(string name, string urlSlug, string description,
        CancellationToken cancellation = default)
    {
        _context.Categories
            .Add(new Category()
            {
                Name = name,
                UrlSlug = urlSlug,
                Description = description,
                ShowOnMenu = true
            });
        Console.WriteLine("Them chuyen muc thanh cong!\n");
        Console.WriteLine("".PadRight(80, '-'));
        await _context.SaveChangesAsync(cancellation);
    }
    //Xóa một chuyên mục theo mã số cho trước
    public async Task DeleteCategory(int id, CancellationToken cancellation = default)
    {
        var categoryDelete = _context.Set<Category>().SingleOrDefault(x => x.Id == id);
        if (categoryDelete != null)
        {
            _context.Categories.Remove(categoryDelete);
            await _context.SaveChangesAsync();
            Console.WriteLine("Xoa chuyen muc thanh cong!");
        }
        else
        {
            Console.WriteLine("Khong tim thay chuyen muc can xoa!");
        }
    }
    //Kiểm tra tên định danh (slug) của một chuyên mục đã tồn tại hay chưa
    public async Task<bool> CheckSlugExist(string slug,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Category>()
            .AnyAsync(x => x.UrlSlug == slug, cancellationToken);
    }
    //Lấy và phân trang danh sách chuyên mục, kết quả trả về kiểu
    //IPagedList<CategoryItem>
    public async Task<IPagedList<CategoryItem>> GetPagedCategoriesAsync(IPagingParams pagingParams,
        CancellationToken cancellationToken = default)
    {
        var categoryQuery = _context.Set<Category>()
            .Select(x => new CategoryItem()
            {
                Id = x.Id,
                Name = x.Name,
                UrlSlug = x.UrlSlug,
                Description = x.Description,
                ShowOnMenu = true,
                PostCount = x.Posts.Count(p => p.Published)
            });
        return await categoryQuery.ToPagedListAsync(pagingParams, cancellationToken);
    }
    //Đếm số lượng bài viết trong N tháng gần nhất. N là tham số đầu vào. Kết
    //quả là một danh sách các đối tượng chứa các thông tin sau: Năm, Tháng, Số
    //bài viết

    //Chuyển đổi trạng thái Published của bài viết
    public async Task ChangeStatusPost(int id, CancellationToken cancellation = default)
    {
        var post = _context.Set<Post>().Include(x => x.Category)
            .Include(x => x.Author).FirstOrDefault(x => x.Id == id);
        if (post != null)
        {
            Console.WriteLine("ID           : {0}", post.Id);
            Console.WriteLine("Title        : {0}", post.Title);
            Console.WriteLine("Date         : {0:MM/dd/yyyy}", post.PostedDate);
            Console.WriteLine("Author       : {0}", post.Author.FullName);
            Console.WriteLine("Category     : {0}", post.Category.Name);
            Console.WriteLine("Published    : {0}", post.Published);
            Console.WriteLine("".PadRight(80, '-'));

            Console.WriteLine("Ban co muon thay doi trang thai cua bai viet (Yes/No)?");
            var answer = Console.ReadLine().Trim();
            if (string.Equals(answer, "yes", StringComparison.OrdinalIgnoreCase))
            {
                post.Published = !post.Published;
                await _context.SaveChangesAsync();
                Console.WriteLine("Thong tin bai viet sau khi thay doi: ");
                Console.WriteLine("ID           : {0}", post.Id);
                Console.WriteLine("Title        : {0}", post.Title);
                Console.WriteLine("Date         : {0:MM/dd/yyyy}", post.PostedDate);
                Console.WriteLine("Author       : {0}", post.Author.FullName);
                Console.WriteLine("Category     : {0}", post.Category.Name);
                Console.WriteLine("Published    : {0}", post.Published);
                Console.WriteLine("".PadRight(80, '-'));
            }
            else
            {
                Console.WriteLine("Bai viet chua duoc thay doi!");
            }
        }
        else
        {
            Console.WriteLine("Khong tim thay bai viet!");
        }

    }
}