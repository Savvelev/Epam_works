using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Performance.Program;

namespace Performance
{
    struct S : IField, IComparable<S>
    {
        public int I { get; set; }

        public int CompareTo(S obj)
        {
            if (obj is S s)
                return I.CompareTo(s.I);
            else
                throw new InvalidOperationException("Unable to compare this objects");
        }       
    }
}
