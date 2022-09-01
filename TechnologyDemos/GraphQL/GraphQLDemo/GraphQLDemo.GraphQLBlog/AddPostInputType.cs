using GraphQL.Types;

namespace GraphQLDemo.GraphQLBlog
{
    public class AddPostInputType : InputObjectGraphType<AddPostInput>
    {
        public AddPostInputType()
        {
            Field(_ => _.BlogId);
            Field(_ => _.Author);
            Field(_ => _.Title);
            Field(_ => _.Content);
        }
    }

    public class AddPostInput
    {
        public int BlogId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
