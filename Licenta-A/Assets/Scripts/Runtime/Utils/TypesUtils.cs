using System;
using System.Collections.Generic;

namespace AF
{
	public static class TypesUtils
	{
		/// <summary>
		/// Returns all types in the executing assemblies that meet the specified condition
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static IEnumerable<Type> GetTypes(Func<Type, bool> predicate)
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (var assembly in assemblies)
			{
				foreach (var assemblyType in assembly.GetTypes())
				{
					if (predicate(assemblyType))
					{
						yield return assemblyType;
					}
				}
			}
		}
	}
}
