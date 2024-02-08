// ***********************************************************************
// Assembly         : MemTrans.Services.Interfaces
// Author           : Jai Brown
// Created          : 7-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 7-2-2024
// ***********************************************************************
// <copyright file="IAiTranscriptionService.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Services.Interfaces;

/// <summary>
/// Represents the interface for an AI transcription service.
/// </summary>
public interface IAiTranscriptionService
{
    /// <summary>
    /// Transcribes the input audio asynchronously.
    /// </summary>
    /// <param name="inputAudioPath">The path to the input audio file.</param>
    /// <returns>An asynchronous enumerable of transcribed strings.</returns>
    public IAsyncEnumerable<string> TranscribeAsync(string inputAudioPath);
}