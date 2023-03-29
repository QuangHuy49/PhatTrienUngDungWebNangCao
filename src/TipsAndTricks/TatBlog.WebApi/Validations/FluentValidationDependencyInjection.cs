using FluentValidation;
using System.Reflection;

namespace TatBlog.WebApi.Validations;

public static class FluentValidationDependencyInjection
{
    public static WebApplicationBuilder ConfigureFluentValidation(
        this WebApplicationBuilder builder)
    {
        //scan and register all validations in given assembly
        builder.Services.AddValidatorsFromAssembly(
            Assembly.GetExecutingAssembly());

        return builder;
    }
}
