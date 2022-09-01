using GraphQL;
using GraphQL.Types;
using GraphQLDemo.Domain;
using GraphQLDemo.DomainService;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLDemo.GraphQLBlog
{
    public class PostType : DomainObjectType<Post>
    {
        public PostType(
            IRepository<User> userRepository,
            IRepository<Blog> blogRepository,
            IRepository<Comment> commentRepository)
        {
            Field<BlogType, Blog>("blog")
                .Resolve(context => blogRepository.Retrieve(context.Source.BlogId));
            Field<ListGraphType<UserType>, IEnumerable<User>>("users")
                .Resolve(context => context.Source.Authors.Select(authorId => userRepository.Retrieve(authorId)));
            Field(_ => _.Content);
            Field(_ => _.PublishedOn);
            Field(_ => _.Tags);
            Field(_ => _.Title);
            Field<ListGraphType<CommentType>, IEnumerable<Comment>>("comments")
                .Argument<IdGraphType, int?>("userId", "The user id")
                .Resolve(context =>
                {
                    var userId = context.GetArgument<int?>("userId");
                    return commentRepository.Find(_ =>
                        _.PostId == context.Source.Id &&
                        _.AuthorId == userId.GetValueOrDefault(_.AuthorId));
                });
        }
    }
}
