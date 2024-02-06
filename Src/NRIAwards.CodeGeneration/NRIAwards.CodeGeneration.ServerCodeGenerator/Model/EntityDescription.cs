using System.Collections.Generic;
using System.Linq;
using CodeGeneration.ServerCodeGenerator.Model.Enum;

namespace CodeGeneration.ServerCodeGenerator.Model;

internal class EntityDescription
{
	internal string Name { get; set; }
	internal string PluralName { get; set; }
	internal GeneratedFiles Files { get; set; }
	internal IList<PropertyDescription> Properties { get; set; }

	internal EntityDescription ExcludeNewProperties()
	{
		return new EntityDescription
		{
			Name = Name,
			PluralName = PluralName,
			Properties = Properties.Where(item => !item.IsNew).ToList()
		};
	}
}