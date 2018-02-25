using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedHttpClient.Formatters
{
    public interface IFormatter
    {
        Task Serialize<T>(T input, Stream stream);
        Task<T> Deserialize<T>(Stream stream);
    }
}
