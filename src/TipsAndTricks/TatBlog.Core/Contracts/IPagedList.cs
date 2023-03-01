using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatBlog.Core.Contracts;

public interface IPagedList
{
	//Tổng số trang (Số tập con)
	int PagedCount { get; }
	//Tổng số phần từ trả về từ truy vấn
	int TotalItemCount { get; }
	//Chỉ số trang hiện tại start 0
	int PageIndex { get; }
	//Vị trí của trang hiện tại start 1
	int PageNumber { get; }
	//Số lượng phần từ tối đa trên một trang
	int PageSize { get; }
	//Kiểm tra có trang trước hay không
	bool HasPreviousPage { get; }
	//Kiểm tra có trang tiếp theo hay không
	bool HasNextPage { get; }
	//Trang hiện tại có phải trang đầu tiên không
	bool IsFirstPage { get; }
	//Trang hiện tại có phải trang cuối cùng không
	bool IsLastPage { get; }
	//Thứ tự của phần tử đầu trang trong truy vấn (start 1)
	int FirstItemIndex { get; }
	//Thứ tự của phần tử cuối trang trong truy vấn (start 1)
	int LastItemIndex { get; }
}

public interface IPagedList<out T> : IPagedList, IEnumerable<T>
{
	//Lấy phần tử tại vị trí index (start 0)
	T this[int index] { get; }
	//Đếm số lượng phần tử chứa trong trang
	int Count { get; }
}
