using Microsoft.AspNetCore.Mvc;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Components;

public class FeaturedPosts : ViewComponent
{
    private readonly IBlogRepository _blogRepository;
    public FeaturedPosts(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        //Lấy danh sách top 3 bài viết được xem nhiều nhất
        var categories = await _blogRepository.GetPopularArticlesAsync(3);
        return View(categories);
    }
}
