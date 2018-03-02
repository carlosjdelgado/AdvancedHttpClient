using System.IO;
using System.Threading.Tasks;

namespace AdvancedHttpClient.Formatters
{
    public interface IFormatter
    {
        Task Serialize<T>(T input, Stream stream);
        Task<T> Deserialize<T>(Stream stream);
    }
}
