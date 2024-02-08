// ***********************************************************************
// Assembly         : MemTrans.Modules.UI
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="MainViewModel.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Modules.UI.ViewModels;

using JaINTP.MemTrans.Core.Models;
using JaINTP.MemTrans.Core.Mvvm;
using JaINTP.MemTrans.Services.Interfaces;
using Prism.Navigation.Regions;

/// <summary>
/// The Main Viewmodel.
/// </summary>
public class MainViewModel
    : RegionViewModelBase
{
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
    private readonly IAiTranscriptionService aiTranscriptionService;
    private readonly IDialogService dialogService;
    private readonly ILocalizationService locService;
    private readonly IOpenFileDialogService openFileDialogService;

    private bool canConvert = false;
    private string filePath = "No File";
    private string memoString = string.Empty;

    private string string_error;
    private string string_errorConvertingFile;
    private string string_errorOpeningFile;
    private string string_fileName;
    private string string_noFile;
    private string string_open;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainViewModel" /> class.
    /// </summary>
    /// <param name="regionManager">The region manager.</param>
    /// <param name="aiTranscriptionService">The AI transcription service.</param>
    /// <param name="openFileDialogService">The open file dialog service.</param>
    /// <param name="dialogService">The dialog service.</param>
    /// <param name="locService">The localization service.</param>
    public MainViewModel(
        IRegionManager regionManager,
        IAiTranscriptionService aiTranscriptionService,
        IOpenFileDialogService openFileDialogService,
        IDialogService dialogService,
        ILocalizationService locService)
        : base(regionManager)
    {
        Logger.Debug("MainViewModel created.");
        this.locService = locService;
        this.SetupLocalization();

        this.aiTranscriptionService = aiTranscriptionService;
        this.dialogService = dialogService;
        this.openFileDialogService = openFileDialogService;

        this.ConvertCommand = new DelegateCommand(
            async () => await this.ExecuteConvertCommandAsync(),
            () => this.CanExecuteConvertCommandAsync());
        this.OpenFileDialogCommand = new DelegateCommand(async () => await this.OpenFileDialog());
        this.FilePath = string.Empty;
        this.PropertyChanged += this.OnPropertyChanged;
    }

    /// <summary>
    /// Gets the convert command.
    /// </summary>
    public DelegateCommand ConvertCommand { get; }

    /// <summary>
    /// Gets or sets the file path.
    /// </summary>
    public string FilePath
    {
        get => this.filePath;
        set => this.SetProperty(ref this.filePath, value);
    }

    /// <summary>
    /// Gets or sets the memo string.
    /// </summary>
    public string MemoString
    {
        get => this.memoString;
        set => this.SetProperty(ref this.memoString, value);
    }

    /// <summary>
    /// Gets the open file dialog command.
    /// </summary>
    public DelegateCommand OpenFileDialogCommand { get; }

    /// <summary>
    /// Gets or sets the error string.
    /// </summary>
    public string String_Error
    {
        get => this.string_error;
        set => this.SetProperty(ref this.string_error, value);
    }

    /// <summary>
    /// Gets or sets the error convert string.
    /// </summary>
    public string String_ErrorConvertingFile
    {
        get => this.string_errorConvertingFile;
        set => this.SetProperty(ref this.string_errorConvertingFile, value);
    }

    /// <summary>
    /// Gets or sets the error opening file string.
    /// </summary>
    public string String_ErrorOpeningFile
    {
        get => this.string_errorOpeningFile;
        set => this.SetProperty(ref this.string_errorOpeningFile, value);
    }

    /// <summary>
    /// Gets or sets the value indicating the file name.
    /// </summary>
    public string String_FileName
    {
        get => this.string_fileName;
        set => this.SetProperty(ref this.string_fileName, value);
    }

    /// <summary>
    /// Gets or sets the value indicating whether there is no file.
    /// </summary>
    public string String_NoFile
    {
        get => this.string_noFile;
        set => this.SetProperty(ref this.string_noFile, value);
    }

    /// <summary>
    /// Gets or sets the value indicating the open action.
    /// </summary>
    public string String_Open
    {
        get => this.string_open;
        set => this.SetProperty(ref this.string_open, value);
    }

    public bool CanExecuteConvertCommandAsync()
    {
        return this.FilePath != string.Empty;
    }

    /// <summary>
    /// Executes the convert command asynchronously.
    /// </summary>
    private async Task ExecuteConvertCommandAsync()
    {
        try
        {
            var isFirstResult = true;

            await foreach (var line in this.aiTranscriptionService.TranscribeAsync(this.FilePath))
            {
                var str = line;
                if (isFirstResult)
                {
                    str = line.TrimStart();
                    isFirstResult = false;
                }

                this.MemoString = string.Join(string.Empty, new[] { this.memoString, str.Replace(". ", ".\n") });
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error converting file.");
            this.dialogService.Show(
                "WarningDialog",
                new DialogParameters
                {
                        { "Title", this.String_Error },
                        { "Message", $"{this.String_ErrorConvertingFile}\n{ex.Message}" },
                },
                r => { },
                "MetroDialogWindow");
        }
    }

    /// <summary>
    /// Handles the property changed event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        this.ConvertCommand.RaiseCanExecuteChanged();
    }

    /// <summary>
    /// Opens the file dialog asynchronously.
    /// </summary>
    private async Task OpenFileDialog()
    {
        try
        {
            var filePath = await this.openFileDialogService.ShowAsync();
            this.FilePath = filePath;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error opening file dialog.");
            this.dialogService.Show(
                "WarningDialog",
                new DialogParameters
                {
                        { "Title", this.String_Error },
                        { "Message", $"{this.String_ErrorOpeningFile}\n{ex.Message}" },
                },
                r => { },
                "MetroDialogWindow");
        }
    }

    /// <summary>
    /// Sets up the localization for the view model.
    /// </summary>
    private void SetupLocalization()
    {
        foreach (var configProp in this.GetType()
                                       .GetProperties()
                                       .Where(p => p.Name.Contains("String_")))
        {
            string newValue = this.locService.Localize(configProp.Name, (string)configProp.GetValue(this));
            configProp.SetValue(this, newValue);
        }
    }
}