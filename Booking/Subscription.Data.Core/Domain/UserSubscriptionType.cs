using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Subscription.Core.Domain
{
    public class UserSubscriptionType
    {
        public int UserId { get; set; }
        public int SubscriptionTypeId { get; set; }
        public bool IsActive { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), DataMember]
        public DateTime? CreateDateTime { get; set; } = DateTime.Now;
        public virtual User User { get; set; }
        public virtual SubscriptionType SubscriptionType { get; set; }
    }
}
