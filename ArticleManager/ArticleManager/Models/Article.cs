using System;
using System.Collections.Generic;

namespace ArticleManager.Models
{
    public partial class Article
    {
        public Article()
        {
            UserArticles = new HashSet<UserArticle>();
        }

        public int Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? EditedOn { get; set; }
        public byte[]? Image { get; set; }

        public virtual ICollection<UserArticle> UserArticles { get; set; }
    }
}
