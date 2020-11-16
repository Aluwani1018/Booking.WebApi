using System.Collections.Generic;

namespace Subscription.Core.Domain
{
    public class Book : BaseModel
    {
        public int PublisherId { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public double PurchasePrice { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }

        public virtual Publisher Publisher { get; set; }
    }
}
