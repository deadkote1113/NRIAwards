using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using CodeGeneration.ServerCodeGenerator.Model;
using CodeGeneration.ServerCodeGenerator.Template;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CodeGeneration.ServerCodeGenerator.Model.Enum;
using CodeGeneration.ServerCodeGenerator.Service.MergeUtility;
using NRIAwards.DAL.Context;

namespace CodeGeneration.ServerCodeGenerator.Service;

internal class CodeGenerator
{
	private readonly ILogger<CodeGenerator> _logger;
	private readonly IMergeUtility _mergeUtility;
	private readonly string _solutionFolderPath;
	private readonly ExistingFilesProcessMode _existingFilesProcessMode;
	private readonly IList<EntityDescription> _entities;
	private readonly int _maxLineWidth;
	private readonly PostgresDbContext _dbContext;
	private readonly List<PropertyDescription> _baseProperties;

	public CodeGenerator(ILogger<CodeGenerator> logger, PostgresDbContext dbContext, IConfiguration configuration, IMergeUtility mergeUtility)
	{
		_logger = logger;
		_dbContext = dbContext;
		_mergeUtility = mergeUtility;
		var config = new XmlDocument();
		config.Load(configuration.GetSection("configPath").Get<string>());
		var rootNode = config.DocumentElement;
		_entities = new List<EntityDescription>();
		_baseProperties = new()
		{
			new()
			{
				EntityName = "Id",
				DbName = "Id",
				EntityType = typeof(int),
				DbType = typeof(int),
			},
			new()
			{
				EntityName = "CreatedAt",
				DbName = "CreatedAt",
				EntityType = typeof(DateTime),
				DbType = typeof(DateTime),
			},
			new()
			{
				EntityName = "UpdatedAt",
				DbName = "UpdatedAt",
				EntityType = typeof(DateTime),
				DbType = typeof(DateTime),
			},
			new()
			{
				EntityName = "DeletedAt",
				DbName = "DeletedAt",
				EntityType = typeof(DateTime?),
				DbType = typeof(DateTime?),
			}
		};
		foreach (XmlNode node in rootNode.ChildNodes)
		{
			switch (node.Name)
			{
				case "generationSettings":
					_solutionFolderPath = node.Attributes["solutionFolderPath"]?.Value;
					_existingFilesProcessMode = System.Enum.TryParse(node.Attributes["existingFilesProcessMode"]?.Value,
						out ExistingFilesProcessMode processMode) ? processMode : ExistingFilesProcessMode.Skip;
					_maxLineWidth = int.TryParse(node.Attributes["maxLineWidth"]?.Value, out int maxLineWidth) ? maxLineWidth : 120;
					_maxLineWidth = Math.Max(1, _maxLineWidth);
					break;
				case "entities":
					foreach (XmlNode entityNode in node.SelectNodes("entity"))
					{
						var entity = ReadEntity(entityNode);
						if (entity != null)
						{
							_entities.Add(entity);
						}
					}
					break;
			}
		}
	}

	internal void Generate()
	{
		var commonProject = new MicrosoftBuildProject(Path.Combine(_solutionFolderPath, "Common\\Common.Entity\\Common.Entity.csproj"));
		GenerateCommonEntities(commonProject);

		var baseDalProject = new MicrosoftBuildProject(Path.Combine(_solutionFolderPath, "DalLevel\\Dal.Base\\Dal.Base.csproj"));
		GenerateBaseDalProject(baseDalProject);

		var dalProject = new MicrosoftBuildProject(Path.Combine(_solutionFolderPath, "DalLevel\\Dal.Repository\\Dal.Repository.csproj"));
		GenerateRepositiryProject(dalProject);

		var baseBlProject = new MicrosoftBuildProject(Path.Combine(_solutionFolderPath, "BlLevel\\Bl.Base\\Bl.Base.csproj"));
		GenerateBaseBlProject(baseBlProject);

		var blProject = new MicrosoftBuildProject(Path.Combine(_solutionFolderPath, "BlLevel\\Bl.Service\\Bl.Service.csproj"));
		GenerateServiceProject(blProject);

		//var project = new MicrosoftBuildProject(Path.Combine(_solutionFolderPath, "UI\\UI.csproj"));
		//GenerateModels(project);
		//GenerateControllers(project);
		//GenerateViews(project);
	}

	private void GenerateCommonEntities(MicrosoftBuildProject project)
	{
		foreach (var description in _entities)
		{
			if ((description.Files & GeneratedFiles.Entity) == GeneratedFiles.None)
			{
				continue;
			}

			var entityFileName = $"{description.Name}.cs";
			var entityItem = new MicrosoftBuildProject.Item(entityFileName, "Compile");
			CreateFileInProject(project, entityItem,
				new EntityTemplate(description.ExcludeNewProperties(), _maxLineWidth, _baseProperties).TransformText(),
				new EntityTemplate(description, _maxLineWidth, _baseProperties).TransformText(),
				$"Генерация сущности {entityFileName}...");

			var searchParamsFileName = $"Search/{description.PluralName}SearchParams.cs";
			var searchParamsItem = new MicrosoftBuildProject.Item(searchParamsFileName, "Compile");
			CreateFileInProject(project, searchParamsItem,
				new SearchParamsTemplate(description.ExcludeNewProperties(), _baseProperties).TransformText(),
				new SearchParamsTemplate(description, _baseProperties).TransformText(),
				$"Генерация сущности {searchParamsFileName}...");

			var orderParamsFileName = $"Order/{description.PluralName}OrderParams.cs";
			var orderParamsItem = new MicrosoftBuildProject.Item(orderParamsFileName, "Compile");
			CreateFileInProject(project, orderParamsItem,
				new OrderParamsTemplate(description.ExcludeNewProperties()).TransformText(),
				new OrderParamsTemplate(description).TransformText(),
				$"Генерация сущности {orderParamsFileName}...");

			var includeParamsFileName = $"Include/{description.PluralName}IncludeParams.cs";
			var includeParamsItem = new MicrosoftBuildProject.Item(includeParamsFileName, "Compile");
			CreateFileInProject(project, includeParamsItem,
				new IncludeParamsTemplate(description.ExcludeNewProperties()).TransformText(),
				new IncludeParamsTemplate(description).TransformText(),
				$"Генерация сущности {includeParamsFileName}...");
		}
	}

	private void GenerateBaseDalProject(MicrosoftBuildProject project)
	{
		foreach (var description in _entities)
		{
			if ((description.Files & GeneratedFiles.Entity) == GeneratedFiles.None)
			{
				continue;
			}

			var dalCrudInterfaseFileName = $"Interface/Crud/ICrud{description.PluralName}Repository.cs";
			var dalCrudInterfaseItem = new MicrosoftBuildProject.Item(dalCrudInterfaseFileName, "Compile");
			CreateFileInProject(project, dalCrudInterfaseItem,
				new DALCrudInterfaseTemplate(description.ExcludeNewProperties()).TransformText(),
				new DALCrudInterfaseTemplate(description).TransformText(),
				$"Генерация сущности {dalCrudInterfaseFileName}...");

			var dalExtendedInterfaseFileName = $"Interface/Extended/IExtended{description.PluralName}Repository.cs";
			var dalExtendedInterfaseItem = new MicrosoftBuildProject.Item(dalExtendedInterfaseFileName, "Compile");
			CreateFileInProject(project, dalExtendedInterfaseItem,
				new DALExtendedInterfaseTemplate(description.ExcludeNewProperties()).TransformText(),
				new DALExtendedInterfaseTemplate(description).TransformText(),
				$"Генерация сущности {dalExtendedInterfaseFileName}...");
		}
	}

	private void GenerateRepositiryProject(MicrosoftBuildProject project)
	{
		foreach (var description in _entities)
		{
			if ((description.Files & GeneratedFiles.Entity) == GeneratedFiles.None)
			{
				continue;
			}

			var dalRepositiryFileName = $"{description.PluralName}Repository.cs";
			var dalRepositiryItem = new MicrosoftBuildProject.Item(dalRepositiryFileName, "Compile");
			CreateFileInProject(project, dalRepositiryItem,
				new RepositoryTemplate(description.ExcludeNewProperties(), _maxLineWidth, _baseProperties).TransformText(),
				new RepositoryTemplate(description, _maxLineWidth, _baseProperties).TransformText(),
				$"Генерация сущности {dalRepositiryFileName}...");
		}
	}

	private void GenerateBaseBlProject(MicrosoftBuildProject project)
	{
		foreach (var description in _entities)
		{
			if ((description.Files & GeneratedFiles.Entity) == GeneratedFiles.None)
			{
				continue;
			}

			var blCrudInterfaseFileName = $"Interface/Crud/ICrud{description.PluralName}Service.cs";
			var blCrudInterfaseItem = new MicrosoftBuildProject.Item(blCrudInterfaseFileName, "Compile");
			CreateFileInProject(project, blCrudInterfaseItem,
				new BLCrudInterfaseTemplate(description.ExcludeNewProperties()).TransformText(),
				new BLCrudInterfaseTemplate(description).TransformText(),
				$"Генерация сущности {blCrudInterfaseFileName}...");

			var blExtendedInterfaseFileName = $"Interface/Extended/IExtended{description.PluralName}Service.cs";
			var blExtendedInterfaseItem = new MicrosoftBuildProject.Item(blExtendedInterfaseFileName, "Compile");
			CreateFileInProject(project, blExtendedInterfaseItem,
				new BLExtendedInterfaseTemplate(description.ExcludeNewProperties()).TransformText(),
				new BLExtendedInterfaseTemplate(description).TransformText(),
				$"Генерация сущности {blExtendedInterfaseFileName}...");
		}
	}

	private void GenerateServiceProject(MicrosoftBuildProject project)
	{
		foreach (var description in _entities)
		{
			if ((description.Files & GeneratedFiles.Entity) == GeneratedFiles.None)
			{
				continue;
			}

			var blServiceFileName = $"{description.PluralName}Service.cs";
			var blServiceItem = new MicrosoftBuildProject.Item(blServiceFileName, "Compile");
			CreateFileInProject(project, blServiceItem,
				new ServiceTemplate(description.ExcludeNewProperties()).TransformText(),
				new ServiceTemplate(description).TransformText(),
				$"Генерация сущности {blServiceFileName}...");
		}
	}

	private void GenerateModels(MicrosoftBuildProject project)
	{
		foreach (var description in _entities)
		{
			if ((description.Files & GeneratedFiles.Model) == GeneratedFiles.None)
				continue;
			var fileName = description.Name + "Model.cs";
			var item = new MicrosoftBuildProject.Item($"Areas\\Admin\\Models\\{fileName}", "Compile");
			CreateFileInProject(project, item,
				new ModelTemplate(description.ExcludeNewProperties(), _maxLineWidth).TransformText(),
				new ModelTemplate(description, _maxLineWidth).TransformText(),
				$"Генерация модели {fileName}...");
		}
	}

	private void GenerateControllers(MicrosoftBuildProject project)
	{
		foreach (var description in _entities)
		{
			if ((description.Files & GeneratedFiles.Controller) == GeneratedFiles.None)
				continue;
			var fileName = description.PluralName + "Controller.cs";
			var item = new MicrosoftBuildProject.Item($"Areas\\Admin\\Controllers\\{fileName}", "Compile");
			CreateFileInProject(project, item,
				new ControllerTemplate(description.ExcludeNewProperties(), _maxLineWidth).TransformText(),
				new ControllerTemplate(description, _maxLineWidth).TransformText(),
				$"Генерация контроллера {fileName}...");
		}
	}

	private void GenerateViews(MicrosoftBuildProject project)
	{
		foreach (var description in _entities)
		{
			if ((description.Files & GeneratedFiles.ViewIndex) == GeneratedFiles.ViewIndex)
			{
				var fileName = "Index.cshtml";
				var item = new MicrosoftBuildProject.Item($"Areas\\Admin\\Views\\{description.PluralName}\\{fileName}", "Content");
				CreateFileInProject(project, item,
					new ViewIndexTemplate(description.ExcludeNewProperties(), _maxLineWidth).TransformText(),
					new ViewIndexTemplate(description, _maxLineWidth).TransformText(),
					$"Генерация представления {description.PluralName}\\{fileName}...");
			}
			if ((description.Files & GeneratedFiles.ViewUpdate) == GeneratedFiles.ViewUpdate)
			{
				var fileName = "Update.cshtml";
				var item = new MicrosoftBuildProject.Item($"Areas\\Admin\\Views\\{description.PluralName}\\{fileName}", "Content");
				CreateFileInProject(project, item,
					new ViewUpdateTemplate(description.ExcludeNewProperties(), _maxLineWidth).TransformText(),
					new ViewUpdateTemplate(description, _maxLineWidth).TransformText(),
					$"Генерация представления {description.PluralName}\\{fileName}...");
			}
		}
	}

	private void CreateFileInProject(MicrosoftBuildProject project, MicrosoftBuildProject.Item item, string oldFileContent, string currentFileContent, string infoMessage)
	{
		_logger.LogInformation(infoMessage);
		var projectDirectory = new FileInfo(project.Path).Directory.FullName;
		var filePath = Path.Combine(projectDirectory, item.Path);
		new FileInfo(filePath).Directory.Create();
		project.AddItem(item);
		var fileContent = currentFileContent;
		if (!File.Exists(filePath) || _existingFilesProcessMode != ExistingFilesProcessMode.Skip)
		{
			if (File.Exists(filePath) && _existingFilesProcessMode == ExistingFilesProcessMode.Merge && _mergeUtility != null)
			{
				var generatedFilePath = filePath + ".generated.tmp";
				File.WriteAllText(generatedFilePath, currentFileContent, Encoding.UTF8);
				var oldFilePath = filePath + ".old.tmp";
				File.WriteAllText(oldFilePath, oldFileContent, Encoding.UTF8);
				var mergedFilePath = filePath + ".merged.tmp";
				File.Delete(mergedFilePath);
				_mergeUtility.PerformMerge(oldFilePath, generatedFilePath, filePath, mergedFilePath);
				if (File.Exists(mergedFilePath))
				{
					var sourceContent = File.ReadAllText(filePath, Encoding.UTF8);
					var mergedContent = File.ReadAllText(mergedFilePath, Encoding.UTF8);
					_logger.LogInformation(sourceContent != mergedContent
						? "Выполняем объединение изменений."
						: "Файл не изменен.");
					fileContent = mergedContent;
					File.Delete(mergedFilePath);
				}
				else
				{
					_logger.LogInformation("Объединение отменено, используем новый файл.");
				}
				File.Delete(generatedFilePath);
				File.Delete(oldFilePath);
			}
			File.WriteAllText(filePath, fileContent, Encoding.UTF8);
		}
		else
		{
			_logger.LogInformation("Файл существует, пропускаем.");
		}
	}

	private EntityDescription ReadEntity(XmlNode node)
	{
		var entityName = node.Attributes?["name"].Value;
		if (string.IsNullOrEmpty(entityName))
		{
			_logger.LogInformation("Не указано имя сущности");
			return null;
		}
		var efType = _dbContext.Model.FindEntityType("Dal.Context.Model." + entityName);
		if (efType == null)
		{
			_logger.LogInformation($"Не удалось загрузить метаданные о типе {entityName} из модели EF");
			return null;
		}
		var entity = new EntityDescription
		{
			Name = entityName,
			Properties = new List<PropertyDescription>(),
			PluralName = efType.GetTableName(),
			Files = GeneratedFiles.All
		};
		var filesNodes = node.SelectSingleNode("files")?.SelectNodes("file");
		if (filesNodes != null)
		{
			var excludedFiles = GeneratedFiles.None;
			foreach (XmlNode fileNode in filesNodes)
			{
				if (!System.Enum.TryParse(fileNode?.Attributes?["type"].Value, out GeneratedFiles fileType))
					continue;
				if (bool.TryParse(fileNode.InnerText, out bool nodeValue))
				{
					if (!nodeValue)
						excludedFiles |= fileType;
				}
			}
			entity.Files ^= excludedFiles;
		}
		var propertiesNodes = node.SelectSingleNode("properties")?.SelectNodes("property")?.Cast<XmlNode>()
			.ToDictionary(item => item.Attributes["dbName"].Value, item => item);
		
		var properties = efType.ClrType.GetProperties()
			.Where(item => _baseProperties.Select(item => item.EntityName).Contains(item.Name) == false);
		foreach (var property in properties)
		{
			if (!property.GetGetMethod().IsVirtual && property.GetGetMethod().IsPublic)
			{
				var propertyDescription = new PropertyDescription
				{
					DbName = property.Name,
					EntityName = property.Name,
					EntityType = property.PropertyType,
					DbType = property.PropertyType,
					IsRequired = !efType.FindProperty(property.Name).IsNullable,
					IsNew = false
				};
				if (propertiesNodes != null && propertiesNodes.ContainsKey(property.Name))
				{
					var propertyNode = propertiesNodes[property.Name];
					if (propertyNode.Attributes?["entityName"] != null)
					{
						propertyDescription.EntityName = propertyNode.Attributes?["entityName"].Value;
					}
					if (propertyNode.Attributes?["ignore"] != null)
					{
						var ignore = bool.Parse(propertyNode.Attributes?["ignore"].Value);
						if (ignore)
							continue;
					}
					if (propertyNode.Attributes?["isNew"] != null)
					{
						propertyDescription.IsNew = bool.Parse(propertyNode.Attributes?["isNew"].Value);
					}
					if (propertyNode.Attributes?["entityType"] != null)
					{
						var typeName = propertyNode.Attributes?["entityType"].Value;
						var entityPropertyType = Type.GetType(typeName);
						if (entityPropertyType == null)
						{
							_logger.LogInformation($"Не удалось загрузить тип {typeName} для свойства {property.Name} сущности {entity.Name}");
						}
						else
						{
							propertyDescription.EntityType = entityPropertyType;
						}
					}
				}
				entity.Properties.Add(propertyDescription);
			}
		}
		return entity;
	}
}