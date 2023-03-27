using Microsoft.AspNetCore.Mvc;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Components;

public class Archives : ViewComponent
{
    private readonly IBlogRepository _blogRepository;

    public Archives(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var now = DateTime.UtcNow;
        var twelveMonthsAgo = now.AddMonths(-11); // Lấy danh sách 12 tháng gần nhất

        var monthlyCounts = await _blogRepository.GetMonthlyPostCountsAsync(twelveMonthsAgo, now);

        var months = Enumerable.Range(0, 12)
            .Select(offset => now.AddMonths(-offset))
            .Select(date => new {
                Date = date,
                Text = date.ToString("MMMM yyyy"), // Định dạng: November 2022
                Count = monthlyCounts.TryGetValue((short)date.Month, out var count) ? count : 0
            })
            .ToList();

        return View(months);
    }
}
