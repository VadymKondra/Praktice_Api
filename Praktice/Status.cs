using System;
using System.Collections.Generic;

#nullable disable

namespace Praktice
{
    public partial class Status
    {
        public Status()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
