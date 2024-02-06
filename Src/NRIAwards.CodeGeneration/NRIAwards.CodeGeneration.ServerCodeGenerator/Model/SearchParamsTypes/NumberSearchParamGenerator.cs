using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGeneration.ServerCodeGenerator.Service;

namespace CodeGeneration.ServerCodeGenerator.Model.SearchParamsTypes;

internal class NumberSearchParamGenerator : SearchParamGenerator
{
	public NumberSearchParamGenerator(string entityName, Type entityType) : base(entityName, entityType)
	{
	}

	private string SearchParameterName => $"{EntityName}";
	private string SearchParameterType => EntityType.IsNullable() ?
		EntityType.GetFriendlyCSharpName(UsedNamespaces)
		:
		$"{EntityType.GetFriendlyCSharpName(UsedNamespaces)}?";

	internal override string GetQueryFilter()
	{
		return StringWrapper($"if(searchParams.{SearchParameterName}.HasValue)") +
			   StringWrapper($"{{") +
			   StringWrapper($"\tdbObjects = dbObjects.Where(item => item.{EntityName} == searchParams.{SearchParameterName});") +
			   StringWrapper($"}}");
	}

	internal override string GetSearchParameters()
	{
		return $"public {SearchParameterType} {SearchParameterName} {{ get; set; }}";
	}
}