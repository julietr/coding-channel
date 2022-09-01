using GraphQLDemo.Domain;
using GraphQLDemo.DomainService;

namespace GraphQLDemo.Controllers
{
    public class CommentsController : CrudController<Comment>
    {
        public CommentsController(IRepository<Comment> repository)
            : base(repository)
        {

        }
    }
}
