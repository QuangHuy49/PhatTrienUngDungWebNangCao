using Microsoft.AspNetCore.Mvc;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Components;

public class TagsWidget : ViewComponent
{
    private readonly IBlogRepository _blogRepository;
    
    public TagsWidget(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }    

    public async Task<IViewComponentResult> InvokeAsync()
    {
        //Lấy danh sách các thẻ
        var tags = await _blogRepository.GetAllTag();

        return View(tags);
    }
}
