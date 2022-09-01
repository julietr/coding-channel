using GraphQL.Types;

namespace GraphQLDemo.GraphQLBlog
{
    public class AddCommentInputType : InputObjectGraphType<AddCommentInput>
    {
        public AddCommentInputType()
        {
            Field(_ => _.Content);
            Field(_ => _.PostId);
            Field(_ => _.Username);
        }
    }

    public class AddCommentInput
    {
        public int PostId { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
    }
}
