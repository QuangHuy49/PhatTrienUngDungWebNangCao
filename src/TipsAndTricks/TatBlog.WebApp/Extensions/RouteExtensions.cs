namespace TatBlog.WebApp.Extensions
{
    static class RouteExtensions
    {
        //Định nghĩa route template, route constraint cho các
        //endpoints kết hợp với các action trong các controller.
        public static IEndpointRouteBuilder UseBlogRoutes(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "posts-by-author",
                pattern: "blog/author/{slug}",
                defaults: new { controller = "Blog", action = "author" });
            endpoints.MapControllerRoute(
                name: "posts-by-category",
                pattern: "blog/category/{slug}",
                defaults: new { controller = "Blog", action = "Category" });
            endpoints.MapControllerRoute(
                name: "posts-by-tag",
                pattern: "blog/tag/{slug}",
                defaults: new { controller = "Blog", action = "Tag" });
            endpoints.MapControllerRoute(
                name: "posts-by-post",
                pattern: "blog/post/{year:int}/{month:int}/{day:int}/{slug}",
                defaults: new { controller = "Blog", action = "Post" });
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Blog}/{action=Index}/{id?}");

            return endpoints;
        }
    }
}
