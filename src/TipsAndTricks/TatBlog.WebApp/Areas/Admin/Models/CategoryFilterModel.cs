using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace TatBlog.WebApp.Areas.Admin.Models;

public class CategoryFilterModel
{
    [DisplayName("Mã số")]
    public int Id { get; set; }

    [DisplayName("Tên chủ đề")]
    public string Name { get; set; }

    [DisplayName("Mô tả")]
    public string Description { get; set; }

    public IEnumerable<SelectListItem> CategoryList { get; set; }
}
