using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using SenkaSticker.Build.Consts;
using SenkaSticker.Build.Attributes;

namespace SenkaSticker.Build.Analyzers.SelfNotify
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SelfNotifyAnalyzer : DiagnosticAnalyzer
    {
        #region Constructor
        #endregion

        #region Events
        #endregion

        #region Fields
        private const string CATEGORY = "Usage";

        private static readonly LocalizableString _errorMessageTitle = "类必须为Partial";
        private static readonly LocalizableString _errorMessageFormat = "使用IncludeSelfNotifyPropertyAttribute特性的类{0}必须标记为partial";
        private static readonly LocalizableString _errorMessageDescription = "使用IncludeSelfNotifyPropertyAttribute特性的类必须声明为partial.";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            SenkaId.SNEKA0001,
            _errorMessageTitle,
            _errorMessageFormat,
            CATEGORY,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            _errorMessageDescription);
        #endregion

        #region Properties
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
        #endregion

        #region Methods
        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeClassDeclaration, SyntaxKind.ClassDeclaration);
        }

        private void AnalyzeClassDeclaration(SyntaxNodeAnalysisContext context)
        {
            var classDecl = (ClassDeclarationSyntax)context.Node;

            if (!classDecl.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword)))
            {
                bool isTargetClass = classDecl.AttributeLists
                    .Any(attrList =>
                        attrList.Attributes.Any(attr =>
                            attr.Name.ToString() == nameof(IncludeSelfNotifyPropertyAttribute)
                            || attr.Name.ToString() == nameof(IncludeSelfNotifyPropertyAttribute).Replace("Attribute","")
                        )
                    );

                if (isTargetClass)
                {
                    var diagnostic = Diagnostic.Create(
                        Rule,
                        classDecl.Identifier.GetLocation(),
                        classDecl.Identifier.Text
                    );
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
        #endregion
    }
}
