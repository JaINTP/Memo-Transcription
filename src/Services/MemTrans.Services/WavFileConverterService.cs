// ***********************************************************************
// Assembly         : MemTrans.Services
// Author           : Jai Brown
// Created          : 7-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 7-2-2024
// ***********************************************************************
// <copyright file="WavFileConverterService.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Services;

using HeyRed.Mime;
using JaINTP.MemTrans.Services.Interfaces;
using NAudio.Wave;
using NLog;

/// <summary>
/// Represents a service for converting audio files to WAV format.
/// </summary>
public class WavFileConverterService
    : IAudioFileConverterService
{
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Converts the audio file at the specified input path to WAV format and saves it to the specified output path.
    /// </summary>
    /// <param name="inputFilePath">The path of the input audio file.</param>
    /// <param name="outputFilePath">The path to save the converted WAV file.</param>
    /// <returns>A task representing the asynchronous operation. The task result is a boolean indicating whether the conversion was successful.</returns>
    public async Task<bool> ConvertToWavAsync(string inputFilePath, string outputFilePath)
    {
        var exists = File.Exists(inputFilePath);
        Logger.Log(LogLevel.Info, $"File {inputFilePath} exists: {exists}");
        using (var reader = new MediaFoundationReader(Path.GetFullPath(inputFilePath)))
        {
            var waveFormat = new WaveFormat(16000, 16, reader.WaveFormat.Channels);
            using (var conversionStream = new WaveFormatConversionStream(waveFormat, reader))
            {
                await Task.Run(() => WaveFileWriter.CreateWaveFile(outputFilePath, conversionStream));
            }
        }

        return true;
    }

    /// <summary>
    /// Determines whether the specified file is a WAV file.
    /// </summary>
    /// <param name="filePath">The path of the file to check.</param>
    /// <returns><c>true</c> if the file is a WAV file; otherwise, <c>false</c>.</returns>
    public bool IsWavFile(string filePath)
    {
        string extension = MimeTypesMap.GetExtension(filePath);
        string mimeType = MimeTypesMap.GetMimeType(extension);
        return mimeType == "audio/wav";
    }
}