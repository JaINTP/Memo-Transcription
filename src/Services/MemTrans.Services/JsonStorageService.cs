// ***********************************************************************
// Assembly         : MemTrans.Services
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="JsonStorageService.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Services;

using System.IO;
using JaINTP.MemTrans.Core.Types;
using JaINTP.MemTrans.Services.Interfaces;
using Newtonsoft.Json;

/// <summary>
///    A basic service to save and load objects as Json.
/// </summary>
public class JsonStorageService
    : IStorageService
{
    /// <summary>
    ///    An event that is raised when the storage is updated so any classes using values can update.
    /// </summary>
    public event EventHandler<EventArgs<string>> StorageUpdated;

    /// <summary>
    ///     Loads the Json based settings file and creates a settings class from it.
    ///     Uses a shitty hack to generate an object of type T via its JsonProperty attributes.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of object to be loaded.
    /// </typeparam>
    /// <param name="filePath">
    ///     The name of the file.
    /// </param>
    /// <returns>
    ///     Deserialised object of type T.
    /// </returns>
    public T LoadObject<T>(string filePath)
    {
        T t;

        if (File.Exists((string)filePath))
        {
            using var streamReader = new StreamReader((string)filePath);
            using var jsonReader = new JsonTextReader(streamReader);
            var jsonSerializer = new JsonSerializer();
            t = jsonSerializer.Deserialize<T>(jsonReader);
        }
        else
        {
            // Cheap hack to have Newtonsoft build out settings file from the JsonProperty.
            // attributes in the Settings class. Will probably throw exceptions if you don't
            // have them...
            t = JsonConvert.DeserializeObject<T>("{}");
            this.SaveObject(t, filePath);
        }

        return t;
    }

    /// <summary>
    ///     Saves an object in a file as Json to the specified location.
    ///     Creates the parent directory in the path if it doesn't exist.
    /// </summary>
    /// <param name="obj">
    ///     The object to be serialized.
    /// </param>
    /// <param name="filePath">
    ///     The file name.
    /// </param>
    public void SaveObject(object obj, string filePath)
    {
        var jsonSerializer = new Newtonsoft.Json.JsonSerializer();
        jsonSerializer.Formatting = Formatting.Indented;
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        using (var streamWriter = new StreamWriter(filePath))
        {
            using var jsonWriter = new JsonTextWriter(streamWriter);
            jsonSerializer.Serialize(jsonWriter, obj);
        }

        this.StorageUpdated?.Invoke(this, new EventArgs<string>(filePath));
    }
}