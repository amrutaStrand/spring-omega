using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OmegaUIControls.Cube
{
   public interface IndexedIntArray : IntArray, IntSet 
    {
        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Returns the index of specified value in the array.</returns>
        int IndexOf(int value);
    }
}
