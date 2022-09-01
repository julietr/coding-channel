using GraphQL.Types;
using GraphQLDemo.Domain;
using GraphQLDemo.DomainService;

namespace GraphQLDemo.GraphQLBlog
{
    public class CommentType : DomainObjectType<Comment>
    {
        public CommentType(
            IRepository<User> userRepository,
            IRepository<Post> postRepository)
        {
            Field<PostType, Post>("post")
                .Resolve(context => postRepository.Retrieve(context.Source.PostId));
            Field<UserType, User>("user")
                .Resolve(context => userRepository.Retrieve(context.Source.AuthorId));
            Field(_ => _.Content);
            Field(_ => _.PublishedOn);
            Field(_ => _.FavoriteSoda, nullable: true)
                //.DeprecationReason("Why did we think people would put their favorite soda on every comment?!")
                ;
        }
    }
}
