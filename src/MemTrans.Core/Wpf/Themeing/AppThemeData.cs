// ***********************************************************************
// Assembly         : MemTrans.Core
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="AppThemeData.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Core.Wpf.Themeing;

using System.Windows;

/// <summary>
///     Basic class to emulate functionality from
///     https://github.com/MahApps/MahApps.Metro/blob/main/src/MahApps.Metro.Samples/MahApps.Metro.Demo/MainWindowViewModel.cs.
/// </summary>
public class AppThemeData
    : AccentColourData
{
    /// <inheritdoc/>
    protected override void ChangeAccentCommandExecute(object sender)
    {
        ControlzEx.Theming
                  .ThemeManager
                  .Current
                  .ChangeThemeBaseColor(Application.Current, this.Name);
    }
}