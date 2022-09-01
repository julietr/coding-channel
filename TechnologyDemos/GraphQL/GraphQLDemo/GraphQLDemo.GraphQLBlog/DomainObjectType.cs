using GraphQL.Types;
using GraphQLDemo.Domain;

namespace GraphQLDemo.GraphQLBlog
{
    public class DomainObjectType<T> : ObjectGraphType<T>
        where T : DomainObject
    {
        public DomainObjectType()
        {
            Field(_ => _.CreatedOn);
            Field(_ => _.Id);
            Field(_ => _.ModifiedOn);
        }
    }
}
