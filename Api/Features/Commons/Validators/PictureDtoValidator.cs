//namespace Api.Features.Commons.Validators;

//public class PictureDtoValidator : AbstractValidator<PictureDto>
//{
//    public PictureDtoValidator()
//    {
//        RuleFor(p => p.PicturePath)
//            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
//            .MaximumLength(500).WithMessage(ValidationMessages.MaximumCharactersMessage(500));

//        RuleFor(p => p.PictureAlt)
//            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
//            .MaximumLength(200).WithMessage(ValidationMessages.MaximumCharactersMessage(200));

//        RuleFor(p => p.PictureTitle)
//            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
//            .MaximumLength(200).WithMessage(ValidationMessages.MaximumCharactersMessage(200));
//    }
//}
