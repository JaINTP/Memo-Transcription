// ***********************************************************************
// Assembly         : MemTrans.Services
// Author           : Jai Brown
// Created          : 7-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 7-2-2024
// ***********************************************************************
// <copyright file="OpenFileDialogService.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Services;

using System;
using JaINTP.MemTrans.Services.Interfaces;
using Microsoft.Win32;

/// <summary>
/// Represents a service for opening file dialogs.
/// </summary>
public class OpenFileDialogService
    : IOpenFileDialogService
{
    /// <summary>
    /// Shows the open file dialog and returns the selected file path.
    /// </summary>
    /// <returns>The selected file path, or null if no file was selected.</returns>
    public string Show()
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Audio files (*.mp3;*.wav;*.flac;*.m4a)|*.mp3;*.wav;*.flac;*.m4a",
            DefaultExt = ".wav",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            Title = "Select a text file",
        };

        if (openFileDialog.ShowDialog() == true)
        {
            return openFileDialog.FileName;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Asynchronously shows the open file dialog and returns the selected file path.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the selected file path, or null if no file was selected.</returns>
    public async Task<string> ShowAsync()
    {
        return await Task.Run(() => this.Show());
    }
}