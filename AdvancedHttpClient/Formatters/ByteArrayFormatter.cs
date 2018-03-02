using System;
using System.IO;
using System.Threading.Tasks;

namespace AdvancedHttpClient.Formatters
{
    public class ByteArrayFormatter : IFormatter
    {
        public async Task<T> Deserialize<T>(Stream stream)
        {
            ValidateAllowedType(typeof(T));

            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream).ConfigureAwait(false);
                var result = (object)memoryStream.ToArray();
                return (T)result;
            }
        }

        public async Task Serialize<T>(T input, Stream stream)
        {
            await Task.Run(() =>
            {
                ValidateAllowedType(typeof(T));
                var inputAsByteArray = (byte[])Convert.ChangeType(input, typeof(byte[]));
                stream = new MemoryStream(inputAsByteArray);

            }).ConfigureAwait(false);
        }

        private void ValidateAllowedType(Type Type)
        {
            if (Type != typeof(byte[]))
                throw new Exception($"Type of input must be of type {typeof(byte[]).Name}");
        }
    }
}
