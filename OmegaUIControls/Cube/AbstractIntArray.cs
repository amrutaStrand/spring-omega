using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaUIControls.Cube
{
    /// <summary>
    /// Abstract implementation of IntArray interface.
    /// This class provides default implementation of iterator and equals method.
    /// </summary>
    public abstract class AbstractIntArray : IntArray
    {
        /// <summary>
        /// Creates an iterator to iterate over this array.
        /// This implementation calls
        /// ArrayUtil#createIntIterator(IntArray) ArrayUtil.createIntIterator(IntArray)} for creating the iterator.
        /// </summary>
        /// <returns></returns>

       virtual public IntIterator Iterator()
    {
        return ArrayUtil.CreateIntIterator(this);
    }

    public new bool Equals(object obj)
    {
        if (this == obj)
            return true;

        if (!(obj is IntArray))
	    return false;

        IntArray array = (IntArray)obj;
        int size = GetSize();

        if (array.GetSize() != size)
            return false;

        for (int i = 0; i < size; i++)
        {
            if (Get(i) != array.Get(i))
                return false;
        }

        return true;
    }

        abstract public int GetSize();

        abstract public int Get(int index);

        
        
        //public override string ToString()
        //{
        //    return base.ToString();
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
             
        //}
    }

}
