namespace TatBlog.WebApi.Models;

public class CategoryEditModel
{
    public string Name { get; set; }
    public string UrlSlug { get; set; }
    public string Desciption { get; set; }
    public bool ShowOnMenu { get; set; }
}
