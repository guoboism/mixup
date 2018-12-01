using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Codes.Linus.IntVectors.Collections
{
	[Serializable]
	public sealed class DataMatrix2<T> : IDataMatrix2<T>
	{
		private List<T> rawData;

		public DataMatrix2 ()
		{
			rawData = new List<T> ();
		}

		public DataMatrix2 (Vector2i size, T padding = default(T))
		{
			rawData = new List<T> ();
			Resize (size, padding);
		}                 

		public int Count
		{
			get { return Size.x * Size.y; }
		}
		
		public Vector2i Size
		{
			get;
			private set;
		}
		
		public bool IsReadOnly
		{
			get { return false; }
		}

		public T this [int x, int y]
		{
			get { return rawData [PositionToIndex(new Vector2i(x,y))];  }
			set { rawData [PositionToIndex(new Vector2i(x,y))] = value; }
		}
		
		public T this [Vector2i index]
		{
			get { return this [index.x, index.y];  }
			set { this [index.x, index.y] = value; }
		}

		public void Resize (Vector2i newSize, T padding = default(T))
		{
			this.Size = newSize;
			Resize (rawData, Size.x * Size.y, padding);
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return rawData.GetEnumerator ();
		}

		public IEnumerator<T> GetEnumerator ()
		{
			return rawData.GetEnumerator ();
		}
		
		public void Clear()
		{
			this.rawData.Clear ();
			Resize (Vector2i.zero);
		}

		public bool Contains (T item)
		{
			return rawData.Contains (item);
		}

		public Vector2i IndexToPosition (int index)
		{
			return new Vector2i (
				index % Size.x, 
				(index / Size.x) % Size.y
			);
		}

		public int PositionToIndex (Vector2i position)
		{
			return position.x + 
				   position.y * Size.x;
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

