using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.ServerCodeGenerator.Model.SearchParamsTypes;

internal class StringSearchParamGenerator : SearchParamGenerator
{
	public StringSearchParamGenerator(string entityName, Type entityType) : base(entityName, entityType)
	{
	}

	private string SearchParameterName => $"{EntityName}Like";
	private string SearchParameterType => $"string";

	internal override string GetQueryFilter()
	{
		return StringWrapper($"if(string.IsNullOrEmpty(searchParams.{SearchParameterName}) == false)") +
			   StringWrapper($"{{") +
			   StringWrapper($"\tdbObjects = dbObjects.Where(item => item.{EntityName}.ToLower().Contains(searchParams.{SearchParameterName}.ToLower()));") +
			   StringWrapper($"}}");
	}

	internal override string GetSearchParameters()
	{
		return $"public {SearchParameterType} {SearchParameterName} {{ get; set; }}";
	}
}