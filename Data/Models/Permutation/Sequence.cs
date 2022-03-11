using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Sequence
    {
        public int[] Indexes { get; set; }

        public Sequence(int[] array)
        {
            if (array.Length <= 1) throw new ArgumentOutOfRangeException("array length invlaid");
            if (!isValid(array)) throw new ArgumentException("invalid array values");
            Indexes = array;
        }

        private bool isValid(int[] array)
        {
            bool[] indexesCovered = new bool[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] >= array.Length || array[i] < 0) return false;
                if (indexesCovered[array[i]]) return false;
                indexesCovered[array[i]] = true;
            }
            return indexesCovered.All(val => val);
        }

        public bool Valid()
        {
            return isValid(Indexes);
        }

        public object[] ApplyTo(object[] items)
        {
            if (Indexes.Length != items.Length) throw new ArgumentOutOfRangeException();

            object[] modifiedSequence = new object[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                modifiedSequence[Indexes[i]] = items[i];
            }

            return modifiedSequence;
        }
    }
}