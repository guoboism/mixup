using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Codes.Linus.IntVectors.Collections
{
	[Serializable]
	public sealed class DataMatrix3<T>: IDataMatrix3<T>
	{
		private List<T> rawData;

		public DataMatrix3 ()
		{
			rawData = new List<T> ();
		}

		public DataMatrix3 (Vector3i size, T padding = default(T))
		{
			rawData = new List<T> ();
			Resize (size, padding);
		}                 

		public int Count
		{
			get { return Size.x * Size.y * Size.z; }
		}
		
		public Vector3i Size
		{
			get;
			private set;
		}
		
		public bool IsReadOnly
		{
			get { return false; }
		}

		public T this [int x, int y, int z]
		{
			get { return rawData [PositionToIndex(new Vector3i(x,y,z))];  }
			set { rawData [PositionToIndex(new Vector3i(x,y,z))] = value; }
		}
		
		public T this [Vector3i index]
		{
			get { return this [index.x, index.y, index.z];  }
			set { this [index.x, index.y, index.z] = value; }
		}

		public void Resize (Vector3i newSize, T padding = default(T))
		{
			this.Size = newSize;
			Resize (rawData, Size.x * Size.y * Size.z, padding);
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return rawData.GetEnumerator ();
		}

		public IEnumerator<T> GetEnumerator ()
		{
			return rawData.GetEnumerator ();
		}
		
		public void Clear ()
		{
			this.rawData.Clear ();
			Resize (Vector3i.zero);
		}
		
		public bool Contains (T item)
		{
			return rawData.Contains (item);
		}

		public Vector3i IndexToPosition (int index)
		{
			return new Vector3i (
				index % Size.x, 
				(index / Size.x) % Size.y,
				index / (Size.x * Size.y)
			);
		}

		public int PositionToIndex (Vector3i position)
		{
			return position.x + 
				   position.y * Size.x + 
				   position.z * Size.x * Size.y;
		}

		private static void Resize (List<T> list, int size, T padding = default (T)) 
		{
			if (list.Count > size)
			{
				list.RemoveRange (size, list.Count - size);
			} else 
			{
				if (list.Capacity > size)
				{
					list.Capacity = size;
				}
				
				list.AddRange (Enumerable.Repeat (padding, size - list.Count));
			}
		}
	}
}

