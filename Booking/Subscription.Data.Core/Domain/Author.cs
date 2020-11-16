
using System.Collections.Generic;

namespace Subscription.Core.Domain
{
    public class Author : BaseModel
    {
        public string Name { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
