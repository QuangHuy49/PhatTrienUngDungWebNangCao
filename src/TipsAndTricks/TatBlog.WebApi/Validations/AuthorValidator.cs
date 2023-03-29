using FluentValidation;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Validations;

public class AuthorValidator : AbstractValidator<AuthorEditModel>
{
    public AuthorValidator() 
    {
        RuleFor(a => a.FullName)
            .NotEmpty()
            .WithMessage("Tên tác giả không được để trống!")
            .MaximumLength(100)
            .WithMessage("Tên tác giả tối đa 100 ký tự!");
        RuleFor(a => a.UrlSlug) 
            .NotEmpty()
            .WithMessage("UrlSlug không được để trống!")
            .MaximumLength(100)
            .WithMessage("UrlSlug tối đa 100 ký tự!");
        RuleFor(a => a.JoinDate)
            .GreaterThan(DateTime.MinValue)
            .WithMessage("Ngày tham gia không hơp lệ!");
        RuleFor(a => a.Email)
            .NotEmpty()
            .WithMessage("Email không được để trống!")
            .MaximumLength(100)
            .WithMessage("Email tối đa 100 ký tự!");
        RuleFor(a => a.Notes)
            .MaximumLength(500)
            .WithMessage("Notes tối đa 50 ký tự!");
    }
}
