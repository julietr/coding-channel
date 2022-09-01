using GraphQLDemo.ApplicationService;
using GraphQLDemo.Domain;
using GraphQLDemo.DomainService;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBlog(this IServiceCollection services)
        {
            var userRepo = new Repository<User>();
            var blogRepo = new Repository<Blog>();
            var postRepo = new Repository<Post>();
            var commentRepo = new Repository<Comment>();

            var romeo =
                userRepo.Add(
                    new User
                    {
                        FirstName = "Romeo",
                        LastName = "Montague",
                        UserName = "romeom",
                        CreatedOn = DateTime.Now
                    });
            var juliet =
                userRepo.Add(
                    new User
                    {
                        FirstName = "Juliet",
                        LastName = "Capulet",
                        UserName = "julietc",
                        CreatedOn = DateTime.Now
                    });
            var mercutio =
                userRepo.Add(
                    new User
                    {
                        FirstName = "Mercutio",
                        LastName = "Escalus",
                        UserName = "mercutioe",
                    });

            var julietBlog =
                blogRepo.Add(
                    new Blog
                    {
                        Id = 1,
                        CreatedBy = juliet.Id,
                        Name = "Juliet's Diary",
                        Descripton = "Where for art thou, Romeo?"
                    });

            var romeoBlog =
                blogRepo.Add(
                    new Blog
                    {
                        Id = 2,
                        CreatedBy = romeo.Id,
                        Name = "Harry Potter Fanfic",
                        Descripton = "Harry Potter and the Intrigue of Romeo Wandspell"
                    });

            var post1 =
                postRepo.Add(
                    new Post
                    {
                        Authors = new[] { juliet.Id },
                        BlogId = julietBlog.Id,
                        PublishedOn = DateTime.Now,
                        Title = "Looking for someone",
                        Content = "Romeo, where you at?",
                        Tags = new[] { "shakespeargate", "cats" }
                    });
            var post2 =
                postRepo.Add(
                    new Post
                    {
                        Authors = new[] { juliet.Id },
                        BlogId = julietBlog.Id,
                        PublishedOn = DateTime.Now,
                        Title = "I'm slowly becoming a crazy cat lady",
                        Content = "I love cats. I love every kind of cat. I just want to hug all of them, but I can't hug every cat. Can't hug every cat.",
                        Tags = new[] { "cats" }
                    });
            var post3 =
                postRepo.Add(
                    new Post
                    {
                        Authors = new[] { juliet.Id },
                        BlogId = julietBlog.Id,
                        PublishedOn = DateTime.Now,
                        Title = "Ah ah ah ah",
                        Content = "STAYIN' ALIVE!",
                        Tags = new[] { "yaaas queeen" }
                    });
            var post4 =
                postRepo.Add(
                    new Post
                    {
                        Authors = new[] { romeo.Id },
                        BlogId = romeoBlog.Id,
                        PublishedOn = DateTime.Now,
                        Title = "A Stranger Comes to Town",
                        Content = "Romeo Wandspell is no ordinary wizard. He's bigger than Hagrid. Smarter than Hermione. And more charming than Gilderoy Lockhart. And let's face it, he's a better Quidditch seeker than Harry Pipsqueak Potter.",
                        Tags = new[] { "fanfic", "harry potter", "quidditch" }
                    });
            var comment1 =
                commentRepo.Add(
                    new Comment
                    {
                        PostId = post4.Id,
                        AuthorId = juliet.Id,
                        PublishedOn = DateTime.Now,
                        Content = "Worst. Plot. Ever.",
                    });
            var comment2 =
                commentRepo.Add(
                    new Comment
                    {
                        PostId = post4.Id,
                        AuthorId = romeo.Id,
                        PublishedOn = DateTime.Now,
                        Content = "mY nAmE Is jUlIet aNd tHis iS tHe wOrsT pLot eVaR waaaaahh"
                    });
            var comment4 =
                commentRepo.Add(
                    new Comment
                    {
                        PostId = post1.Id,
                        AuthorId = mercutio.Id,
                        Content = "Romeo is a Jokeo. Why not Mercutio?",
                        PublishedOn = DateTime.Now,
                    });
            var comment5 =
                commentRepo.Add(
                    new Comment
                    {
                        PostId = post1.Id,
                        AuthorId = juliet.Id,
                        Content = "Why not Zoidberg?",
                        PublishedOn = DateTime.Now
                    });

            services.AddSingleton<IRepository<User>>(_ => userRepo);
            services.AddSingleton<IRepository<Blog>>(_ => blogRepo);
            services.AddSingleton<IRepository<Post>>(_ => postRepo);
            services.AddSingleton<IRepository<Comment>>(_ => commentRepo);
        }
    }
}