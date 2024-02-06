using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeGeneration.ServerCodeGenerator.Model.SearchParamsTypes;
using Microsoft.CSharp;

namespace CodeGeneration.ServerCodeGenerator.Service;

internal static class Extensions
{
	private static List<Type> _boolTypes = new List<Type>()
	{
		typeof(bool), typeof(bool?)
	};
	private static List<Type> _numberTypes = new List<Type>()
	{
		typeof(int),
		typeof(double),
		typeof(decimal),
		typeof(short),
		typeof(long),
		typeof(int?),
		typeof(double?),
		typeof(decimal?),
		typeof(short?),
		typeof(long?),
	};
	private static List<Type> _stringTypes = new List<Type>()
	{
		typeof(string),
	};
	private static List<Type> _DateTimeTypes = new List<Type>()
	{
		typeof(DateTime),
		typeof(DateOnly),
		typeof(TimeOnly),
		typeof(DateTime?),
		typeof(DateOnly?),
		typeof(TimeOnly?),
	};
		internal static string ToFirstLower(this string s)
	{
		return s.Substring(0, 1).ToLowerInvariant() + s.Substring(1);
	}

	internal static string ToFirstUpper(this string s)
	{
		return s.Substring(0, 1).ToUpperInvariant() + s.Substring(1);
	}

	internal static string SplitToLines(this string s, int startingTabsCount, int maxLineWidth, string[] separators, int tabWidth = 4)
	{
		var currentTabsCount = startingTabsCount;
		var stringParts = new List<string>();
		var currentPart = new StringBuilder("");
		for (var i = 0; i < s.Length;)
		{
			var currentSeparator = separators.FirstOrDefault(separator => i + separator.Length <= s.Length
				&& s.IndexOf(separator, i, separator.Length, StringComparison.Ordinal) == i);
			if (currentSeparator == null)
			{
				currentPart.Append(s[i]);
				++i;
				continue;
			}
			currentPart.Append(currentSeparator);
			stringParts.Add(currentPart.ToString());
			currentPart.Clear();
			i += currentSeparator.Length;
		}
		if (currentPart.Length > 0)
			stringParts.Add(currentPart.ToString());
		var result = new StringBuilder("");
		for (var i = 0; i < stringParts.Count;)
		{
			var currentLine = new StringBuilder(stringParts[i]);
			var j = i + 1;
			while (j < stringParts.Count && stringParts[j].Length + currentLine.Length <= maxLineWidth - currentTabsCount * tabWidth)
			{
				currentLine.Append(stringParts[j]);
				++j;
			}
			result.Append(new string('\t', currentTabsCount));
			result.AppendLine(currentLine.ToString().Trim());
			currentLine.Clear();
			currentTabsCount = startingTabsCount + 1;
			i = j;
		}
		return result.ToString().Substring(0, result.Length - Environment.NewLine.Length);
	}

	internal static bool CanBeASearchParameter(Type type)
	{
		return _boolTypes.Contains(type) ||
			   _numberTypes.Contains(type) ||
			   _stringTypes.Contains(type) ||
			   _DateTimeTypes.Contains(type);
	}

	internal static string GetFriendlyCSharpName(this Type type, IEnumerable<string> namespacesToRemove = null)
	{
		var underlyingType = Nullable.GetUnderlyingType(type);
		if (underlyingType != null)
		{
			return underlyingType.GetFriendlyCSharpName(namespacesToRemove) + "?";
		}
		if (type.IsGenericType)
		{
			var genericArguments = type.GenericTypeArguments.Select(item => item.GetFriendlyCSharpName(namespacesToRemove));
			var genericType = type.GetGenericTypeDefinition();
			var name = genericType.Name;
			var charIndex = name.IndexOf('`');
			if (charIndex >= 0)
				name = name.Substring(0, charIndex);
			return RemoveNamespaces(genericType, name, namespacesToRemove) + "<" + string.Join(", ", genericArguments) + ">";
		}
		using (var provider = new CSharpCodeProvider())
		{
			var result = provider.GetTypeOutput(new CodeTypeReference(type));
			return RemoveNamespaces(type, result, namespacesToRemove);
		}
	}

	private static string RemoveNamespaces(Type type, string typeString, IEnumerable<string> namespacesToRemove)
	{
		var namespacesList = new List<string> { "System" };
		if (namespacesToRemove != null)
			namespacesList.AddRange(namespacesToRemove);
		if (type.Namespace != null && typeString.StartsWith(type.Namespace) && namespacesList.Contains(type.Namespace))
			typeString = typeString.Substring(type.Namespace.Length + 1);
		return typeString;
	}

	internal static bool IsNullable(this Type type)
	{
		return Nullable.GetUnderlyingType(type) != null;
	}

	internal static Type GetNonNullableType(this Type type)
	{
		var result = Nullable.GetUnderlyingType(type);
		if (result == null)
			result = type;
		return result;
	}

	internal static string GetSearchParameterProperty(Type type, string name)
	{
		return GetSearchParameterType(type, name).GetSearchParameters();
	}

	internal static string GetSearchParameterQueryFilter(Type type, string name)
	{
		return GetSearchParameterType(type, name).GetQueryFilter();
	}

	private static SearchParamGenerator GetSearchParameterType(Type type, string name)
	{
		if (_boolTypes.Contains(type))
		{
			return new BoolSearchParamGenerator(name, type);
		}
		if (_numberTypes.Contains(type))
		{
			return new NumberSearchParamGenerator(name, type);
		}
		if (_stringTypes.Contains(type))
		{
			return new StringSearchParamGenerator(name, type);
		}
		if (_DateTimeTypes.Contains(type))
		{
			return new DateTimeSearchParamGenerator(name, type);
		}
		throw new Exception($"there is no searchParameterGenerator for type {type.Name}");
	}
}
