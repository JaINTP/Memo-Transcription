// ***********************************************************************
// Assembly         : MemTrans.Services
// Author           : Jai Brown
// Created          : 7-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 7-2-2024
// ***********************************************************************
// <copyright file="WhisperTranscriptionService.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Services;

using JaINTP.MemTrans.Services.Interfaces;
using Whisper.net;
using Whisper.net.Ggml;
using Whisper.net.Logger;

/// <summary>
/// Provides transcription services using the Whisper library.
/// </summary>
public class WhisperTranscriptionService
    : IAiTranscriptionService
{
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
    private IAudioFileConverterService audioFileConverter;

    /// <summary>
    /// Initializes a new instance of the <see cref="WhisperTranscriptionService"/> class.
    /// </summary>
    /// <param name="audioFileConverter">The audio file converter service.</param>
    public WhisperTranscriptionService(IAudioFileConverterService audioFileConverter)
    {
        LogProvider.Instance.OnLog += (level, message) =>
        {
            Logger.Info($"{level}: {message}");
        };
        this.audioFileConverter = audioFileConverter;
    }

    /// <summary>
    /// Downloads the Whisper model file.
    /// </summary>
    /// <param name="fileName">The name of the model file.</param>
    /// <param name="ggmlType">The GGML type of the model.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task DownloadModel(string fileName, GgmlType ggmlType)
    {
        Logger.Info($"Downloading Model {fileName}");
        using var modelStream = await WhisperGgmlDownloader.GetGgmlModelAsync(ggmlType);
        using var fileWriter = File.OpenWrite(fileName);
        await modelStream.CopyToAsync(fileWriter);
    }

    /// <summary>
    /// Transcribes the input audio file asynchronously.
    /// </summary>
    /// <param name="inputAudioPath">The path of the input audio file.</param>
    /// <returns>An asynchronous enumerable of transcriptions.</returns>
    public async IAsyncEnumerable<string> TranscribeAsync(string inputAudioPath)
    {
        var ggmlType = GgmlType.Base;
        var modelFileName = "ggml-base.bin";
        var wavFileName = "memo.wav";

        if (!File.Exists(modelFileName))
        {
            await this.DownloadModel(modelFileName, ggmlType);
        }

        if (!this.audioFileConverter.IsWavFile(inputAudioPath))
        {
            var converted = await this.audioFileConverter.ConvertToWavAsync(inputAudioPath, wavFileName);

            if (!converted)
            {
                throw new Exception($"Failed to convert audio file to wav:");
            }
        }
        else
        {
            wavFileName = inputAudioPath;
        }

        using var whisperFactory = WhisperFactory.FromPath("ggml-base.bin");
        using var processor = whisperFactory.CreateBuilder()
            .WithLanguage("auto")
            .Build();

        using var fileStream = File.OpenRead(wavFileName);
        var start = DateTime.Now;
        await foreach (var result in processor.ProcessAsync(fileStream))
        {
            yield return result.Text;
        }

        fileStream.Close();
        TimeSpan duration = DateTime.Now - start;

        string formattedDuration = duration.TotalSeconds < 60 ?
            $"{duration.TotalSeconds} seconds" :
            $"{(int)duration.TotalMinutes} minutes {(int)duration.TotalSeconds % 60} seconds";

        yield return $"\n\nTranscription took: {formattedDuration}";

        File.Delete(wavFileName);
    }
}