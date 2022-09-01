using GraphQL.Types;
using System;

namespace GraphQLDemo.GraphQLBlog
{
    public class ApiSchema : Schema
    {
        public ApiSchema(IServiceProvider serviceProvider, ApiQuery query, ApiMutation mutation)
            : base(serviceProvider)
        {
            Query = query;
            Mutation = mutation;
        }
    }
}
