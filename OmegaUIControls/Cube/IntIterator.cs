using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaUIControls.Cube
{

    /// <summary>
    /// Iterator interface to iterate through a sequence of integers.
    /// </summary>
    public interface IntIterator
    {
        /// <summary>
        /// </summary>
        /// <returns>Returns true if the iterator has more elements.</returns>
        bool HasNext();

        /// <summary>
        /// </summary>
        /// <returns>Returns the next element in the iteration.</returns>
        int Next();
    }

}
