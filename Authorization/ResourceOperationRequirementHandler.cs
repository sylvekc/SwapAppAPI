using Microsoft.AspNetCore.Authorization;
using SwapApp.Entities;
using System.Security.Claims;

namespace SwapApp.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Item>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Item item)
        {
            if(requirement.ResourceOperation == ResourceOperation.Read ||
                requirement.ResourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }
            var userId = context.User.FindFirst(e => e.Type == ClaimTypes.NameIdentifier).Value;

            if (item.UserId == int.Parse(userId) || context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
