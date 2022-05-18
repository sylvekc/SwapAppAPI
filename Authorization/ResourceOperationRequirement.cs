using Microsoft.AspNetCore.Authorization;

namespace SwapApp.Authorization
{
    public enum ResourceOperation
    {
        Create,
        Read,
        Update,
        Delete,
        ExtendValidity,
        ChangeVisibility
    }
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperationRequirement(ResourceOperation resourceOperation)
        {
            ResourceOperation = resourceOperation;

        }
        public ResourceOperation ResourceOperation { get; }
    }
}
