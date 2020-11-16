using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subscription.Core.Domain
{
    public class BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
