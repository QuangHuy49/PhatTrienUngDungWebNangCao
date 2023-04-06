﻿using Microsoft.EntityFrameworkCore;
using NLog.Web;
using TatBlog.Data.Contexts;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.Services.Timing;
using TatBlog.WebApi.Middlewares;

namespace TatBlog.WebApi.Extensions
{
    static class WebApplicationExtensions
    {

        public static WebApplication UseRequestPipeline(this WebApplication app)
        {
            //Thêm middleware lựa chọn endpoint phù hợp nhất 
            //để xử lý một HTTP request.
            app.UseRouting();

            //Thêm middleware để lưu viết người dùng
            app.UseMiddleware<UserActivityMiddleware>();

            return app;
        }

        public static WebApplicationBuilder ConfigureServices(
            this WebApplicationBuilder builder)
        {
            builder.Services.AddMemoryCache();

            builder.Services.AddDbContext<BlogDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ITimeProvider, LocalTimeProvider>();
            builder.Services.AddScoped<IMediaManager, LocalFileSystemMediaManager>();
            builder.Services.AddScoped<IBlogRepository, BlogRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();

            return builder;
        }

        public static WebApplicationBuilder ConfigureCors(
            this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("TatBlogApp", policyBuilder =>
                    policyBuilder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            return builder;
        }

        //Cấu hình việc sử dụng NLog
        public static WebApplicationBuilder ConfigureNLog(
            this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            return builder;
        }

        public static WebApplicationBuilder ConfigureSwaggerOpenApi(
            this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return builder;
        }

        public static WebApplication SetupRequestPipeLine(
            this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseCors("TatBlogApp");

            return app;
        }
    }
}
