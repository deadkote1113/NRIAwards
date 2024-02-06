using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.ServerCodeGenerator.Model.SearchParamsTypes;

internal abstract class SearchParamGenerator
{
	protected const string EndOfLine = "\r\n";
	protected const string QueryFilterIdent = "\t\t\t";
	protected readonly List<string> UsedNamespaces = new() { "Common.Enums", "System.Collections.Generic" };
	protected readonly string EntityName;
	protected readonly Type EntityType;

	protected SearchParamGenerator(string entityName, Type entityType)
	{
		EntityName = entityName;
		EntityType = entityType;
	}

	internal abstract string GetQueryFilter();
	internal abstract string GetSearchParameters();

	protected string StringWrapper(string value)
	{
		return $"{QueryFilterIdent}{value}{EndOfLine}";
	}
}