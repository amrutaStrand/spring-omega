using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaUIControls.Cube
{

    /// <summary>
    /// Abstract implementation of IntSet interface. 
    /// This class provides default implementation of equals method.
    /// </summary>
    public abstract class AbstractIntSet : IntSet
    {
        abstract public bool Contains(int element);
       

        public bool Equals_obj(object obj)
    {
        if (this == obj)
            return true;

        if (!(obj is IntSet))
	    return false;

        IntSet set = (IntSet)obj;

        IntIterator a = this.Iterator();
        IntIterator b = set.Iterator();

        while (a.HasNext() && b.HasNext())
        {
            if (a.Next() != b.Next())
                return false;
        }

        if (a.HasNext() || b.HasNext())
            return false;

        return true;
    }

    
        public abstract IntIterator Iterator();
    }

}
