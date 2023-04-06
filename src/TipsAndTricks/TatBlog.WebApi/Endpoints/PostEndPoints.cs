using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TatBlog.Core.Collections;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.WebApi.Filters;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Endpoints;

public static class PostEndPoints
{
    public static WebApplication MapPostEndpoints(
        this WebApplication app)
    {
        var routeGroupBuilder = app.MapGroup("/api/posts");

        routeGroupBuilder.MapGet("/", GetPosts)
            .WithName("GetPosts")
            .Produces<ApiResponse<PaginationResult<PostItem>>>();

        routeGroupBuilder.MapGet("/featured/{numPosts:int}", GetPostsPopularArticles)
            .WithName("GetPostsPopularArticles")
            .Produces<ApiResponse<PostItem>>();

        routeGroupBuilder.MapGet("/random/{numPosts:int}", GetRandomPosts)
            .WithName("GetRandomPosts")
            .Produces<ApiResponse<PostItem>>();

        //

        routeGroupBuilder.MapGet("/{id:int}", GetPostById)
            .WithName("GetPostById")
            .Produces<ApiResponse<PostItem>>();

        routeGroupBuilder.MapGet(
                "/byslug/{slug:regex(^[a-z0-9_-]+$)}",
                GetPostsBySlug)
            .WithName("GetPostsBySlug")
            .Produces<ApiResponse<PaginationResult<PostDto>>>();

        routeGroupBuilder.MapPost("/", AddPost)
            .WithName("AddNewPost")
            .AddEndpointFilter<ValidatorFilter<PostEditModel>>()
            .RequireAuthorization()
            .Produces(401)
            .Produces<ApiResponse<PostItem>>();

        return app;
    }

    private static async Task<IResult> GetPosts(
        [AsParameters] PostFilterModel model,
        IPostRepository postRepository)
    {
        var postList = await postRepository
            .GetPagedPostsAsync(model, model.Name);

        var paginationResult = new PaginationResult<PostItem>(postList);

        return Results.Ok(ApiResponse.Success(paginationResult));
    }

    private static async Task<IResult> GetPostsPopularArticles(
        int numPosts,
        IPostRepository postRepository,
        IMapper mapper)
    {
        var post = await postRepository.GetPopularArticlesAsync(numPosts);
        return Results.Ok(ApiResponse.Success(post));
    }

    private static async Task<IResult> GetRandomPosts(
        int numPosts,
        IPostRepository postRepository,
        IMapper mapper)
    {
        var post = await postRepository.GetRandomPostAsync(numPosts);
        return Results.Ok(ApiResponse.Success(post));
    }

    //

    private static async Task<IResult> GetPostById(
        int id,
        IPostRepository postRepository,
        IMapper mapper)
    {
        var category = await postRepository.GetPostByIdAsync(id);
        return category == null
            ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
            $"Không tìm thấy bài viết có mã số {id}"))
            : Results.Ok(ApiResponse.Success(mapper.Map<CategoryItem>(category)));
    }

    private static async Task<IResult> GetPostsBySlug(
        [FromRoute] string slug,
        [AsParameters] PagingModel pagingModel,
        IBlogRepository blogRepository)
    {
        var postQuery = new PostQuery()
        {
            TitleSlug = slug,
            PublishedOnly = true
        };

        var postsList = await blogRepository.GetPagedPostsAsync(
            postQuery, pagingModel,
            posts => posts.ProjectToType<PostDto>());
        var paginationResult = new PaginationResult<PostDto>(postsList);

        return Results.Ok(ApiResponse.Success(paginationResult));
    }

    private static async Task<IResult> AddPost(
        PostEditModel model,
        IValidator<PostEditModel> validator,
        IPostRepository postRepository,
        IMapper mapper)
    {
        if (await postRepository.IsPostSlugExistedAsync(0, model.UrlSlug))
        {
            return Results.Ok(ApiResponse.Fail(
                HttpStatusCode.Conflict, $"Slug '{model.UrlSlug}' đã được sử dụng!"));
        }

        var post = mapper.Map<Post>(model);
        await postRepository.AddOrUpdateAsync(post);

        return Results.Ok(ApiResponse.Success(
           mapper.Map<PostItem>(post), HttpStatusCode.Created));
    }
}
