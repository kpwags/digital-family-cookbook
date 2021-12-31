using System.Collections.Generic;

namespace DigitalFamilyCookbook.GraphQL
{
    public class GraphQLQuery
    {
        public string OperationName { get; set; } = string.Empty;

        public string NamedQuery { get; set; } = string.Empty;

        public string Query { get; set; } = string.Empty;

        public Dictionary<string, object?> Variables { get; set; } = new Dictionary<string, object?>();
    }
}