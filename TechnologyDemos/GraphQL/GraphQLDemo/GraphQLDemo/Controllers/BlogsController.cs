using GraphQLDemo.Domain;
using GraphQLDemo.DomainService;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GraphQLDemo.Controllers
{
    public class BlogsController : CrudController<Blog>
    {
        readonly IRepository<Post> _postsRepository;
        public BlogsController(IRepository<Blog> repository, IRepository<Post> postsRepository)
            : base(repository)
        {
            _postsRepository = postsRepository;
        }

        [HttpGet]
        [Route("{blogId}/Posts")]
        public IActionResult GetPosts(int blogId)
        {
            var posts =
                _postsRepository
                .FindAll()
                .Where(_ => _.BlogId == blogId);

            return Ok(posts);
        }
    }
}
