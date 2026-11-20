namespace SunamoWpf.Controls.ToggleSwitch.Utils;

	/// <summary>
	/// Represents the helper extensions type.
	/// </summary>
	public static class HelperExtensions
	{
		/// <summary>
		/// Clamp t.
		/// </summary>
		/// <typeparam name="T">The type T.</typeparam>
		/// <param name="val">The val.</param>
		/// <param name="min">The min.</param>
		/// <param name="max">The max.</param>
		/// <returns>The clamp t result.</returns>
		public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
		{
			return val.CompareTo(min) < 0 ? min : (val.CompareTo(max) > 0 ? max : val);
		}
	}