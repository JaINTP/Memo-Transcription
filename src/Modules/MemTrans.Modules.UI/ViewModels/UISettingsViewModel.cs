// ***********************************************************************
// Assembly         : MemTrans.Modules.UI
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="UISettingsViewModel.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Modules.UI.ViewModels;

using System.Collections.ObjectModel;
using System.Windows.Input;
using ControlzEx.Theming;
using JaINTP.MemTrans.Core.Configuration;
using JaINTP.MemTrans.Core.Models;
using JaINTP.MemTrans.Core.Types;
using JaINTP.MemTrans.Core.Wpf.Themeing;
using JaINTP.MemTrans.Services.Interfaces;

/// <summary>
/// Represents the view model for the UI settings.
/// </summary>
public class UISettingsViewModel
    : Core.Mvvm.RegionViewModelBase
{
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
    private readonly ILocalizationService locService;
    private readonly IStorageService storageService;

    private ApplicationConfig config;
    private ObservableCollection<string> languageList;
    private bool minimizeToSystemTray;
    private bool onStartup;
    private AccentColourData? selectedAccentColour;
    private string selectedLanguage;
    private AppThemeData? selectedTheme;
    private string string_Accent;
    private string string_Appearance;
    private string string_Language;

    /// <summary>
    /// Initializes a new instance of the <see cref="UISettingsViewModel"/> class.
    /// </summary>
    /// <param name="regionManager">The region manager.</param>
    /// <param name="storageService">The storage service.</param>
    /// <param name="locService">The localization service.</param>
    public UISettingsViewModel(
        IRegionManager regionManager,
        IStorageService storageService,
        ILocalizationService locService)
        : base(regionManager)
    {
        this.locService = locService;
        this.storageService = storageService;
        this.storageService.StorageUpdated += this.StorageService_StorageUpdated;
        this.config = this.storageService.LoadObject<ApplicationConfig>(Core.Constants.ConfigFile);
        this.config.UIOptions ??= new UIOptions();

        this.SetupLocalization();
        this.LanguageList = new ObservableCollection<string>()
        {
            this.locService.Localize("String_English", "English"),
            this.locService.Localize("String_German", "German"),
        };

        this.Title = "UI Settings";
        this.AppThemes =
                ThemeManager.Current
                            .Themes
                            .GroupBy(x => x.BaseColorScheme)
                            .Select(x => x.First())
                            .Select(a => new AppThemeData
                            {
                                Name = a.BaseColorScheme,
                                BorderColourBrush = a.Resources["MahApps.Brushes.ThemeForeground"] as System.Windows.Media.Brush,
                                ColourBrush = a.Resources["MahApps.Brushes.ThemeBackground"] as System.Windows.Media.Brush,
                            })
                            .ToList()!;
        this.AccentColours =
            ThemeManager.Current
                        .Themes
                        .GroupBy(x => x.ColorScheme)
                        .OrderBy(a => a.Key)
                        .Select(a => new AccentColourData
                        {
                            Name = a.Key,
                            ColourBrush = a.First().ShowcaseBrush,
                        })
                        .ToList();

        this.SelectedAccentColour =
            this.AccentColours.Single(x => x.Name == (this.config.UIOptions.SelectedAccentColour?.Name ?? "Purple"));
        this.SelectedAccentColour.ChangeAccentCommand?.Execute(null);
        this.SelectedTheme =
            this.AppThemes.Single(x => x.Name == (this.config.UIOptions.SelectedTheme?.Name ?? "Light"));
        this.SelectedTheme.ChangeAccentCommand?.Execute(null);

        this.PropertyChanged += this.OnPropertyChanged;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UISettingsViewModel"/> class.
    /// This constructor is used for design-time data.
    /// </summary>
    private UISettingsViewModel()
        : base(null!)
    {
    }

    /// <summary>
    /// Event that is raised when the language is changed.
    /// </summary>
    public static event EventHandler LanguageChanged;

    /// <summary>
    /// Gets or sets the accent colours.
    /// </summary>
    public List<AccentColourData> AccentColours { get; set; }

    /// <summary>
    /// Gets or sets the application themes.
    /// </summary>
    public List<AppThemeData> AppThemes { get; set; }

    /// <summary>
    /// Gets the command for when the language is changed.
    /// </summary>
    public ICommand LanguageChangedCommand
    {
        get => new DelegateCommand(this.LanguageChangedCommandExecute);
    }

    /// <summary>
    /// Gets or sets the language list.
    /// </summary>
    public ObservableCollection<string> LanguageList
    {
        get => this.languageList;
        set => this.SetProperty(ref this.languageList, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether to minimize to system tray.
    /// </summary>
    public bool MinimizeToSystemTray
    {
        get => this.minimizeToSystemTray;
        set => this.SetProperty(ref this.minimizeToSystemTray, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether to run on startup.
    /// </summary>
    public bool OnStartup
    {
        get => this.onStartup;
        set => this.SetProperty(ref this.onStartup, value);
    }

    /// <summary>
    /// Gets or sets the selected accent colour.
    /// </summary>
    public AccentColourData SelectedAccentColour
    {
        get => this.selectedAccentColour;
        set => this.SetProperty(ref this.selectedAccentColour, value);
    }

    /// <summary>
    /// Gets or sets the selected language.
    /// </summary>
    public string SelectedLanguage
    {
        get => this.selectedLanguage;
        set => this.SetProperty(ref this.selectedLanguage, value);
    }

    /// <summary>
    /// Gets or sets the selected theme.
    /// </summary>
    public AppThemeData SelectedTheme
    {
        get => this.selectedTheme;
        set => this.SetProperty(ref this.selectedTheme, value);
    }

    /// <summary>
    /// Gets or sets the localized string for "Accent".
    /// </summary>
    public string String_Accent
    {
        get => this.string_Accent;
        set => this.SetProperty(ref this.string_Accent, value);
    }

    /// <summary>
    /// Gets or sets the localized string for "Appearance".
    /// </summary>
    public string String_Appearance
    {
        get => this.string_Appearance;
        set => this.SetProperty(ref this.string_Appearance, value);
    }

    /// <summary>
    /// Gets the localized string for "Language".
    /// </summary>
    public string String_Language
    {
        get => this.string_Language;
        private set => this.SetProperty(ref this.string_Language, value);
    }

    /// <summary>
    /// Executes the command when the language is changed.
    /// </summary>
    private void LanguageChangedCommandExecute()
    {
        this.config.UIOptions.SelectedLanguage = this.SelectedLanguage;
        LanguageChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Handles the property changed event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        var configProp = typeof(UIOptions).GetProperty(e.PropertyName);
        var currentProp = typeof(UISettingsViewModel).GetProperty(e.PropertyName);
        configProp.SetValue(this.config.UIOptions, currentProp.GetValue(this));
        this.storageService.SaveObject(this.config, Core.Constants.ConfigFile);
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

    /// <summary>
    /// Handles the storage updated event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void StorageService_StorageUpdated(object? sender, EventArgs<string> e)
    {
        if (e.Value == Core.Constants.ConfigFile)
        {
            this.config = this.storageService.LoadObject<ApplicationConfig>(Core.Constants.ConfigFile);
        }
    }
}