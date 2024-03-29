﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Версия среды выполнения: 17.0.0.0
//  
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторного создания кода.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace CodeGeneration.ServerCodeGenerator.Template
{
    using CodeGeneration.ServerCodeGenerator.Model;
    using CodeGeneration.ServerCodeGenerator.Service;
    using CodeGeneration.ServerCodeGenerator.Model.Enum;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    internal partial class RepositoryTemplate : RepositoryTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using ");
            
            #line 7 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.Name));
            
            #line default
            #line hidden
            this.Write("Entity = NRIAwards.Common.Entity.");
            
            #line 7 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.Name));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\nnamespace NRIAwards.DAL.Repository;\r\n\r\npublic class ");
            
            #line 11 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.PluralName));
            
            #line default
            #line hidden
            this.Write("Repository : BaseRepository<PostgresDbContext, ");
            
            #line 11 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.Name));
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 11 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.Name));
            
            #line default
            #line hidden
            this.Write("Entity, int, ");
            
            #line 11 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.PluralName));
            
            #line default
            #line hidden
            this.Write("SearchParams, ");
            
            #line 11 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.PluralName));
            
            #line default
            #line hidden
            this.Write("OrderParams, ");
            
            #line 11 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.PluralName));
            
            #line default
            #line hidden
            this.Write("IncludeParams>,\r\n\tICrud");
            
            #line 12 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.PluralName));
            
            #line default
            #line hidden
            this.Write("Repository, IExtended");
            
            #line 12 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.PluralName));
            
            #line default
            #line hidden
            this.Write("Repository\r\n{\r\n\r\n\tprotected internal ");
            
            #line 15 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.PluralName));
            
            #line default
            #line hidden
            this.Write("Repository(IServiceProvider serviceProvider) : base(serviceProvider)\r\n\t{\r\n\t}\r\n\r\n\t" +
                    "protected override bool RequiresUpdatesAfterObjectSaving => false;\r\n\r\n\tprotected" +
                    " override async Task UpdateBeforeSavingAsync(");
            
            #line 21 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.Name));
            
            #line default
            #line hidden
            this.Write("Entity entity, ");
            
            #line 21 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.Name));
            
            #line default
            #line hidden
            this.Write(" dbObject, bool exists)\r\n\t{\r\n\t\tawait base.UpdateBeforeSavingAsync(entity, dbObjec" +
                    "t, exists);\r\n\r\n");
            
            #line 25 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
 
var usedNamespaces = new[] { "Common.Enums", "System.Collections.Generic" };
foreach (var propertyDescription in EntityDescription.Properties)
{
if (string.CompareOrdinal(propertyDescription.DbName, "Id") != 0)
{
	if (propertyDescription.RepresentsBitMask())
	{
		var nullCondition = propertyDescription.DbType.IsNullable() ? "" : " ?? 0";

            
            #line default
            #line hidden
            this.Write("\t\tdbObject.");
            
            #line 35 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(propertyDescription.DbName));
            
            #line default
            #line hidden
            this.Write(" = entity.");
            
            #line 35 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(propertyDescription.EntityName));
            
            #line default
            #line hidden
            this.Write("?.Aggregate(0, (currentResult, item) => currentResult | (");
            
            #line 35 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(propertyDescription.DbType.GetNonNullableType().GetFriendlyCSharpName(usedNamespaces)));
            
            #line default
            #line hidden
            this.Write(") item)");
            
            #line 35 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(nullCondition));
            
            #line default
            #line hidden
            this.Write(";\r\n");
            
            #line 36 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"

	}
	else
	{
		var typeCast = !propertyDescription.EntityType.Equals(propertyDescription.DbType) ? "(" + propertyDescription.DbType.GetFriendlyCSharpName(usedNamespaces) + ")" : "";

            
            #line default
            #line hidden
            this.Write("\t\tdbObject.");
            
            #line 42 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(propertyDescription.DbName));
            
            #line default
            #line hidden
            this.Write(" = ");
            
            #line 42 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(typeCast));
            
            #line default
            #line hidden
            this.Write("entity.");
            
            #line 42 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(propertyDescription.EntityName));
            
            #line default
            #line hidden
            this.Write(";\r\n");
            
            #line 43 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"

	}
}
}

            
            #line default
            #line hidden
            this.Write("\t}\r\n\t");
            
            #line 49 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"

var queries = String.Empty;
foreach (var propertyDescription in EntityDescription.Properties.Where(item => BaseProperty.Select(property => property.EntityName).Contains(item.EntityName) == false))
{
	if (Extensions.CanBeASearchParameter(propertyDescription.EntityType) == false)
	{
		continue;
	}
	var queryFilter = Extensions.GetSearchParameterQueryFilter(propertyDescription.EntityType, propertyDescription.EntityName);
	queries += queryFilter;
}

            
            #line default
            #line hidden
            this.Write("\r\n\tprotected override IQueryable<");
            
            #line 62 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.Name));
            
            #line default
            #line hidden
            this.Write("> BuildDbQuery(IQueryable<");
            
            #line 62 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.Name));
            
            #line default
            #line hidden
            this.Write("> dbObjects, ");
            
            #line 62 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.PluralName));
            
            #line default
            #line hidden
            this.Write("SearchParams searchParams)\r\n\t{\r\n\t\tif (searchParams != null)\r\n\t\t{\r\n\t\t\tdbObjects = " +
                    "base.BuildDbQuery(dbObjects, searchParams);\r\n\t\t\tif (searchParams.Ids != null)\r\n\t" +
                    "\t\t{\r\n\t\t\t\tdbObjects = dbObjects.Where(item => searchParams.Ids.Contains(item.Id))" +
                    ";\r\n\t\t\t}\r\n");
            
            #line 71 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(queries));
            
            #line default
            #line hidden
            this.Write("        }\r\n\t\treturn dbObjects;\r\n\t}\r\n\r\n\tprotected override IQueryable<");
            
            #line 75 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.Name));
            
            #line default
            #line hidden
            this.Write("> OrderDbQuery(IQueryable<");
            
            #line 75 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.Name));
            
            #line default
            #line hidden
            this.Write("> dbObjects, ");
            
            #line 75 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.PluralName));
            
            #line default
            #line hidden
            this.Write("OrderParams orderParams)\r\n\t{\r\n\t\tdbObjects = base.OrderDbQuery(dbObjects, orderPar" +
                    "ams);\r\n\r\n\t\tif (orderParams != null)\r\n\t\t{\r\n\r\n\t\t}\r\n\r\n\t\treturn dbObjects;\r\n\t}\r\n\r\n\tp" +
                    "rotected override async Task<IList<");
            
            #line 87 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.Name));
            
            #line default
            #line hidden
            this.Write("Entity>> BuildEntitiesListAsync(IQueryable<");
            
            #line 87 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.Name));
            
            #line default
            #line hidden
            this.Write("> dbObjects, ");
            
            #line 87 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.PluralName));
            
            #line default
            #line hidden
            this.Write("IncludeParams includeParams)\r\n\t{\r\n\t\treturn (await dbObjects.ToListAsync()).Select" +
                    "(ConvertDbObjectToEntity).ToList();\r\n\t}\r\n\r\n\tinternal static ");
            
            #line 92 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.Name));
            
            #line default
            #line hidden
            this.Write("Entity ConvertDbObjectToEntity(");
            
            #line 92 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityDescription.Name));
            
            #line default
            #line hidden
            this.Write(" dbObject)\r\n\t{\r\n");
            
            #line 94 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"

var constructorParams = "";
List<PropertyDescription> allPropertyies = new();
allPropertyies.AddRange(BaseProperty);
allPropertyies.AddRange(EntityDescription.Properties);
foreach (var propertyDescription in allPropertyies)
{
	if (constructorParams != "")
	{
		constructorParams += ", ";
	}
	if (propertyDescription.RepresentsBitMask())
	{
		var enumTypeFriendlyName = propertyDescription.EntityType.GetGenericArguments()[0].GetFriendlyCSharpName(usedNamespaces);
		var nonNullableDbTypeFriendlyName = propertyDescription.DbType.GetNonNullableType().GetFriendlyCSharpName(usedNamespaces);
		var nullCondition = propertyDescription.DbType.IsNullable() ? $"dbObject.{propertyDescription.DbName} == null ? null : " : "";
		constructorParams += $"{nullCondition}typeof({enumTypeFriendlyName}).GetEnumValues().Cast<{enumTypeFriendlyName}>().Where(item => (dbObject.{propertyDescription.DbName} & ({nonNullableDbTypeFriendlyName})item) == ({nonNullableDbTypeFriendlyName})item).ToList()";
	}
	else 
	{
		var typeCast = !propertyDescription.EntityType.Equals(propertyDescription.DbType) ? "(" + propertyDescription.EntityType.GetFriendlyCSharpName(usedNamespaces) + ")" : "";
		constructorParams += typeCast + "dbObject." + propertyDescription.DbName;
	}
}

            
            #line default
            #line hidden
            
            #line 119 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture($"return new {EntityDescription.Name}Entity({constructorParams});".SplitToLines(2, MaxLineWidth, new string[] { ", " })));
            
            #line default
            #line hidden
            this.Write("\r\n\t}\r\n}\r\n\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 123 "C:\Projects\Home\NRIAwards\Src\NRIAwards.CodeGeneration\NRIAwards.CodeGeneration.ServerCodeGenerator\Template\RepositoryTemplate.tt"

	internal EntityDescription EntityDescription;
	internal int MaxLineWidth;
	internal List<PropertyDescription> BaseProperty;
	internal RepositoryTemplate(EntityDescription entityDescription, int maxLineWidth, List<PropertyDescription> baseProperty) {
		EntityDescription = entityDescription;
		MaxLineWidth = maxLineWidth;
		BaseProperty = baseProperty;
	}

        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    internal class RepositoryTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        public System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
