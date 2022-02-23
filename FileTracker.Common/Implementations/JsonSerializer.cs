using FileTracker.Common.Interfaces;
using Newtonsoft.Json;

namespace FileTracker.Common.Implementations
{
    internal class JsonSerializer : ISerializer
    {
        public T Deserialize<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

        public string Serialize<T>(T input)
        {
            return JsonConvert.SerializeObject(input);
        }
    }
}