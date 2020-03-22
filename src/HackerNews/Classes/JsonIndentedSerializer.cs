using HackerNews.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HackerNews.Classes
{
    public class JsonIndentedSerializer : ISerializer
    {
        public string Serialize(object obj)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
        }
    }
}