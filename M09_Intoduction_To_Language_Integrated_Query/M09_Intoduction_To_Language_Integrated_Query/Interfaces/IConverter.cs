using System.Collections.Generic;

namespace M09_Intoduction_To_Language_Integrated_Query
{
    public interface IConverter<T>
    {
        IReadOnlyCollection<T> Convert();
    }
}
