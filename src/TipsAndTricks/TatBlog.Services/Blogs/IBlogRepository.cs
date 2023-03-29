using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Entities;
using TatBlog.Core.DTO;
using TatBlog.Core.Contracts;
using Microsoft.EntityFrameworkCore;

namespace TatBlog.Services.Blogs;

public interface IBlogRepository
{
    //Tìm bài viết có tên định danh là 'slug' và được đăng vào tháng 'month' năm 'year'
    Task<Post> GetPostAsync(int year, int month, int day, string slug,
        CancellationToken cancellationToken = default);
    //Tìm top N bài viết được nhiều người xem nhất
    Task<IList<Post>> GetPopularArticlesAsync(int numPosts,
        CancellationToken cancellationToken = default);
    //Lấy ngẫu nhiên n bài viết 
    Task<IList<Post>> GetRandomPostAsync(int numPosts,
        CancellationToken cancellationToken = default);
    //Kiểm tra tên định danh của bài viết đã có hay chưa
    Task<bool> IsPostSlugExistedAsync(int postId, string slug,
        CancellationToken cancellationToken = default);
    //Tăng số lượt xem của một bài viết
    Task IncreaseViewCountAsync(int postId,
        CancellationToken cancellationToken = default);
    //Lấy danh sách chuyên mục và số lượng bài viết thuộc từng chuyên mục
    Task<IList<CategoryItem>> GetCategoryAsync(bool showOnMenu = false,
        CancellationToken cancellationToken = default);
    Task<IPagedList<TagItem>> GetPagedTagsAsync(
        IPagingParams pagingParams, CancellationToken cancellationToken = default);
    //Tìm một thẻ (Tag) theo tên định danh (slug)
    Task<Tag> GetTagAsync(string slug, CancellationToken cancellationToken = default);
    //Lấy danh sách tất cả các thẻ (Tag) kèm theo số bài viết chứa thẻ đó. Kết
    //quả trả về kiểu IList<TagItem>
    Task<IList<TagItem>> GetAllTag(CancellationToken cancellation = default);
    // Xóa một thẻ theo mã cho trước
    Task DeleteTag(int id, CancellationToken cancellationToken = default);
    //Tìm một chuyên mục (Category) theo tên định danh (slug)
    Task<Category> GetOneCategory(string slug, CancellationToken cancellationToken = default);
    //Tìm một chuyên mục theo mã số cho trước
    Task<CategoryItem> FindCategoryById(int id, CancellationToken cancellationToken = default);
    //Thêm hoặc cập nhật một chuyên mục/chủ đề
    /*Task AddCategory(string name, string urlSlug, string description,
        CancellationToken cancellation = default);*/
    //Xóa một chuyên mục theo mã số cho trước
    Task DeleteCategory(int id, CancellationToken cancellation = default);
    //Kiểm tra tên định danh (slug) của một chuyên mục đã tồn tại hay chưa
    Task<bool> CheckSlugExist(string slug,
        CancellationToken cancellationToken = default);
    //Lấy và phân trang danh sách chuyên mục, kết quả trả về kiểu
    //IPagedList<CategoryItem>
    Task<IPagedList<CategoryItem>> GetPagedCategoriesAsync(IPagingParams pagingParams,
        CancellationToken cancellationToken = default);
    //Đếm số lượng bài viết trong N tháng gần nhất. N là tham số đầu vào. Kết
    //quả là một danh sách các đối tượng chứa các thông tin sau: Năm, Tháng, Số
    //bài viết

    //Tìm một bài viết theo mã số
    Task<Post> GetPostByIdAsync(int id, bool includeDetails = false,
        CancellationToken cancellation = default);
    //Tìm bải viết theo điều kiện truy vấn
    Task<IPagedList<Post>> GetPostByQueryAsync(PostQuery query, int pageNumber = 1,
        int pageSize = 10, CancellationToken cancellation = default);
    //Thêm hay cập nhật một bài viết
    Task<Post> CreateOrUpdatePostAsync(
        Post post, IEnumerable<string> tags,
        CancellationToken cancellationToken = default);
    Task DeletePost(int id, CancellationToken cancellation = default);
    //Chuyển đổi trạng thái Published của bài viết
    Task ChangeStatusPost(int id, CancellationToken cancellation = default);
    //Lấy ngẫu nhiên N bài viết. N là tham số đầu vào

    //Đếm số lượng bài viết thỏa mãn điều kiện tìm kiếm được cho trong đối
    //tượng PostQuery
    Task<int> CountPostsOfPostQueryAsync(PostQuery query, IPagingParams pagingParams,
        CancellationToken cancellationToken = default);

    //Tìm và phân trang các bài viết thỏa mãn điều kiện tìm kiếm được cho trong
    //đối tượng PostQuery(kết quả trả về kiểu IPagedList<Post>)
    Task<IList<Post>> FindPostByPostQueryAsync(PostQuery postQuery, IPagingParams pagingParams,
        CancellationToken cancellationToken = default);

    Task<IPagedList<Post>> GetPagedPostAsync(
        PostQuery condition,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);

    Task<IPagedList<T>> GetPagedPostsAsync<T>(
        PostQuery condition,
        IPagingParams pagingParams,
        Func<IQueryable<Post>, IQueryable<T>> mapper);

    //Lấy danh sách tác giả
    /*Task<IList<AuthorItem>> GetAuthorsAsync(CancellationToken cancellationToken = default);*/

    //Lấy top 4 tác giả có nhiều bài viết nhất
    /*Task<IList<Author>> GetAuthorManyPostAsync(int numAuthors,
        CancellationToken cancellationToken = default);*/

    /////////////////////////////////////////////////////////////////////////////////////////
    Task<IList<TagItem>> GetTagsAsync(CancellationToken cancellationToken = default);

    Task<Post> GetPostBySlugAsync(
      string slug, bool published = false,
      CancellationToken cancellationToken = default);

    Task<Dictionary<short, int>> GetMonthlyPostCountsAsync(DateTime startDate, DateTime endDate);
}
