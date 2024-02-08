// ***********************************************************************
// Assembly         : MemTrans.Services.Interfaces
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="IStorageService.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Services.Interfaces;

using System;
using JaINTP.MemTrans.Core.Types;

/// <summary>
///     Interface for a basic service to save and load objects as Json.
/// </summary>
public interface IStorageService
{
    /// <summary>
    ///    An event that is raised when the storage is updated so any classes using values can update.
    /// </summary>
    public event EventHandler<EventArgs<string>> StorageUpdated;

    /// <summary>
    ///     Loads data from storage of a given key of type T.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of object to be loaded.
    /// </typeparam>
    /// <param name="key">
    ///     The key of the storage.
    /// </param>
    /// <returns>
    ///     Deserialised object of type T.
    /// </returns>
    T LoadObject<T>(string key);

    /// <summary>
    ///     Saves data to storage under the given key.
    /// </summary>
    /// <param name="obj">
    ///     The object to be serialized.
    /// </param>
    /// <param name="key">
    ///     The storage key.
    /// </param>
    void SaveObject(object obj, string key);
}