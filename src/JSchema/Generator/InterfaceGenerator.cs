﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.JSchema.Generator
{
    /// <summary>
    /// Generate the text of an interface.
    /// </summary>
    /// <remarks>
    /// Hat tip: Mike Bennett, "Generating Code with Roslyn",
    /// https://dogschasingsquirrels.com/2014/07/16/generating-code-with-roslyn/
    /// </remarks>
    public class InterfaceGenerator : ClassOrInterfaceGenerator
    {
        public InterfaceGenerator(JsonSchema rootSchema, HintDictionary hintDictionary)
            : base(rootSchema, hintDictionary)
        {
        }

        public override BaseTypeDeclarationSyntax CreateTypeDeclaration(JsonSchema schema)
        {
            var modifiers = SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

            return SyntaxFactory.InterfaceDeclaration(TypeName).WithModifiers(modifiers);
        }

        public override void AddMembers(JsonSchema schema)
        {
            List<MemberDeclarationSyntax> members = GenerateProperties(schema);
            SyntaxList<MemberDeclarationSyntax> memberList = SyntaxFactory.List(members);
            TypeDeclaration = (TypeDeclaration as InterfaceDeclarationSyntax).WithMembers(memberList);
        }

        protected override SyntaxTokenList CreatePropertyModifiers()
        {
            return default(SyntaxTokenList);
        }

        protected override string MakeHintDictionaryKey(string propertyName)
        {
            // We want the interface to use the same hints as the class it was made from.
            // For example, if the class has an object-valued property that should really
            // be a dictionary, we want the interface to declare the property as a dictionary
            // as well. The dictionary is keyed by the name of the class+property, for
            // example, "Foo.Options". The interface name is "IFoo", so we remove the first
            // letter.
            return TypeName.Substring(1) + "." + propertyName.ToPascalCase();
        }
    }
}
