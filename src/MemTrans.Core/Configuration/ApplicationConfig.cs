// ***********************************************************************
// Assembly         : MemTrans.Core
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="ApplicationConfig.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Core.Configuration;

using JaINTP.MemTrans.Core.Models;
using Newtonsoft.Json;
using System.ComponentModel;

/// <summary>
/// Basic application configuration class.
/// </summary>
public class ApplicationConfig
{
    /// <summary>
    /// Gets or sets the UI options.
    /// </summary>
    [DefaultValue(default(UIOptions))]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
    public UIOptions UIOptions { get; set; }
}