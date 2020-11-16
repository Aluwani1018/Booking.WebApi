using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Subscription.Core.Domain
{
    public class Role : IdentityRole<int>
    {
        public virtual ICollection<UserRole> Users { get; set; }
        public virtual ICollection<IdentityRoleClaim<int>> Claims { get; set; }
    }
}
