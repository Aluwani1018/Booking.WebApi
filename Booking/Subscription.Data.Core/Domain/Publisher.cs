using System;
using System.Collections.Generic;
using System.Text;

namespace Subscription.Core.Domain
{
    public class Publisher : BaseModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
