using GraphQL;
using GraphQL.Types;
using GraphQLDemo.Domain;
using GraphQLDemo.DomainService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLDemo.GraphQLBlog
{
    public class BlogType : DomainObjectType<Blog>
    {
        public BlogType(
            IRepository<User> userRepository,
            IRepository<Post> postRepository)
        {
            Field(_ => _.Descripton);
            Field(_ => _.Name);
            Field<UserType, User>("createdBy")
                .Resolve(context => userRepository.Retrieve(context.Source.CreatedBy));
            Field<ListGraphType<PostType>, IEnumerable<Post>>("posts")
                .Argument<IdGraphType, int?>("userId", "The user id")
                .Resolve(context =>
                {
                    var userId = context.GetArgument<int?>("userId");
                    return postRepository.Find(_ =>
                        _.BlogId == context.Source.Id &&
                        (userId.HasValue ? _.Authors.Contains(userId.Value) : true));
                });
        }
    }
}
