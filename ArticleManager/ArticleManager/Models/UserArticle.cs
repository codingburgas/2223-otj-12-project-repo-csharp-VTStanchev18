using System;
using System.Collections.Generic;

namespace ArticleManager.Models
{
    public partial class UserArticle
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ArticleId { get; set; }

        public virtual Article? Article { get; set; }
        public virtual User? User { get; set; }
    }
}
