﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

namespace Microsoft.JSchema.Generator
{
    /// <summary>
    /// Values that specify the type of initialization code that must be generated
    /// for each property in the implementation of the <code>Init</code> method.
    /// </summary>
    public enum InitializationKind
    {
        /// <summary>
        /// Do not generate code to initialize this property
        /// </summary>
        None,

        /// <summary>
        /// Initialize this property with an assignment without a null check.
        /// </summary>
        SimpleAssign,

        /// <summary>
        /// Initialize this property with special code to handle Uris.
        /// </summary>
        Uri
    }
}