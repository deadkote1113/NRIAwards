using System;

namespace CodeGeneration.ServerCodeGenerator.Model.SearchParamsTypes;

internal class BoolSearchParamGenerator : SearchParamGenerator
{
	public BoolSearchParamGenerator(string entityName, Type entityType) : base(entityName, entityType)
	{
	}

	private string SearchParameterName => $"Is{EntityName}";
	private string SearchParameterType => $"bool?";

	internal override string GetQueryFilter()
	{
		return StringWrapper($"if(searchParams.{SearchParameterName}.HasValue)") +
			   StringWrapper($"{{") +
			   StringWrapper($"\tdbObjects = dbObjects.Where(item => item.{EntityName} == searchParams.{SearchParameterName});{EndOfLine}") +
			   StringWrapper($"}}");
	}

	internal override string GetSearchParameters()
	{
		return $"public {SearchParameterType} {SearchParameterName} {{ get; set; }}";
	}
}