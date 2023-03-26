using Microsoft.AspNetCore.Mvc;
using TatBlog.Services.Blogs;
using TatBlog.WebApp.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Printing;
using TatBlog.Core.DTO;
using FluentValidation;
using MapsterMapper;
using TatBlog.Services.Media;

namespace TatBlog.WebApp.Areas.Admin.Controllers;

public class CategoriesController : Controller
{
    private readonly ILogger<CategoriesController> _logger;
    private readonly IBlogRepository _blogRepository;
    private readonly IMediaManager _mediaManager;
    private readonly IMapper _mapper;

    public CategoriesController
    (
        ILogger <CategoriesController> logger,
        IBlogRepository blogRepository,
        IMediaManager mediaManager,
        IMapper mapper)
    {
        _logger = logger;
        _blogRepository = blogRepository;
        _mediaManager = mediaManager;
        _mapper = mapper;
    }
    public async IActionResult Index(CategoryFilterModel model, int pageNumber = 1, int pageSize = 10)
    {
        _logger.LogInformation("Tạo điều kiện truy vấn");

        //Sử dụng Mapster để tạo đối tượng PostQuery từ đối tượng PostFilterModel model
        var postQuery = _mapper.Map<Category>(model);

        _logger.LogInformation("Lấy danh sách bài viết từ CSDL");
        ViewBag.PostsList = await _blogRepository
            .GetPagedPostAsync(postQuery, pageNumber, pageSize);

        _logger.LogInformation("Chuẩn bị dữ liệu cho ViewModel");

        await PopulateCategoryFilterModelAsync(model);

        return View(model);
    }

    public async Task PopulateCategoryFilterModelAsync(CategoryFilterModel model)
    {
        var categories = await _blogRepository.GetCategoryAsync();

        model.CategoryList = categories.Select(c => new SelectListItem()
        {
            Text = c.Name,
            Value = c.Id.ToString()
        });
    }
}
