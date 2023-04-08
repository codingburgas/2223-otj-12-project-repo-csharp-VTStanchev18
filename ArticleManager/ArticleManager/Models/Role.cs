using System;
using System.Collections.Generic;

namespace ArticleManager.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? IsAdmin { get; set; }
        public int? PermissionId { get; set; }

        public virtual Permission? Permission { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
