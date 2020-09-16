using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaUIControls.Cube
{
    public class DefaultIntArray : AbstractIntArray
    {
        private int[] data;
        private int size;

        /// <summary>
        /// Creates a DefaultIntArray with an initial capacity of ten.
        /// </summary>
        public DefaultIntArray() : this(10)
        {

        }
        public DefaultIntArray(int capacity)
        {
            this.data = new int[capacity];
            this.size = 0;
        }

        /// <summary>
        /// Increases the capacity of this <tt>DefaultIntArray</tt> instance, if
        /// necessary, to ensure  that it can hold at least the number of elements
        /// specified by the minimum capacity argument.
        /// </summary>
        /// <param name="minCapacity">the desired minimum capacity.</param>
        public void EnsureCapacity(int minCapacity)
        {

            int oldCapacity = data.Length;

            if (oldCapacity >= minCapacity)
                return;

            int[] oldData = data;

            // increment the oldCapacity by 1.5 times atleast
            int newCapacity = (oldCapacity * 3) / 2 + 1;

            if (newCapacity < minCapacity)
                newCapacity = minCapacity;

            data = new int[newCapacity];
            System.Array.Copy(oldData, 0, data, 0, size);
        }

        public int GetCapacity()
        {
            return data.Length;
        }
        /// <summary>
        /// Clears all elements in the array. Size is set to zero.
        /// </summary>
        public void Clear()
        {
            size = 0;
        }

        /// <summary>
        /// Returns the size of the array.
        /// </summary>
        /// <returns></returns>
        public override int GetSize()
        {
            return size;
        }

        /// <summary>
        /// Returns element at the specified index.
        /// The <code>index</code> should be strictly less then maxSize of the array.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public override int Get(int index)
        {
            if (index > size)
                return -1;
            return data[index];
        }

        /// <summary>
        /// Replaces the element at the specified position in this array with the specified element.
        /// <code>index</code> should be strictly less than the size of the array.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="element"></param>
        public void Set(int index, int element)
        {
            EnsureCapacity(index + 1);
            data[index] = element;
            size = Math.Max(size, index + 1);
        }

        /// <summary>
        /// Appends the specified element at the end of this array.
        /// </summary>
        /// <param name="element"></param>
        public void Add(int element)
        {
            EnsureCapacity(size + 1);
            data[size++] = element;
        }

        /// <summary>
        /// Appends all the elements in the specified array at the 
        /// end of this array.
        /// </summary>
        /// <param name="array"></param>
        public void Add(int[] array)
        {
            EnsureCapacity(size + array.Length);

            for (int i = 0; i < array.Length; i++)
                data[size++] = array[i];
        }

        /// <summary>
        /// Appends all the elements in the specified array at the 
        /// end of this array.
        /// </summary>
        /// <param name="array"></param>
        public void Add(IntArray array)
        {
            int arraySize = array.GetSize();

            EnsureCapacity(size + arraySize);

            for (int i = 0; i < arraySize; i++)
                data[size++] = array.Get(i);
        }

        public int RemoveElementAt(int index)
        {
            int value = data[index];

            int numMoved = size - index - 1;
            if (numMoved > 0)
                System.Array.Copy(data, index + 1, data, index, numMoved);

            data[--size] = 0; // just cleaning up

            return value;
        }

        public new IntIterator Iterator()
        {
            return ArrayUtil.CreateIntIterator(data, size);
        }



    }
}
