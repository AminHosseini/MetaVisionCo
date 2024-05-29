namespace Api.Features.Shop.ProductCategories.Validators;

/// <summary>
/// کلاس اعتبار سنجی اطلاعات دسته بندی محصول وارد شده توسط کاربر برای ایجاد
/// </summary>
public class CreateProductCategoryDtoValidator : AbstractValidator<CreateProductCategoryDto>
{
    /// <summary>
    /// سازنده کلاس به همراه قوانین اعتبار سنجی
    /// </summary>
    public CreateProductCategoryDtoValidator()
    {
        //RuleFor(pc => pc.ParentId)
        //    .NotEqual(0).WithMessage(ValidationMessages.NullMessage);

        RuleFor(pc => pc.Name)
            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
            .MaximumLength(50).WithMessage(ValidationMessages.MaximumCharactersMessage(50));

        RuleFor(pc => pc.Description)
            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
            .MaximumLength(1000).WithMessage(ValidationMessages.MaximumCharactersMessage(1000));

        RuleFor(pc => pc.Seo!.Slug)
            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
            .MaximumLength(200).WithMessage(ValidationMessages.MaximumCharactersMessage(200));

        RuleFor(pc => pc.Seo!.MetaDescription)
            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
            .MaximumLength(200).WithMessage(ValidationMessages.MaximumCharactersMessage(200));

        RuleFor(pc => pc.Seo!.Keywords)
            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
            .Must(k => k!.Count() <= 8).WithMessage(ValidationMessages.MaximumListCountMessage(8));

        RuleForEach(pc => pc.Seo!.Keywords)
            .Must(k => !string.IsNullOrWhiteSpace(k)).WithMessage(ValidationMessages.NullMessage)
            .Must(k => k.Length <= 24).WithMessage(ValidationMessages.MaximumCharactersForListMembersMessage(24));
    }
}