// ***********************************************************************
// Assembly         : MemTrans.Services
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="LocalizationService.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Services;

using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using CheapLoc;
using JaINTP.MemTrans.Core.Types;
using JaINTP.MemTrans.Services.Interfaces;

/// <summary>
/// Represents a service for localization.
/// </summary>
public class LocalizationService
    : ILocalizationService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocalizationService"/> class.
    /// </summary>
    public LocalizationService()
    {
        try
        {
            var supportedLangs = new[]
            {
                    "en",
            };

            var currentUiLang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

            if (supportedLangs.Any(x => currentUiLang == x))
            {
                Loc.Setup(this.ReadResource($"loc_{currentUiLang}.json"));
            }
            else
            {
                Loc.SetupWithFallbacks();
            }
        }
        catch (Exception)
        {
            Loc.Setup("{}");
        }
    }

    /// <summary>
    /// Supported languages.
    /// </summary>
    public enum Language : int
    {
        /// <summary>
        /// Represents the English language.
        /// </summary>
        [StringValue("en-US")]
        English,
    }

    /// <summary>
    /// Localizes the specified key.
    /// </summary>
    /// <param name="key">The localization key.</param>
    /// <param name="fallback">The fallback value.</param>
    /// <returns>The localized string.</returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public string Localize(string key, string fallback)
    {
        return Loc.Localize(key, fallback, Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// Reads the resource from the assembly.
    /// </summary>
    /// <param name="resourceName">The name of the resource.</param>
    /// <returns>The content of the resource as a string.</returns>
    private string ReadResource(string resourceName)
    {
        var assembly = AppDomain.CurrentDomain.GetAssemblies()
            .SingleOrDefault(a => a.GetName().Name == "MemTrans.Core");
        using var stream = assembly.GetManifestResourceStream(
            assembly.GetManifestResourceNames()
                    .FirstOrDefault(s => s.Contains(resourceName)));
        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }
}