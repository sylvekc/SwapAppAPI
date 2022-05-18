using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SwapApp.Entities;

namespace SwapApp.Models.Validators
{
    public class ItemQueryValidator : AbstractValidator<ItemQuery>
    {
        private int[] allowedPageSizes = new[] { 10, 25, 50 };
        private string[] allowedSortByColumnNames = {nameof(Item.Name), nameof(Item.CreatedAt)};
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

            RuleFor(r => r.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}");
        }
    }
}
