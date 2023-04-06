using FluentValidation;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Validations
{
    public class PostValidator : AbstractValidator<PostEditModel>
    {
        public PostValidator() 
        {
            RuleFor(a => a.Title)
                .NotEmpty()
                .WithMessage("Title không được để trống!")
                .MaximumLength(100)
                .WithMessage("Title tối đa 100 ký tự!");
            RuleFor(a => a.UrlSlug)
                .NotEmpty()
                .WithMessage("UrlSlug không được để trống!")
                .MaximumLength(100)
                .WithMessage("UrlSlug tối đa 100 ký tự!");
            RuleFor(a => a.ShortDescription)
                .MaximumLength(100)
                .WithMessage("Mô tả ngắn tối đa 50 ký tự!");
            RuleFor(a => a.Description)
                .MaximumLength(500)
                .WithMessage("Mô tả tối đa 50 ký tự!"); ;
        }
    }
}
