// ***********************************************************************
// Assembly         : MemTrans.Services.Interfaces
// Author           : Jai Brown
// Created          : 7-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 7-2-2024
// ***********************************************************************
// <copyright file="IAudioFileConverterService.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Services.Interfaces;

using System.Threading.Tasks;

/// <summary>
/// Represents the interface for an audio file converter service.
/// </summary>
public interface IAudioFileConverterService
{
    /// <summary>
    /// Converts the input audio file to WAV format asynchronously.
    /// </summary>
    /// <param name="inputFilePath">The path of the input audio file.</param>
    /// <param name="outputFilePath">The path where the converted WAV file will be saved.</param>
    /// <returns>A task representing the asynchronous operation. The task result is true if the conversion is successful; otherwise, false.</returns>
    public Task<bool> ConvertToWavAsync(string inputFilePath, string outputFilePath);

    /// <summary>
    /// Checks if the specified file is a WAV file.
    /// </summary>
    /// <param name="filePath">The path of the file to check.</param>
    /// <returns>True if the file is a WAV file; otherwise, false.</returns>
    public bool IsWavFile(string filePath);
}