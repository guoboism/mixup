using System;

namespace Codes.Linus.IntVectors.Extensions
{
	public static class ArrayExtensions
	{	
	// 2D arrays
		public static T GetValue<T> (this T[,] array, Vector2i index)
		{
			CheckArrayArgumentNotNull(array);

			return array[index.x, index.y];
		}
		
		public static void SetValue<T> (this T[,] array, T newValue, Vector2i index)
		{
			CheckArrayArgumentNotNull (array);

			array[index.x, index.y] = newValue;
		}

		public static Vector2i GetSize<T> (this T[,] array)
		{
			CheckArrayArgumentNotNull (array);

			return new Vector2i (
				array.GetLength (0),
				array.GetLength (1)
			);
		}

		public static T GetValue<T> (this T[][] array, Vector2i index)
		{
			CheckArrayArgumentNotNull (array);

			return array[index.x][index.y];
		}
		
		public static void SetValue<T> (this T[][] array, T newValue, Vector2i index)
		{
			CheckArrayArgumentNotNull (array);

			array[index.x][index.y] = newValue;
		}

	// 3D arrays
		public static T GetValue<T> (this T[,,] array, Vector3i index)
		{
			CheckArrayArgumentNotNull (array);

			return array[index.x, index.y, index.z];
		}

		public static void SetValue<T> (this T[,,] array, T newValue, Vector3i index)
		{
			CheckArrayArgumentNotNull (array);

			array[index.x, index.y, index.z] = newValue;
		}

		public static Vector3i GetSize<T> ( this T[,,] array)
		{
			CheckArrayArgumentNotNull (array);

			return new Vector3i (
				array.GetLength (0),
				array.GetLength (1),
				array.GetLength (2)
			);
		}

		public static T GetValue<T> (this T[][][] array, Vector3i index)
		{
			CheckArrayArgumentNotNull (array);

			return array[index.x][index.y][index.z];
		}
		
		public static void SetValue<T> (this T[][][] array,  T newValue, Vector3i index)
		{
			CheckArrayArgumentNotNull (array);

			array[index.x][index.y][index.z] = newValue;
		}
	
	// Preconditions
		private static void CheckArrayArgumentNotNull<T> (T array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
		}
	}
}

