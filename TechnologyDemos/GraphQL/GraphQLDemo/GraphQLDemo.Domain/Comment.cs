using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLDemo.Domain
{
    public class Comment : DomainObject
    {
        public int PostId { get; set; }
        public DateTime PublishedOn { get; set; }
        public int AuthorId { get; set; }
        public string Content { get; set; }
        public string FavoriteSoda { get; set; }
    }
}
