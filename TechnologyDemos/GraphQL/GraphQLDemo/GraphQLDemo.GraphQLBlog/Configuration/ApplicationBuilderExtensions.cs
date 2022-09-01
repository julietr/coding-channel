using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Voyager;
using GraphQLDemo.GraphQLBlog;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseBlogGraphQL(this IApplicationBuilder app)
        {
            app.UseGraphQL<ApiSchema>("/graphql");

            app.UseGraphQLGraphiQL(
                new GraphiQLOptions
                {
                    GraphQLEndPoint = "/graphql"
                },
                path: "/ui/graphiql");

            app.UseGraphQLVoyager(
                new VoyagerOptions
                {
                    GraphQLEndPoint = "/graphql",
                },
                path: "/ui/voyager");
        }
    }
}
