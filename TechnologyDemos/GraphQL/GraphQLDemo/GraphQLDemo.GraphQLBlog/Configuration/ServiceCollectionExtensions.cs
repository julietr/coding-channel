using GraphQLDemo.GraphQLBlog;
using GraphQL.Server;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBlogGraphQL(this IServiceCollection services)
        {
            services.AddSingleton<ApiSchema>();
            services.AddGraphQL(options =>
                {
                    options.EnableMetrics = false;
                })
                .AddSystemTextJson()
                .AddGraphTypes();
        }
    }
}
