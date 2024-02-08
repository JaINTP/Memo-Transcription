// ***********************************************************************
// Assembly         : MemTrans.Services.Interfaces
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="ILocalizationService.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Services.Interfaces;

/// <summary>
/// Represents the interface for a localization service.
/// </summary>
public interface ILocalizationService
{
    /// <summary>
    /// Localizes the specified key with a fallback value.
    /// </summary>
    /// <param name="key">The key to localize.</param>
    /// <param name="fallback">The fallback value.</param>
    /// <returns>The localized string.</returns>
    string Localize(string key, string fallback);
}