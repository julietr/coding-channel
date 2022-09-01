using System;

namespace GraphQLDemo.Domain
{
    public abstract class DomainObject
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
