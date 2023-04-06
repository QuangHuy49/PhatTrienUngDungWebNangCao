using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;

namespace TatBlog.Services.Blogs;

public interface IPostRepository
{
    Task<IPagedList<PostItem>> GetPagedPostsAsync(
        IPagingParams pagingParams,
        string name = null,
        CancellationToken cancellationToken = default);

    Task<IList<PostItem>> GetPopularArticlesAsync(int numPosts,
        CancellationToken cancellationToken = default);

    Task<IList<PostItem>> GetRandomPostAsync(int numPosts,
        CancellationToken cancellationToken = default);

    //

    Task<Post> GetPostByIdAsync(int id, bool includeDetails = false,
        CancellationToken cancellation = default);

    Task<bool> IsPostSlugExistedAsync(
        int postId,
        string slug,
        CancellationToken cancellationToken = default);

    Task<bool> AddOrUpdateAsync(
        Post post, CancellationToken cancellationToken = default);
}
