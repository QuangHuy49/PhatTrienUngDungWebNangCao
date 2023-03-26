using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Controllers
{
    public class BlogController : Controller
    {
        public async Task<IActionResult> Index(
            [FromQuery(Name = "k")] string keyword = null,
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 10)
        {
            //Tạo đối tướng chứa các điều kiện truy vấn
            var postQuery = new PostQuery()
            {
                //Chỉ lấy những bài viết có trạng thái Published
                PublishedOnly = true,

                //Tìm bài viết theo từ khóa
                KeyWord = keyword
            };

            //Truy vấn các bài viết theo điều kiện đã tạo
            var postsList = await _blogRepository
                .GetPagedPostAsync(postQuery, pageNumber, pageSize);
            //Lưu lại điều kiện truy vẫn để hiển thị trong view
            ViewBag.PostQuery = postQuery;
            //Truyền danh sach bài viết vào view để render ra HTML
            return View(postsList);
        }

        public async Task<IActionResult> Category(string slug)
        {
            var postQuey = new PostQuery
            {
                CategorySlug = slug
            };

            var post = await _blogRepository.GetPostByQueryAsync(postQuey);

            return View(post);
        }

        public async Task<IActionResult> Author(string slug)
        {
            var postQuery = new PostQuery
            {
                AuthorSlug = slug
            };

            var post = await _blogRepository.GetPostByQueryAsync(postQuery);

            return View(post);
        }

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
