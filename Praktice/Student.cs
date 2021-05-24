using System;
using System.Collections.Generic;

#nullable disable

namespace Praktice
{
    public partial class Student
    {
        public Student()
        {
            Files = new HashSet<File>();
        }

        public int Id { get; set; }
        public int Info { get; set; }
        public string Theme { get; set; }
        public int Curator { get; set; }

        public virtual Teacher CuratorNavigation { get; set; }
        public virtual User InfoNavigation { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
