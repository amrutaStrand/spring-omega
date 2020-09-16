using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    /// <summary>
    /// Copyright 2003-5, Strand Genomics Pvt. Ltd.
    /// All Rights Reserved.
    /// * $Revision: 5915 $ *
    /// </summary>
namespace OmegaUIControls.Cube
{
    /// <summary>
    /// An IntArray that guarantees that all the entries will always be sorted in a specified order.
    /// If no order is specified, then the default order is the ascending order.
    /// This also supports lookup and indexing functionality.
    /// 
    /// - Index of any value can be found in O(1).
    /// - When a series of additions or removal operations are happening, 
    /// the first operation is O(n) and the rest are O(1).
    /// 
    /// LIMITATION: 
    /// This works only with values in the range [0..n), 
    /// where n is the capacity of the array.
    /// </summary>
    public class SortedIntArray : IndexedIntArray
    {
        private int[] data;
        private int[] lookup;
        private bool dirty;

        private int size;

#pragma warning disable CS0414 // The field 'SortedIntArray.debug' is assigned but its value is never used
        private static bool debug = false;
#pragma warning restore CS0414 // The field 'SortedIntArray.debug' is assigned but its value is never used

        public SortedIntArray(int capacity)
        {
            data = new int[capacity];
            lookup = new int[capacity];
            Clear();
        }
        /// <summary>
        /// invoked by hexff.
        /// </summary>
        private SortedIntArray()
        {
        }
        private void Initialize()
        {
            data = new int[lookup.Length];
            dirty = true;
        }

        public int GetCapacity()
        {
            return lookup.Length;
        }

        public void SetCapacity(int capacity)
        {
            // smallest of the two
            int len = capacity < data.Length ? capacity : data.Length;

            int[] _lookup = new int[capacity];
            int[] _data = new int[capacity];

            // preserve the contents
            for (int i = 0; i < len; i++)
                _lookup[i] = lookup[i];

            this.lookup = _lookup;
            this.data = _data;
            dirty = true;
        }

        public int GetSize()
        {
            return size;
        }

        public int Get(int index)
        {
            if (dirty) UpdateData();
            return data[index];
        }

        public void Add(int value)
        {
            if (Contains(value))
                return;

            lookup[value] = 0; // x is present in the array iff lookup[x] >= 0 
            size++;
            dirty = true;
        }

        public void Remove(int value)
        {
            if (!Contains(value))
                return;

            lookup[value] = -1;
            size--;
            dirty = true;
        }

        public void Add(IntArray indices)
        {
            int size = indices.GetSize();
            for (int i = 0; i < size; i++)
            {
                Add(indices.Get(i));
            }
        }

        public void Add(IntIterator iter)
        {
            while (iter.HasNext())
                Add(iter.Next());
        }

        public void Remove(IntArray indices)
        {
            int size = indices.GetSize();
            for (int i = 0; i < size; i++)
            {
                Remove(indices.Get(i));
            }
        }

        public void Remove(IntIterator iter)
        {
            while (iter.HasNext())
                Remove(iter.Next());
        }

        public void Invert()
        {
            int capacity = GetCapacity();
            size = capacity - size;

            for (int i = 0; i < capacity; i++)
            {
                if (lookup[i] < 0)
                    lookup[i] = 0;
                else
                    lookup[i] = -1;
            }

            dirty = true;
        }

        public void Intersect(IntSet array)
        {
            int capacity = GetCapacity();

            size = 0;

            for (int i = 0; i < capacity; i++)
            {
                if (lookup[i] >= 0 && array.Contains(i))
                {
                    lookup[i] = 0;
                    size++;
                }
                else
                    lookup[i] = -1;
            }

            dirty = true;
        }

        public int IndexOf(int value)
        {
            if (value < 0 || value >= lookup.Length)
                return -1;

            if (dirty) UpdateData();
            return lookup[value];
        }

        public bool Contains(int value)
        {
            return (0 <= value && value < lookup.Length && lookup[value] >= 0);
        }

        public IntIterator Iterator()
        {
            if (dirty)
                UpdateData();

            return ArrayUtil.CreateIntIterator(data, size);
        }

        public void Clear()
        {
            int capacity = GetCapacity();
            size = 0;
            for (int i = 0; i < capacity; i++)
                lookup[i] = -1;
            dirty = true;
        }

        public void SelectAll()
        {
            int capacity = GetCapacity();
            size = capacity;
            for (int i = 0; i < capacity; i++)
                lookup[i] = 0;
            dirty = true;
        }

        // data is in invalid state. 
        // update data from lookup.
        // 
        // array = { x | lookup[x] >= 0 }
        private void UpdateData()
        {
            size = 0;

            for (int i = 0; i < lookup.Length; i++)
            {
                int index = i;

                if (lookup[index] < 0)
                    continue;

                data[size] = index;
                lookup[index] = size;
                size++;
            }

            dirty = false;
        }

        public new bool Equals(object o)
        {
            if (o == null)
                return false;

            if (!(o is SortedIntArray))
	    return false;

            SortedIntArray that = (SortedIntArray)o;

            if (this.GetSize() != that.GetSize())
                return false;

            int size = this.GetSize();

            for (int i = 0; i < size; i++)
                if (this.Get(i) != that.Get(i))
                    return false;

            return true;
        }

        //public override string ToString()
        //{
        //    return ArrayUtil.ToString(this);
        //}

        public void Cleanup()
        {
            data = null;
            lookup = null;
        }

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}
    }
}
