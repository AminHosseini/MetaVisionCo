namespace Api.Features.Pictures.Validators;

/// <summary>
/// کلاس اعتبار سنجی اطلاعات عکس وارد شده توسط کاربر برای ویرایش
/// </summary>
public class UpdatePictureDtoValidator : AbstractValidator<UpdatePictureDto>
{
    /// <summary>
    /// سازنده کلاس به همراه قوانین اعتبار سنجی
    /// </summary>
    public UpdatePictureDtoValidator()
    {
        RuleFor(p => p.PictureAlt)
            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
            .MaximumLength(200).WithMessage(ValidationMessages.MaximumCharactersMessage(200));

        RuleFor(p => p.PictureTitle)
            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
            .MaximumLength(200).WithMessage(ValidationMessages.MaximumCharactersMessage(200));
    }
}
