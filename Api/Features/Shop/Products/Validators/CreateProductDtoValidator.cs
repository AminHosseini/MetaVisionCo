using Api.Features.Shop.Products.Dtos;

namespace Api.Features.Shop.Products.Validators;

/// <summary>
/// کلاس اعتبار سنجی اطلاعات محصول وارد شده توسط کاربر برای ایجاد
/// </summary>
public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    /// <summary>
    /// سازنده کلاس به همراه قوانین اعتبار سنجی
    /// </summary>
    public CreateProductDtoValidator()
    {
        RuleFor(p => p.ProductCategoryId)
            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
            .NotEqual(0).WithMessage(ValidationMessages.NullMessage);

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
            .MaximumLength(50).WithMessage(ValidationMessages.MaximumCharactersMessage(50));

        //RuleFor(p => p.Code)
        //    .NotEmpty().WithMessage(ValidationMessages.NullMessage)
        //    .MaximumLength(50).WithMessage(ValidationMessages.MaximumCharactersMessage(50));

        RuleFor(p => p.ShortDescription)
            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
            .MaximumLength(300).WithMessage(ValidationMessages.MaximumCharactersMessage(300));

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
            .MaximumLength(1000).WithMessage(ValidationMessages.MaximumCharactersMessage(1000));

        RuleFor(p => p.Seo!.Slug)
            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
            .MaximumLength(200).WithMessage(ValidationMessages.MaximumCharactersMessage(200));

        RuleFor(p => p.Seo!.MetaDescription)
            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
            .MaximumLength(200).WithMessage(ValidationMessages.MaximumCharactersMessage(200));

        RuleFor(p => p.Seo!.Keywords)
            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
            .Must(k => k!.Count() <= 8).WithMessage(ValidationMessages.MaximumListCountMessage(8));

        RuleForEach(p => p.Seo!.Keywords)
            .Must(k => !string.IsNullOrWhiteSpace(k)).WithMessage(ValidationMessages.NullMessage)
            .Must(k => k.Length <= 24).WithMessage(ValidationMessages.MaximumCharactersForListMembersMessage(24));
    }
}
