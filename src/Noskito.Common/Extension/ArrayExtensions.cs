using System.Linq;

namespace Noskito.Common.Extension
{
    public static class ArrayExtensions
    {
        public static T[] Slice<T>(this T[] array, int startIndex, int length)
        {
            return array.Skip(startIndex).Take(length).ToArray();
        }
    }
}