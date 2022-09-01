using GraphQLDemo.Domain;
using GraphQLDemo.DomainService;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GraphQLDemo.Controllers
{
    public class UsersController : CrudController<User>
    {
        readonly IRepository<Blog> _blogsRepository;
        readonly IRepository<Post> _postsRepository;
        readonly IRepository<Comment> _commentsRepository;

        public UsersController(IRepository<User> repository, IRepository<Blog> blogsRepository, IRepository<Post> postsRepository, IRepository<Comment> commentsRepository)
            : base(repository)
        {
            _blogsRepository = blogsRepository;
            _postsRepository = postsRepository;
            _commentsRepository = commentsRepository;
        }

        [HttpGet]
        [Route("{userId}/Blogs")]
        public IActionResult GetBlogs(int userId)
        {
            var blogs =
                _blogsRepository
                .Find(_ => _.CreatedBy == userId);

            return Ok(blogs);
        }

        [HttpGet]
        [Route("{userId}/Posts")]
        public IActionResult GetPosts(int userId, [FromQuery] int? blogId)
        {
            var posts =
                _postsRepository
                .Find(_ =>
                    _.Authors.Contains(userId) &&
                    (
                        blogId == null ||
                        _.BlogId == blogId
                    ));

            return Ok(posts);
        }

        [HttpGet]
        [Route("{userId}/Comments")]
        public IActionResult GetComments(int userId, [FromQuery] int? blogId, [FromQuery] int? postId)
        {
            var comments =
                _commentsRepository
                .Find(_ =>
                    _.AuthorId == userId &&
                    (
                        blogId == null ||
                        blogId == _postsRepository.Retrieve(_.PostId)?.BlogId
                    ) &&
                    (
                        postId == null ||
                        postId == _.PostId
                    ));

            return Ok(comments);
        }
    }
}
