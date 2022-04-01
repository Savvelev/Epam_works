using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Performance.Program;

namespace Performance
{
    class C : IField, IComparable
    {
        public int I { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is C c)
                return I.CompareTo(c.I);
            else
                throw new InvalidOperationException("Unable to compare this objects");
        }
    }
}
