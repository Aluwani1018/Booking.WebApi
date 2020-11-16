using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Subscription.Core.Domain
{
    public class ShoppingCart : BaseModel
    {
        public int ReferenceId { get; set; }
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), DataMember]
        public DateTime? CreateDateTime { get; set; }
    }
}
