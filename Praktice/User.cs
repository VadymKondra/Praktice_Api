using System;
using System.Collections.Generic;

#nullable disable

namespace Praktice
{
    public partial class User
    {
        public User()
        {
            Messages = new HashSet<Message>();
            Teachers = new HashSet<Teacher>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public int Status { get; set; }
        public string Password { get; set; }

        public virtual Status StatusNavigation { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
