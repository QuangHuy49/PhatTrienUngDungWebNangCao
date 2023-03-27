using Microsoft.AspNetCore.Mvc;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Components;

public class RandomPosts : ViewComponent
{
    private readonly IBlogRepository _blogRepository;
    public RandomPosts(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        //Lấy danh sách top 5 bài viết ngẫu nhiên
        var categories = await _blogRepository.GetRandomPostAsync(5);
        return View(categories);
    }
}
