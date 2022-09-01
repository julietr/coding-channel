using GraphQL;
using GraphQL.Types;
using GraphQLDemo.Domain;
using GraphQLDemo.DomainService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLDemo.GraphQLBlog
{
    public class ApiQuery : ObjectGraphType
    {
        public ApiQuery(
            IRepository<User> userRepository,
            IRepository<Blog> blogRepository,
            IRepository<Post> postRepository,
            IRepository<Comment> commentRepository)
        {
            Field<ListGraphType<UserType>, IEnumerable<User>>("users")
                .Resolve(context => userRepository.FindAll());
            Field<UserType, User>("user")
                .Argument<NonNullGraphType<IdGraphType>, int>("id", "The user id")
                .Resolve(context =>
                {
                    var id = context.GetArgument<int>("id");
                    return userRepository.Retrieve(id);
                });

            Field<ListGraphType<BlogType>, IEnumerable<Blog>>("blogs")
                .Argument<IdGraphType, int?>("userId", "The user id")
                .Resolve(context =>
                {
                    var userId = context.GetArgument<int?>("userId");
                    return blogRepository.Find(_ => _.CreatedBy == userId.GetValueOrDefault(_.CreatedBy));
                });
            Field<BlogType, Blog>("blog")
                .Argument<NonNullGraphType<IdGraphType>, int>("id", "The blog id")
                .Resolve(context =>
                {
                    var id = context.GetArgument<int>("id");
                    return blogRepository.Retrieve(id);
                });

            Field<ListGraphType<PostType>, IEnumerable<Post>>("posts")
                .Argument<IdGraphType, int?>("userId", "The user id")
                .Argument<IdGraphType, int?>("blogId", "The blog id")
                .Resolve(context =>
                {
                    var userId = context.GetArgument<int?>("userId");
                    var blogId = context.GetArgument<int?>("blogId");
                    return postRepository.Find(_ =>
                        _.BlogId == blogId.GetValueOrDefault(_.BlogId) &&
                        (userId == null || _.Authors.Contains(userId.Value)));
                });
            Field<PostType, Post>("post")
                .Argument<NonNullGraphType<IdGraphType>, int>("id", "The post id")
                .Resolve(context =>
                {
                    var id = context.GetArgument<int>("id");
                    return postRepository.Retrieve(id);
                });

            Field<ListGraphType<CommentType>, IEnumerable<Comment>>("comments")
                .Argument<IdGraphType, int?>("userId", "The user id")
                .Argument<IdGraphType, int?>("postId", "The post id")
                .Resolve(context =>
                {
                    var authorId = context.GetArgument<int?>("userId");
                    var postId = context.GetArgument<int?>("postId");
                    return commentRepository.Find(_ =>
                        _.AuthorId == authorId.GetValueOrDefault(_.AuthorId) &&
                        _.PostId == postId.GetValueOrDefault(_.PostId));
                });
            Field<CommentType, Comment>("comment")
                .Argument<NonNullGraphType<IdGraphType>, int>("id", "The comment id")
                .Resolve(context =>
                {
                    var id = context.GetArgument<int>("id");
                    return commentRepository.Retrieve(id);
                });
        }
    }
}
