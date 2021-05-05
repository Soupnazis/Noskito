using System.Collections.Generic;
using Mapster;

namespace Noskito.Database
{
    public sealed class Mapper<T1, T2>
    {
        public T1 Map(T2 value)
        {
            return value.Adapt<T1>();
        }

        public T2 Map(T1 value)
        {
            return value.Adapt<T2>();
        }

        public IEnumerable<T1> Map(IEnumerable<T2> values)
        {
            return values.Adapt<IEnumerable<T1>>();
        }

        public IEnumerable<T2> Map(IEnumerable<T1> values)
        {
            return values.Adapt<IEnumerable<T2>>();
        }
    }
}