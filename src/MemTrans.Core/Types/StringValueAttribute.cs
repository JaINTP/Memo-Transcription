// ***********************************************************************
// Assembly         : MemTrans.Core
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="StringValueAttribute.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Core.Types;

using System;

/// <summary>
/// Represents an attribute that can be used to associate a string value with a class or member.
/// </summary>
public class StringValueAttribute
    : Attribute
{
    private string stringValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="StringValueAttribute"/> class with the specified string value.
    /// </summary>
    /// <param name="stringValue">The string value to associate with the class or member.</param>
    public StringValueAttribute(string stringValue)
    {
        this.StringValue = stringValue;
    }

    /// <summary>
    /// Gets or sets the string value associated with the class or member.
    /// </summary>
    public string StringValue
    {
        get => this.stringValue;
        set => this.stringValue = value;
    }
}