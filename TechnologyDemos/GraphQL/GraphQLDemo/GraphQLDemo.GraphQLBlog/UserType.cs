using GraphQL;
using GraphQL.Types;
using GraphQLDemo.Domain;
using GraphQLDemo.DomainService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLDemo.GraphQLBlog
{
    public class UserType : DomainObjectType<User>
    {
        public UserType(
            IRepository<Blog> blogRepository,
            IRepository<Post> postRepository,
            IRepository<Comment> commentRepository)
        {
            Field(_ => _.FirstName);
            Field(_ => _.LastName);
            Field(_ => _.UserName);
            Field<ListGraphType<BlogType>, IEnumerable<Blog>>("blogs")
                .Resolve(context => blogRepository.Find(_ => _.CreatedBy == context.Source.Id));
            Field<ListGraphType<PostType>, IEnumerable<Post>>("posts")
                .Argument<IdGraphType, int?>("blogId", "The blog id")
                .Resolve(context =>
                {
                    var blogId = context.GetArgument<int?>("blogId");
                    return postRepository.Find(_ =>
                        _.Authors.Contains(context.Source.Id) &&
                        _.BlogId == blogId.GetValueOrDefault(_.BlogId));
                });
            Field<ListGraphType<CommentType>, IEnumerable<Comment>>("comments")
                .Argument<IdGraphType, int?>("postId", "The post id")
                .Resolve(context =>
                {
                    var postId = context.GetArgument<int?>("postId");
                    return commentRepository.Find(_ =>
                        _.AuthorId == context.Source.Id &&
                        _.PostId == postId.GetValueOrDefault(_.PostId));
                });
        }
    }
}
