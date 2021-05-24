using System;
using System.Collections.Generic;

#nullable disable

namespace Praktice
{
    public partial class File
    {
        public int Id { get; set; }
        public byte[] File1 { get; set; }
        public int? Author { get; set; }

        public virtual Student AuthorNavigation { get; set; }
    }
}
