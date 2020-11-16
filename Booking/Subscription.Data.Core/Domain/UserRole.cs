using Microsoft.AspNetCore.Identity;

namespace Subscription.Core.Domain
{
    public class UserRole : IdentityUserRole<int>
    {
        public virtual Role Role { get; set; }
    }
}
