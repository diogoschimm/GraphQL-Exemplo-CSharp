using GraphQL.Types;
using GraphQL.Utilities;
using System;

namespace GraphQLSampleWeb.GraphQLCore
{
    public class AppSchema: Schema
    {
        public AppSchema(IServiceProvider provider) :base(provider)
        {
            Query = provider.GetRequiredService<AppQuery>();
        }
    }
}
