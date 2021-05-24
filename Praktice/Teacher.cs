using System;
using System.Collections.Generic;

#nullable disable

namespace Praktice
{
    public partial class Teacher
    {
        public Teacher()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public int? Info { get; set; }
        public string Cathedra { get; set; }

        public virtual User InfoNavigation { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
