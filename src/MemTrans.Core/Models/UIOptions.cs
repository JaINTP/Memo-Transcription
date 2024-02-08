// ***********************************************************************
// Assembly         : MemTrans.Core
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="UIOptions.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Core.Models;

using JaINTP.MemTrans.Core.Wpf.Themeing;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Windows.Media;

public class UIOptions
{
    /// <summary>
    ///     Gets or sets a value indicating whether the application should [minimize to system tray].
    /// </summary>
    /// <value>
    ///     <c>true</c> if the application [minimize to system tray]; otherwise, <c>false</c>.
    /// </value>
    [DefaultValue(false)]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
    public bool MinimizeToSystemTray { get; set; } = false;

    /// <summary>
    ///     Gets or sets a value indicating whether the application should run [on startup].
    /// </summary>
    /// <value>
    ///     <c>true</c> if the application should run [on startup]; otherwise, <c>false</c>.
    /// </value>
    [DefaultValue(false)]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
    public bool OnStartup { get; set; } = false;

    /// <summary>
    ///     Gets or sets the selected accent colour.
    /// </summary>
    /// <value>
    ///     The selected accent colour.
    /// </value>
    [DefaultValue(null)]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
    public AccentColourData? SelectedAccentColour { get; set; }
        = new AccentColourData
        {
            Name = "Blue",
            ColourBrush = new BrushConverter().ConvertFromString("#FF0078D7") as SolidColorBrush,
        };

    /// <summary>
    ///     Gets or sets the selected theme.
    /// </summary>
    /// <value>
    ///     The selected theme.
    /// </value>
    [DefaultValue(null)]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
    public AppThemeData? SelectedTheme { get; set; }
    = new AppThemeData
    {
        Name = "Light",
        ColourBrush = new BrushConverter().ConvertFromString("#FFFFFFFF") as SolidColorBrush,
        BorderColourBrush = new BrushConverter().ConvertFromString("#FF000000") as SolidColorBrush,
    };

    /// <summary>
    ///     Gets or sets the selected language.
    /// </summary>
    /// <value>
    ///     The selected language.
    /// </value>
    [DefaultValue("English")]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
    public string SelectedLanguage { get; set; }
}