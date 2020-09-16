using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Copyright 2001-3, Strand Genomics Pvt. Ltd.
/// All Rights Reserved.
/// $Revision: 5614 $ 
/// </summary>
namespace OmegaUIControls.Cube
{

    
    public interface IntArray
    {

        /// <summary>
        /// </summary>
        /// <returns>Returns the size of the array.</returns>
        int GetSize();
        /// <summary>
        /// </summary>
        /// <param name="index">index of the element to return</param>
        /// <returns>Returns the value at the specified position in the array.</returns>
        int Get(int index);
        /// <summary>
        /// </summary>
        /// <returns>Returns an iterator to iterate over this array.</returns>
        IntIterator Iterator();
    }

}
