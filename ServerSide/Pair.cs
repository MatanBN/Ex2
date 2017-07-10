using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    public class Pair<T, P>
    {
        T item1;
        P item2;

        public Pair(T one, P two)
        {
            this.item1 = one;
            this.item2 = two;
        }
        public T Item1
        {
            get
            {
                return this.item1;
            }
            set
            {
                this.item1 = value;
            }
        }
        public P Item2
        {
            get
            {
                return this.item2;
            }
            set
            {
                this.item2 = value;
            }
        }
    }
}
