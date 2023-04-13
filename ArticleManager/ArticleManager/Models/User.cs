using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArticleManager.Models
{
    public partial class User
    {
        public User()
        {
            UserArticles = new HashSet<UserArticle>();
        }
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }
        //public bool IsActive { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<UserArticle> UserArticles { get; set; }
    }
}
