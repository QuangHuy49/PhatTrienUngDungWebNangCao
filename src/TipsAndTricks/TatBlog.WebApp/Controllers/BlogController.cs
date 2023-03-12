using Microsoft.AspNetCore.Mvc;
using TatBlog.Core.DTO;
using TatBlog.Data.Contexts;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Controllers
{
    public class BlogController : Controller
    {
        public async Task<IActionResult> Index(
            [FromQuery(Name = "k")] string keyword = null,
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 2)
        {
            //Tạo đối tướng chứa các điều kiện truy vấn
            var postQuery = new PostQuery()
            {
                //Chỉ lấy những bài viết có trạng thái Published
                PublishedOnly = true,

                //Tìm bài viết theo từ khóa
                KeyWord = keyword,
            };

            //Truy vấn các bài viết theo điều kiện đã tạo
            var postsList = await _blogRepository
                .GetPagedPostAsync(postQuery, pageNumber, pageSize);
            //Lưu lại điều kiện truy vẫn để hiển thị trong view
            ViewBag.PostQuery = postQuery;    
            //Truyền danh sach bài viết vào view để render ra HTML
            return View(postsList);
        }
        /*public async Task<IActionResult> Category(string slug, int pageNumber = 1, int pageSize = 10)
        {
            var postQuery = new PostQuery()
            {
                CategorySlug = slug
            };
            //Lấy danh sách bài viết thuộc chủ đề có slug tương ứng
            var postsList = await _blogRepository.GetPostsByCategorySlugAsync(slug, pageNumber, pageSize);

            //Nếu không có bài viết thuộc chủ đề này, trả về trang 404
            if (postsList == null || !postsList.Any())
            {
                return NotFound();
            }

            //Lấy thông tin chuyên mục
            var category = await _blogRepository.GetCategoryBySlugAsync(slug);

            //Truyền thông tin chuyên mục vào ViewBag để sử dụng trong view
            ViewBag.Category = category;

            //Truyền các thông tin phân trang vào ViewBag để sử dụng trong view
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = postsList.TotalItemCount;
            ViewBag.TotalPages = postsList.PagedCount;

            return View(postsList);
        }*/
        public IActionResult About() => View();
        public IActionResult Contact() => View();
        public IActionResult Rss() => Content("Nội dung sẽ được cập nhật");

        private readonly IBlogRepository _blogRepository;
        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
    }
}
