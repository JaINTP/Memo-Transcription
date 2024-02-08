// ***********************************************************************
// Assembly         : MemTrans.Core
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="Constants.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Core;

using System;
using System.IO;

/// <summary>
/// Class containing constants used in the Memo Transcription application.
/// </summary>
public class Constants
{
    /// <summary>
    /// Gets the application folder path.
    /// </summary>
    public static readonly string ApplicationFolder =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Memo Transcription");

    /// <summary>
    /// Gets the configuration file path.
    /// </summary>
    public static readonly string ConfigFile = Path.Combine(ApplicationFolder, "config.json");

    /// <summary>
    /// Gets the log folder path.
    /// </summary>
    public static readonly string LogFolder = Path.Combine(ApplicationFolder, "Logs");
}