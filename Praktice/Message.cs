using System;
using System.Collections.Generic;

#nullable disable

namespace Praktice
{
    public partial class Message
    {
        public int Id { get; set; }
        public int? Author { get; set; }
        public string Text { get; set; }
        public DateTime? Date { get; set; }

        public virtual User AuthorNavigation { get; set; }
    }
}
