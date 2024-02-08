// ***********************************************************************
// Assembly         : MemTrans
// Author           : Jai Brown
// Created          : 8-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 8-2-2024
// ***********************************************************************
// <copyright file="MetroDialogWindow.xaml.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Views;

/// <summary>
/// Interaction logic for MetroDialogWindow.xaml.
/// </summary>
public partial class MetroDialogWindow
    : MahApps.Metro.Controls.MetroWindow, IDialogAware, IDialogWindow
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MetroDialogWindow" /> class.
    /// </summary>
    public MetroDialogWindow()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Gets the dialog close listener.
    /// </summary>
    public DialogCloseListener RequestClose { get; private set; }

    /// <summary>
    /// Gets or sets the dialog result.
    /// </summary>
    public IDialogResult Result { get; set; }

    /// <summary>
    /// Determines whether the dialog can be closed.
    /// </summary>
    /// <returns><c>true</c> if the dialog can be closed; otherwise, <c>false</c>.</returns>
    public bool CanCloseDialog()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Called when the dialog is closed.
    /// </summary>
    public void OnDialogClosed()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Called when the dialog is opened.
    /// </summary>
    /// <param name="parameters">The dialog parameters.</param>
    public void OnDialogOpened(IDialogParameters parameters)
    {
        throw new NotImplementedException();
    }
}