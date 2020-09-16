using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OmegaUIControls.Cube
{
    /// <summary>
    /// Utilities for creating arrays and iterators.
    /// </summary>
    public class ArrayUtil
    {
        public static IntIterator CreateIntIterator(IntArray array)
        {
            return new HelperIntIterator2(array);
        }

        public static IntIterator CreateIntIterator(int[] data)
        {
            return CreateIntIterator(data, data.Length);
        }
        /// <summary>
        /// Creates and returns an IntIterator using first n elements of the specified data.
        /// </summary>
        /// <param name=""></param>
        /// <param name="data"></param>
        /// <param name=""></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static IntIterator CreateIntIterator(int[] data, int n)
        {
            return new HelperIntIteratorData(data, n);
        }

        public static IntArray CreateIntArray(int[] data)
        {
            return CreateIntArray(data, data.Length);
        }

        /// <summary>
        /// Creates an IntArray with the specified data with the first 
        /// elements of the array.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="n">Size of the created IntArray will be n.</param>
        /// <returns></returns>
        public static IntArray CreateIntArray(int[] data, int n)
        {
            return new HelperIntArray(data, n);
        }



        /// <summary>
        /// Creates an IntSet from the given int[].
        /// calls <code>ArrayUtil.createIntSet(ArrayUtil.createIntArray(data))</code>.
        /// TODO: optimize
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IntSet CreateIntSet(int[] data)
        {
            return CreateIntSet(CreateIntArray(data));
        }

        /// <summary>
        /// Constructs an IntSet using the specified array.
        /// <para>
        /// NOTE: The array should not have any negative elements.
        /// </para>
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static IntSet CreateIntSet(IntArray array)
        {
            if (array is IntSet)
                return (IntSet)array;

            int size = array.GetSize();
            BitSetIntSet bitset = new BitSetIntSet(size);

            for (int i = 0; i < size; i++)
                bitset.Set(array.Get(i));

            return bitset;
        }

        internal class HelperIntIterator2 : IntIterator
        {
            protected int index;
            protected IntArray array;
            protected int size;

            public HelperIntIterator2(IntArray array)
            {
                this.array = array;
                size = array.GetSize();
                index = 0;
            }
            public bool HasNext()
            {
                return index < size;
            }

            public int Next()
            {
                return array.Get(index++);
            }
            //public override string ToString()
            //{
            //    return ArrayUtil.ToString(this);
            //}
        }

        internal class HelperIntIteratorData : IntIterator
        {
            private int index;
            private int[] data;
            private int n;

            public HelperIntIteratorData(int[] data, int n)
            {
                index = 0;
                this.data = data;
                this.n = n;
            }
            public bool HasNext()
            {
                return index < n;
            }

            public int Next()
            {
                return data[index++];
            }
        }

        internal class HelperIntArray : AbstractIntArray
        {
            private int[] data;
            private int n;
            public HelperIntArray(int[] data, int n)
            {
                this.data = data;
                this.n = n;
            }

            override public int GetSize()
            {
                return n;
            }

            override public int Get(int index)
            {
                return data[index];
            }

            //public override string ToString()
            //{
            //    return this.ToString();
            //}
        }
        /// <summary>
        ///  
        /// {{{ Utility classes 
        /// Array classes
        /// </summary>
    }
}


