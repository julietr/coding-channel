using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLDemo.Domain
{
    public class Post : DomainObject
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<int> Authors { get; set; }
        public DateTime PublishedOn { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
