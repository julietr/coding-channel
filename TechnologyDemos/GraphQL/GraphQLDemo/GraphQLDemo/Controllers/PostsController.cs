using GraphQLDemo.Domain;
using GraphQLDemo.DomainService;
using Microsoft.AspNetCore.Mvc;

namespace GraphQLDemo.Controllers
{
    public class PostsController : CrudController<Post>
    {
        public IRepository<Comment> _commentsRepository;

        public PostsController(IRepository<Post> repository, IRepository<Comment> commentsRepository)
            : base(repository)
        {
            _commentsRepository = commentsRepository;
        }

        [HttpGet]
        [Route("{postId}/Comments")]
        public IActionResult GetComments(int postId)
        {
            var comments =
                _commentsRepository
                .Find(_ => _.PostId == postId);

            return Ok(comments);
        }
    }
}
