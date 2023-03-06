using Azure;
using System.Runtime.InteropServices;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Data.Seeders;
using TatBlog.Services.Blogs;
using TatBlog.WinApp;

var context = new BlogDbContext();
//var seeder = new DataSeeder(context);
//seeder.Initialize();

/*var authors = context.Author.ToList();

Console.WriteLine("{0,-4}{1,-30}{2,-30}{3,12}", "ID", "Full Name", "Email", "Joined Date");
foreach (var author in authors)
{
	Console.WriteLine("{0,-4}{1,-30}{2,-30}{3,12:MM/dd/yyyy}", author.Id, author.FullName, author.Email, author.JoinedDate);
}
*/

/*var posts = context.Posts
	.Where(p => p.Published)
	.OrderBy(p => p.Title)
	.Select(p => new
	{
		Id = p.Id,
		Title = p.Title,
		ViewCount = p.ViewCount,
		PostedDate = p.PostedDate,
		Author = p.Author.FullName,
		Category = p.Category.Name,
	}).ToList();
foreach (var post in posts)
{
	Console.WriteLine("ID         : {0}", post.Id);
	Console.WriteLine("Title      : {0}", post.Title);
	Console.WriteLine("View       : {0}", post.ViewCount);
	Console.WriteLine("Date       : {0:MM/dd/yyyy}", post.PostedDate);
	Console.WriteLine("Author     : {0}", post.Author);
	Console.WriteLine("Category   : {0}", post.Category);
	Console.WriteLine("".PadRight(80, '-'));
}*/

IBlogRepository blogRepo = new BlogRepository(context);
/*var posts = await blogRepo.GetPopularArticlesAsync(3);
foreach (var post in posts)
{
	Console.WriteLine("ID         : {0}", post.Id);
	Console.WriteLine("Title      : {0}", post.Title);
	Console.WriteLine("View       : {0}", post.ViewCount);
	Console.WriteLine("Date       : {0:MM/dd/yyyy}", post.PostedDate);
	Console.WriteLine("Author     : {0}", post.Author.FullName);
	Console.WriteLine("Category   : {0}", post.Category.Name);
	Console.WriteLine("".PadRight(80, '-'));
}*/

/*var categories = await blogRepo.GetCategoryAsync();
Console.WriteLine("{0,-5}{1,-50}{2,10}", "ID", "Name", "Count");
foreach (var item in categories)
{
	Console.WriteLine("{0,-5}{1,-50}{2,10}",
		item.Id, item.Name, item.PostCount);
}*/

//Lấy danh sách từ khóa
/*var tagsList = await blogRepo.GetPagedTagsAsync(pagingParams);
Console.WriteLine("{0,-5} {1,-50} {2,10}", "ID", "Name", "Count");
foreach (var item in tagsList)
{
	Console.WriteLine("{0,-5} {1,-50} {2,10}", item.Id, item.Name, item.PostCount);
}*/
//Tìm một thẻ (Tag) theo tên định danh (slug)
/*string slug = "Google";
var getTag = await blogRepo.GetOneTag(slug);
Console.WriteLine("{0,-5} {1,-50} {2,10}", "ID", "Name", "Count");
Console.WriteLine("{0,-5} {1,-50} {2,10}", getTag.Id, getTag.Name, getTag.UrlSlug);*/
//Lấy danh sách tất cả các thẻ (Tag) kèm theo số bài viết chứa thẻ đó. Kết
//quả trả về kiểu IList<TagItem>
/*var tagItems = await blogRepo.GetAllTag();
Console.WriteLine("{0,-5}{1,-50}{2,10}", "ID", "Name", "Count");
foreach (var item in tagItems)
{
	Console.WriteLine("{0,-5}{1,-50}{2,10}",
		item.Id, item.Name, item.PostCount);
}*/
// Xóa một thẻ theo mã cho trước
/*Console.WriteLine("Nhap ma the can xoa: ");
int nhap = Convert.ToInt32(Console.ReadLine());
var tagDelete = blogRepo.DeleteTag(nhap);
var tags = context.Tags
	.OrderBy(x => x.Name)
	.Select(x => new
	{
		Id = x.Id,
		Name = x.Name,
		UrlSlug = x.UrlSlug,
		Description = x.Description,
		PostCount = x.Posts.Count()
	}).ToList();
if (tags.Count > 0)
{
	Console.WriteLine("Danh sach the con lai: ");
	foreach (var tag in tags)
	{
		Console.WriteLine("ID             : {0}", tag.Id);
		Console.WriteLine("Name           : {0}", tag.Name);
		Console.WriteLine("UrlSlug        : {0}", tag.UrlSlug);
		Console.WriteLine("Description    : {0}", tag.Description);
		Console.WriteLine("PostCount      : {0}", tag.PostCount);
		Console.WriteLine("".PadRight(80, '='));
	}
}
else
{
	Console.WriteLine("Khong tim thay the can tim!");
}*/
//Tìm một chuyên mục (Category) theo tên định danh (slug)
/*string slugCategory = "Architecture";
var getCategory = await blogRepo.GetOneCategory(slugCategory);
Console.WriteLine("{0,-5} {1,-50} {2,10}", "ID", "Name", "Description");
Console.WriteLine("{0,-5} {1,-50} {2,10}", getCategory.Id, getCategory.Name, getCategory.Description);*/
//Tìm một chuyên mục theo mã số cho trước
/*Console.WriteLine("Nhap chuyen muc can tim: ");
int nhapCategory = Convert.ToInt32(Console.ReadLine());
var findById = await blogRepo.FindCategoryById(nhapCategory);
Console.WriteLine("{0,-5} {1,-20} {2,-30} {3,-20} {4,-30}", "ID", "Name", "UrlSlug", "Description", "PostCount");
Console.WriteLine("{0,-5} {1,-20} {2,-30} {3,-20} {4,-30}", findById.Id, findById.Name, findById.UrlSlug, findById.Description, findById.PostCount);*/
//Thêm hoặc cập nhật một chuyên mục/chủ đề
/*Console.WriteLine("Nhap thong tin chuyen muc can them: \n");
Console.Write("Name: ");
string cateName = Console.ReadLine().Trim();
Console.Write("UrlSlug: ");
string cateUrlSlug = Console.ReadLine().Trim();
Console.Write("Descriprion: ");
string cateDescription = Console.ReadLine().Trim();
blogRepo.AddCategory(cateName, cateUrlSlug, cateDescription);
var category = context.Category
	.OrderBy(x => x.Id)
	.Select(x => new
	{
		Id = x.Id,
		Name = x.Name,
		UrlSlug = x.UrlSlug,
		Description = x.Description,
		PostCount = x.Posts.Count()
	}).ToList();
Console.WriteLine("Danh sach chuyen muc hien tai: ");
foreach (var item in category)
{
	Console.WriteLine("ID          : {0}", item.Id);
	Console.WriteLine("Name        : {0}", item.Name);
	Console.WriteLine("UrlSlug     : {0}", item.UrlSlug);
	Console.WriteLine("Desciption  : {0}", item.Description);
	Console.WriteLine("PostCount   : {0}", item.PostCount);
	Console.WriteLine("".PadRight(80, '-'));
}*/
//Xóa một chuyên mục theo mã số cho trước
/*Console.WriteLine("Nhap ma chuyen muc can xoa: ");
int nhap = Convert.ToInt32(Console.ReadLine());
blogRepo.DeleteCategory(nhap);
var category = context.Category
	.OrderBy(x => x.Name)
	.Select(x => new
	{
		Id = x.Id,
		Name = x.Name,
		UrlSlug = x.UrlSlug,
		Description = x.Description,
		PostCount = x.Posts.Count()
	}).ToList();
if (category.Count > 0)
{
	Console.WriteLine("Danh sach chuyen muc con lai: ");
	foreach (var item in category)
	{
		Console.WriteLine("ID             : {0}", item.Id);
		Console.WriteLine("Name           : {0}", item.Name);
		Console.WriteLine("UrlSlug        : {0}", item.UrlSlug);
		Console.WriteLine("Description    : {0}", item.Description);
		Console.WriteLine("PostCount      : {0}", item.PostCount);
		Console.WriteLine("".PadRight(80, '='));
	}
}
else
{
	Console.WriteLine("Khong tim thay the can tim!");
}*/
//Kiểm tra tên định danh (slug) của một chuyên mục đã tồn tại hay chưa
/*Console.WriteLine("Nhap ten dinh danh chuyen muc can kiem tra: ");
string slug = Console.ReadLine();
if (await blogRepo.CheckSlugExist(slug))
{
	Console.WriteLine("Ten dinh danh chuyen muc da ton tai.");
}
else
{
	Console.WriteLine("Ten dinh danh chuyen muc chua ton tai.");
}*/
//Lấy và phân trang danh sách chuyên mục, kết quả trả về kiểu
//IPagedList<CategoryItem>
//Tạo đối tượng chứa tham số phân trang
var pagingParams = new PagingParams
{
	PageNumber = 1,
	PageSize = 10,
	SortColumn = "Id",
	SortOrder = "ASC"
};
var catrgoryItem = await blogRepo.GetPagedCategoriesAsync(pagingParams);
Console.WriteLine("{0,-5} {1,-20} {2,-30} {3,-20} {4,-30}", "ID", "Name", "UrlSlug", "Description", "PostCount");
foreach (var item in catrgoryItem)
{
	Console.WriteLine("{0,-5} {1,-20} {2,-30} {3,-20} {4,-30}", item.Id, item.Name, item.UrlSlug, item.Description, item.PostCount);
}
