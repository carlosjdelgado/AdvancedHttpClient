using System;
using System.IO;
using System.Threading.Tasks;

namespace AdvancedHttpClient.Formatters
{
    public class StreamFormatter : IFormatter
    {
        public async Task<T> Deserialize<T>(Stream stream)
        {
            ValidateAllowedType(typeof(T));
            var result = (T)Convert.ChangeType(stream, typeof(T));
            return await Task.FromResult(result).ConfigureAwait(false) ;
        }

        public async Task Serialize<T>(T input, Stream stream)
        {
            ValidateAllowedType(typeof(T));
            using (var sourceStream = (Stream)Convert.ChangeType(input, typeof(Stream)))
            {
                await sourceStream.CopyToAsync(stream).ConfigureAwait(false);
                stream.Position = 0;
            }
        }

        private void ValidateAllowedType(Type Type)
        {
            if (Type != typeof(Stream))
                throw new Exception($"Type of input must be of type {typeof(Stream).Name}");
        }
    }
}
