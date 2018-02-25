using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedHttpClient.Formatters
{
    public class ModelAsJsonFormatter : IFormatter
    {
        public async Task<T> Deserialize<T>(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                var serializer = new JsonSerializer();
                var output = serializer.Deserialize<T>(jsonReader);
                stream.Position = 0;
                return await Task.FromResult(output).ConfigureAwait(false);
            }
        }

        public async Task Serialize<T>(T input, Stream stream)
        {
            var streamWriter = new StreamWriter(stream);
            var jsonWriter = new JsonTextWriter(streamWriter);
            var serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, input);
            await streamWriter.FlushAsync().ConfigureAwait(false);
            stream.Position = 0;
        }
    }
}
