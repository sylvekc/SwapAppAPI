using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SwapApp.Models.Validators
{
    public class ItemQueryValidator : AbstractValidator<ItemQuery>
    {
        private int[] allowedPageSizes = new[] { 10, 25, 50 };
        public ItemQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", allowedPageSizes)}]");
                }
            });
        }
    }
}
