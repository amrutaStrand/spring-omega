using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace OmegaUIControls.Cube
{
    public interface IntSet
    {
        /// <summary>
        /// Creates and returns an iterator to iterate over this set.
        /// </summary>
        /// <returns></returns>
                                                        //  IEnumerator<int> GetEnumerator();
        /// <summary>
        /// </summary>
        /// <param name="element"></param>
        /// <returns>Returns true if the specified element is present in this set.</returns>
        IntIterator Iterator();
         bool Contains(int element);

    }
}
