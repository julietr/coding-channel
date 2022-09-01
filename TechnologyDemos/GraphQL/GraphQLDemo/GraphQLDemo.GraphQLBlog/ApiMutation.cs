using GraphQL;
using GraphQL.Types;
using GraphQLDemo.Domain;
using GraphQLDemo.DomainService;
using System.Linq;

namespace GraphQLDemo.GraphQLBlog
{
    public class ApiMutation : ObjectGraphType
    {
        public ApiMutation(
            IRepository<User> userRepository,
            IRepository<Post> postRepository,
            IRepository<Comment> commentRepository)
        {
            Field<PostType, Post>("addPost")
                .Argument<NonNullGraphType<AddPostInputType>, AddPostInput>("message", "The request object")
                .Resolve(context =>
                {
                    var message = context.GetArgument<AddPostInput>("message");
                    var authorIds =
                        userRepository
                        .Find(_ => message.Author.Contains(_.UserName))
                        .Select(_ => _.Id);
                    var post = postRepository.Add(
                        new Post
                        {
                            Authors = authorIds,
                            BlogId = message.BlogId,
                            Title = message.Title,
                            Content = message.Content
                        });
                    return post;
                });

            Field<CommentType, Comment>("addComment")
                .Argument<NonNullGraphType<AddCommentInputType>, AddCommentInput>("message", "The request object")
                .Resolve(context =>
                {
                    var message = context.GetArgument<AddCommentInput>("message");
                    var author = userRepository.Find(_ => _.UserName == message.Username).SingleOrDefault();
                    var comment = commentRepository.Add(
                        new Comment
                        {
                            AuthorId = author.Id,
                            PostId = message.PostId,
                            Content = message.Content
                        });
                    return comment;
                });
        }
    }
}
