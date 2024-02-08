// ***********************************************************************
// Assembly         : MemTrans.Services.Interfaces
// Author           : Jai Brown
// Created          : 7-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 7-2-2024
// ***********************************************************************
// <copyright file="IOpenFileDialogService.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Services.Interfaces;

/// <summary>
/// Represents the interface for an open file dialog service.
/// </summary>
public interface IOpenFileDialogService
{
    /// <summary>
    /// Shows the open file dialog and returns the selected file path.
    /// </summary>
    /// <returns>The selected file path.</returns>
    string Show();

    /// <summary>
    /// Shows the open file dialog asynchronously and returns the selected file path.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the selected file path.</returns>
    Task<string> ShowAsync();
}