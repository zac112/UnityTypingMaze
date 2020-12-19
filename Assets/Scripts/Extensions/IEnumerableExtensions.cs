using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class IEnumerableExtensions
{
	public static T GetRandomValue<T>(this IEnumerable<T> enumerable)
	{
		return enumerable.ElementAt(Random.Range(0, enumerable.Count()));
	}
}