using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

namespace OmegaUIControls.Cube
{
    /// <summary>
    /// An implementation of IntSet that uses <code>BitSet</code>.
    /// <see cref="java.util.BitSet"/>
    /// </summary>
    public class BitSetIntSet : AbstractIntSet
    {
        // BitSet to store data , Bitset was used in Java,  BitArray is used in C#

        private BitArray bitset;
        public int size;

        /// <summary>
        /// Constructs a BitSetIntSet with default capacity.
        /// </summary>
        public BitSetIntSet()
        {
            bitset = new BitArray(0);
        }
        /// <summary>
        /// Constructs a BitSetIntSet with the specified capacity. 
        /// </summary>
        /// <param name="capacity"></param>
        public BitSetIntSet(int capacity)
        {
            bitset = new BitArray(capacity);
            this.size = capacity;
        }

        /// <summary>
        /// This is hack added to handle bug BitSetIntSet format 1.0,
        /// size was not save in it, so many places where size is used it won't work!
        /// </summary>
        /// <param name="capacity"></param>
        public void SetCapacity(int capacity)
        {
            this.size = capacity;
        }

        public  void Set(int index)
        {
            bitset.Set(index,true);
        }

        public void SetAll()
        {
            // public void Set (int index, bool value); in C#
            bitset.SetAll(true);
        }

        //Clear(int index) makes value at index false, Set(int index, value) is an alternative in C#
        public void Clear(int index)
        {
            bitset.Set(index,false);
        }

        // clear() was used in java to make all the value false in java BitSet,SetAll is used in C#
        public void Clear()
        {
            bitset.SetAll(false);
        }

        // Not() doesn't take any argument in C#, it compliments entire set. Whereas in java flip(int index)
        // is used to get compliment at specified index 
        // In C# I have written a custom logic
        public void Invert(int index)
        {
            // Not is used in C#. Flip was used in Java
            if (bitset[index] == false) bitset[index] = true;
            else bitset[index] = false;
        }

        public void Invert()
        {
            // Not is used in C#. Flip was used in Java
            bitset.Not();
        }

        public void Add(int index)
        {
            bitset.Set(index,true);
        }

        public void Add(IntIterator indices)
        {
            while (indices.HasNext())
                bitset.Set(indices.Next(),true);
        }

        /// <summary>
        /// Returns the bitset of this set.
        /// </summary>
        /// <returns></returns>
        public BitArray GetBitSet()
        {
            return bitset;
        }

        public override bool Contains(int value)
        {
            return (value >= 0) && bitset.Get(value);
        }
        public override IntIterator Iterator()
        {
            BitArray bitset = this.bitset;
            return new HelperAbstractIntIterator(bitset);
        }
        
    }
    internal class HelperAbstractIntIterator : AbstractIntIterator
    {
        private BitArray bitset;
        public HelperAbstractIntIterator(BitArray bitArray)
        {
            bitset = bitArray;
            nextElement = -1;
            Initialize();
        }

        // NextSetBit(int fromIndex) in BitSet Class is used to return the index 
        // of the first bit that is set to true, that occurs on or after the specified 
        //starting index. If no such bit exists then -1 is returned.
        // Here I have writen a custom logic 
        public override int FindNextElement()
        {
           
            for(int i = nextElement + 1; i<bitset.Length; i++)
            {
                if(bitset[i]== true)
                {
                    return i;
                }
            }
            
            return -1;
           
            // return bitset.NextSetBit(nextElement + 1); java code
        }
    }
}
