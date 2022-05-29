using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SwapApp.Models.Validators
{
    public class UpdateItemDtoValidator : AbstractValidator<UpdateItemDto>
    {
        public UpdateItemDtoValidator()
        {
            RuleFor(x => x.City).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.District).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Street).NotEmpty().MaximumLength(50);
            RuleFor(x => x.ForFree).NotEmpty();
            RuleFor(x => x.SwapFor).MaximumLength(100);
        }
    }
}
