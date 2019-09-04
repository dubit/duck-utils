namespace DUCK.Utils
{
	public static class IndexUtil
	{
		/// <summary>
		/// Increments the target value by one and returns the result.
		/// It will not exceed the maximum value given
		/// </summary>
		/// <param name="value">The target (current) index</param>
		/// <param name="max">The maximum valid value (exclusive) of the index</param>
		/// <param name="wrapAround">
		/// Declares the behaviour when the index reaches the maximum.
		/// If set to true then the value will return to 0 and wrap around.
		/// If set to false it will not exceed the maximum.
		/// Defaults to true
		/// </param>
		/// <returns>The new index</returns>
		public static int Increment(this int value, int max, bool wrapAround = true)
		{
			var newIndex = value + 1;
			newIndex = newIndex >= max ? (wrapAround ? 0 : value) : newIndex;
			return newIndex;
		}

		/// <summary>
		/// Decrements the target value by one and returns the result.
		/// It will go below 0.
		/// </summary>
		/// <param name="value">The target (current) index</param>
		/// <param name="max">The maximum valid value (exclusive) of the index</param>
		/// <param name="wrapAround">
		/// Declares the behaviour when the index goes below 0.
		/// If set to true then the value will return to max - 1 and wrap around.
		/// If set to false it will not go below 0.
		/// Defaults to true
		/// </param>
		/// <returns>The new index</returns>
		public static int Decrement(this int value, int max, bool wrapAround = true)
		{
			var newIndex = value - 1;
			newIndex = newIndex < 0 ? (wrapAround ? max - 1 : value) : newIndex;
			return newIndex;
		}
	}
}