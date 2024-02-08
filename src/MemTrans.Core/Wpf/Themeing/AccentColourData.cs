// ***********************************************************************
// Assembly         : MemTrans.Core
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="AccentColourData.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Core.Wpf.Themeing;

using Prism.Commands;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

/// <summary>
/// Basic class to emulate functionality from
/// https://github.com/MahApps/MahApps.Metro/blob/main/src/MahApps.Metro.Samples/MahApps.Metro.Demo/MainWindowViewModel.cs.
/// </summary>
public class AccentColourData
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AccentColourData" /> class.
    /// </summary>
    public AccentColourData()
    {
        this.ChangeAccentCommand = new DelegateCommand<object>(this.ChangeAccentCommandExecute);
    }

    /// <summary>
    ///     Gets or sets the name.
    /// </summary>
    /// <value>
    ///     The name.
    /// </value>
    public string? Name { get; set; } = "Blue";

    /// <summary>
    ///     Gets or sets the border colour brush.
    /// </summary>
    /// <value>
    ///     The border colour brush.
    /// </value>
    public Brush? BorderColourBrush { get; set; } = Brushes.Black;

    /// <summary>
    ///     Gets or sets the colour brush.
    /// </summary>
    /// <value>
    ///     The colour brush.
    /// </value>
    public Brush? ColourBrush { get; set; } = Brushes.Blue;

    /// <summary>
    ///     Gets the change accent command.
    /// </summary>
    /// <value>
    ///     The change accent command.
    /// </value>
    public ICommand ChangeAccentCommand { get; private set; }

    /// <summary>
    ///     The ChangeAccentCommand execute method.
    /// </summary>
    /// <param name="sender">
    ///     The sender.
    /// </param>
    protected virtual void ChangeAccentCommandExecute(object sender)
    {
        _ = ControlzEx.Theming
                  .ThemeManager
                  .Current
                  .ChangeThemeColorScheme(Application.Current, this.Name);
    }
}