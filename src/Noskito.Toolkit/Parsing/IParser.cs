using System.IO;
using System.Threading.Tasks;

namespace Noskito.Toolkit.Parsing
{
    public interface IParser
    {
        Task Parse(DirectoryInfo directory);
    }
}