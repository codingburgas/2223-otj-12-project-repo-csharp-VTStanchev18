using System;
using System.Collections.Generic;

namespace ArticleManager.Models
{
    public partial class Permission
    {
        public Permission()
        {
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public bool? CanCreate { get; set; }
        public bool? CanRead { get; set; }
        public bool? CanUpdate { get; set; }
        public bool? CanDelete { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
