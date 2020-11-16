using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subscription.Core.Domain
{
    public class SubscriptionType : BaseModel   
    {
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateDateTime { get; set; }
        public virtual ICollection<UserSubscriptionType> UserSubscriptions { get; set; }
    }
}
