#region References
using System.Collections.Generic;
#endregion

public static class Property
{
	public static bool Set<T> (ref T field, T value)
	{
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		return true;
	}
}
