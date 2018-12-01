using System;
using System.Collections.Generic;

namespace Codes.Linus.IntVectors.Collections
{
	public interface IDataMatrix3<T> : IEnumerable<T>
	{
		int Count
		{
			get;
		}

		Vector3i Size
		{
			get;
		}

		bool IsReadOnly
		{
			get;
		}

		T this [int x, int y, int z]
		{
			get;
			set;
		}
		
		T this [Vector3i index]
		{
			get;
			set;
		}

		Vector3i IndexToPosition (int index);
		
		int PositionToIndex (Vector3i position);

		void Resize (Vector3i newSize, T padding = default(T));

		void Clear();

		bool Contains (T item);
	}
}

