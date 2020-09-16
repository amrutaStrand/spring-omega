using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaUIControls.Cube
{
    /// <summary>
    /// Abstract implementation of IntIterator interface.
    /// </summary>
    public abstract class AbstractIntIterator : IntIterator
    {

    /// <summary>
    /// Next element in the iteration.
    /// -1 if there is no next element.
    /// </summary>
    protected int nextElement;

        /// <summary>
        /// Default constrcutor.
        /// </summary>
        public AbstractIntIterator()
        {
        }

   /// <summary>
   /// Initializes this iterator.
   /// This method must be called from the constructor of any subclass.
   /// </summary>
    protected void Initialize()
    {
        nextElement = FindNextElement();
    }

        /// <summary>
        ///  Returns the next element in the iterator.  
        /// </summary>
        /// <returns>If end of iterator is reached, this method returns -1.</returns>
        public abstract int FindNextElement();

    public bool HasNext()
    {
        return nextElement >= 0;
    }

    public int Next()
    {
        int element = nextElement;
        nextElement = FindNextElement();
        return element;
    }
}

}
