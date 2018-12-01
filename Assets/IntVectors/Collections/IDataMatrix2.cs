using System;
using System.Collections.Generic;

namespace Codes.Linus.IntVectors.Collections
{
	public interface IDataMatrix2<T> : IEnumerable<T>
	{
		int Count
		{
			get;
		}

		Vector2i Size
		{
			get;
		}

		bool IsReadOnly
		{
			get;
		}

		T this [int x, int y]
		{
			get;
			set;
		}
		
		T this [Vector2i index]
		{
			get;
			set;
		}

		Vector2i IndexToPosition (int index);
		
		int PositionToIndex (Vector2i position);

		void Resize (Vector2i newSize, T padding = default(T));

		void Clear();

		bool Contains (T item);
	}
}

