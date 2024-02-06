using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGeneration.ServerCodeGenerator.Service;

namespace CodeGeneration.ServerCodeGenerator.Model.SearchParamsTypes;

internal class DateTimeSearchParamGenerator : SearchParamGenerator
{
	public DateTimeSearchParamGenerator(string entityName, Type entityType) : base(entityName, entityType)
	{
	}

	private string SearchParameterType => EntityType.IsNullable() ?
		EntityType.GetFriendlyCSharpName(UsedNamespaces)
		:
		$"{EntityType.GetFriendlyCSharpName(UsedNamespaces)}?";

	internal override string GetQueryFilter()
	{
		return StringWrapper($"if(searchParams.From{EntityName}.HasValue)") +
			   StringWrapper($"{{") +
			   StringWrapper($"\tdbObjects = dbObjects.Where(item => item.{EntityName} >= searchParams.From{EntityName});") +
			   StringWrapper($"}}") +
			   StringWrapper($"if(searchParams.To{EntityName}.HasValue)") +
			   StringWrapper($"{{") +
			   StringWrapper($"\tdbObjects = dbObjects.Where(item => item.{EntityName} <= searchParams.To{EntityName});") +
			   StringWrapper($"}}");
	}

	internal override string GetSearchParameters()
	{
		return $"public {SearchParameterType} From{EntityName} {{ get; set; }}{EndOfLine}" +
			   $"\tpublic {SearchParameterType} To{EntityName} {{ get; set; }}";
	}
}