// ***********************************************************************
// Assembly         : MemTrans.Core
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="EventArgs.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Core.Types;

using System;

/// <summary>
///     A generic version of <see cref="EventArgs" /> that accepts a generic typed parameter.
/// </summary>
/// <typeparam name="T">
///     The type of the parameter to be passed with the event arg.
/// </typeparam>
public class EventArgs<T>
    : EventArgs
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EventArgs{T}" /> class.
    /// </summary>
    /// <param name="value">
    ///     The value to be passed with the event arg.
    /// </param>
    public EventArgs(T value)
    {
        this.Value = value;
    }

    /// <summary>
    ///     Gets the value passed with the event arg.
    /// </summary>
    /// <value>
    ///     The value passed with the event arg.
    /// </value>
    public T Value { get; private set; }
}