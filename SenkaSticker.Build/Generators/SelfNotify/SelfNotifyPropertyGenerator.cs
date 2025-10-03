using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SenkaSticker.Build.Attributes;
using SenkaSticker.Build.Consts;
using System;
using System.Collections;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;

namespace SenkaSticker.Build.Generators.SelfNotify;

[Generator]
public class SelfNotifyPropertyGenerator : IIncrementalGenerator
{
    #region Methods
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var classes = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: (syntaxNode, _) => IsTargetClass(syntaxNode),
            transform: (syntaxContext, _) => GetClassInfo(syntaxContext))
            .Where(item =>
            !string.IsNullOrEmpty(item.Namespace)
            && !string.IsNullOrEmpty(item.ClassName)
            && !string.IsNullOrEmpty(item.Namespace)
            && item.PropertyInfos != null
            && item.ClassDeclaration != null);

        context.RegisterSourceOutput(classes,
            (spc, source) => GenerateCode(source, spc));
    }

    private bool IsTargetClass(SyntaxNode syntaxNode)
    {
        if (syntaxNode is not ClassDeclarationSyntax classDeclarationSyntax)
        {
            return false;
        }
        else
        {
            return classDeclarationSyntax
                .AttributeLists.Any(attrList => attrList.Attributes.Any(attr =>
                IsTargetAttr(attr.Name.ToString())
                ));
        }
    }

    private ClassInfo GetClassInfo(GeneratorSyntaxContext context)
    {
        var classDecl = (ClassDeclarationSyntax)context.Node;
        var semanticModel = context.SemanticModel;

        var classSymbol = semanticModel.GetDeclaredSymbol(classDecl) as INamedTypeSymbol;
        if (classSymbol == null)
        {
            return null;
        }

        var namespaceName = classSymbol.ContainingNamespace.ToString();
        var className = classSymbol.Name;
        var toIncludeProperties = new List<(string, string, string)>();

        foreach (var attr in classSymbol.GetAttributes())
        {
            if (IsTargetAttr(attr.AttributeClass.Name))
            {
                var propertyTyperg = attr.ConstructorArguments[0].Value as ITypeSymbol;
                var propertyTypeName = propertyTyperg.Name;
                var propertyDefinedNamespace = propertyTyperg.ContainingNamespace.ToString();
                var propertyName = attr.ConstructorArguments[1].Value as string;
                toIncludeProperties.Add((propertyTypeName, propertyDefinedNamespace, propertyName));
            }
        }

        return new ClassInfo(classDecl, namespaceName, className, toIncludeProperties);
    }

    private void GenerateCode(ClassInfo info, SourceProductionContext context)
    {
        if (info == null)
        {
            return;
        }

        var codeSourcePath = info.ClassDeclaration.SyntaxTree.FilePath;
        var headerCode = new StringBuilder();
        var content = new StringBuilder();
        headerCode.AppendLine($"namespace {info.Namespace};");
        headerCode.AppendLine("using Caliburn.Micro;");
        headerCode.AppendLine("using System;");
        headerCode.AppendLine("using System.Collections;");
        headerCode.AppendLine("using System.Linq;");

        content.AppendLine($"public partial class {info.ClassName}");
        content.AppendLine("{");
        foreach (var property in info.PropertyInfos)
        {
            var typeName = property.Item1;
            var typeNamespace = property.Item2;
            var propertyName = property.Item3;
            var fieldName = $"_{char.ToLower(propertyName[0])}{propertyName.Substring(1)}";
            
            headerCode.AppendLine($"using {typeNamespace};");
            
            content.AppendLine($"  private {typeName} {fieldName};");
            content.AppendLine($"  public {typeName} {propertyName}");
            content.AppendLine("   {");
            content.AppendLine($"      get => {fieldName};");
            content.AppendLine("       set");
            content.AppendLine("      {");
            content.AppendLine($"          if ({fieldName} != value)");
            content.AppendLine("           {");
            content.AppendLine($"              {fieldName} = value;");
            content.AppendLine($"              NotifyOfPropertyChange(nameof({propertyName}));");
            content.AppendLine("           }");
            content.AppendLine("      }");
            content.AppendLine("   }");
        }
        content.AppendLine("}");
        headerCode.Append(content);

        context.AddSource(
            hintName: $"{info.ClassName}.g.cs",
            sourceText: SourceText.From(headerCode.ToString(), Encoding.UTF8));
    }

    private bool IsTargetAttr(string attrName)
    {
        return attrName == nameof(IncludeSelfNotifyPropertyAttribute)
            || attrName == nameof(IncludeSelfNotifyPropertyAttribute).Replace("Attribute", "");
    }
    #endregion

    private class ClassInfo
    {
        public ClassInfo(
            ClassDeclarationSyntax classDeclarationSyntax,
            string namespaceName,
            string className,
            List<(string, string, string)> properties)
        {
            ClassDeclaration = classDeclarationSyntax;
            Namespace = namespaceName;
            ClassName = className;
            PropertyInfos = properties;
        }

        public ClassDeclarationSyntax ClassDeclaration { get; }
        public string Namespace { get; }
        public string ClassName { get; }
        public List<(string, string, string)> PropertyInfos { get; }
    }
}
