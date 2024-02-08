// ***********************************************************************
// Assembly         : MemTrans
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="MainWindowViewModel.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.ViewModels;

using System.Diagnostics;
using System.Windows.Input;
using JaINTP.MemTrans.Core.Configuration;
using JaINTP.MemTrans.Core.Mvvm;
using JaINTP.MemTrans.Services.Interfaces;
using MahApps.Metro.Controls;
using Mono.Cecil.Cil;
using Prism.Commands;
using Prism.Navigation.Regions;

/// <summary>
/// Represents the view model for the main window of the application.
/// </summary>
public class MainWindowViewModel
    : RegionViewModelBase
{
    private readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    private readonly ILocalizationService locService;
    private readonly IStorageService storageService;
    private ApplicationConfig config;
    private bool debugVisible = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    /// <param name="regionManager">The region manager.</param>
    /// <param name="dialogService">The dialog service.</param>
    /// <param name="storageService">The storage service.</param>
    /// <param name="locService">The localization service.</param>
    public MainWindowViewModel(
        IRegionManager regionManager,
        IStorageService storageService,
        ILocalizationService locService)
        : base(regionManager)
    {
        this.locService = locService;
        this.SetupLocalization();

        this.storageService = storageService;
        this.storageService.StorageUpdated += this.StorageService_StorageUpdated;
        this.config = this.storageService.LoadObject<ApplicationConfig>(Core.Constants.ConfigFile);

        this.GithubCommand = new DelegateCommand(this.GithubCommandExecute);
        this.OpenFlyoutCommand = new DelegateCommand<Flyout>(this.OpenFlyoutCommandExecute);
    }

    /// <summary>
    /// Gets the command to open the GitHub page.
    /// </summary>
    public ICommand GithubCommand { get; }

    /// <summary>
    /// Gets the command to open a flyout.
    /// </summary>
    public ICommand OpenFlyoutCommand { get; }

    /// <summary>
    /// Gets the command to toggle the visibility of the debug information.
    /// </summary>
    public ICommand ToggleDebugCommand { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the debug information is visible.
    /// </summary>
    public bool DebugVisible
    {
        get => this.debugVisible;
        set => this.SetProperty(ref this.debugVisible, value);
    }

    /// <summary>
    /// Gets the tooltip for the GitHub command.
    /// </summary>
    public string GithubCommandToolTip { get; private set; }

    /// <summary>
    /// Gets the text for the settings.
    /// </summary>
    public string Settings { get; private set; }

    /// <summary>
    /// Gets the tooltip for the settings command.
    /// </summary>
    public string SettingsCommandToolTip { get; private set; }

    /// <summary>
    /// Sets up the localization for the view model.
    /// </summary>
    private void SetupLocalization()
    {
        this.GithubCommandToolTip = this.locService.Localize("ToolTipText_Github", "Opens the Author's GitHub.");
        this.Settings = this.locService.Localize("String_Settings", "Settings");
        this.SettingsCommandToolTip = this.locService.Localize("ToolTipText_Settings", "Opens the Application settings.");
    }

    /// <summary>
    /// Executes the GitHub command.
    /// </summary>
    private void GithubCommandExecute()
    {
        this.logger.Debug("GithubCommand executed.");

        var psInfo = new ProcessStartInfo(@"https://github.com/JaINTP")
        {
            UseShellExecute = true,
            Verb = "open",
        };

        Process.Start(psInfo);
    }

    /// <summary>
    /// Executes the command to open a flyout.
    /// </summary>
    /// <param name="flyout">The flyout to open.</param>
    private void OpenFlyoutCommandExecute(Flyout flyout)
    {
        if (flyout is not null)
        {
            flyout.SetCurrentValue(Flyout.IsOpenProperty, !flyout.IsOpen);
        }
    }

    /// <summary>
    /// Handles the StorageUpdated event of the storage service.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The event arguments.</param>
    private void StorageService_StorageUpdated(object? sender, EventArgs e)
    {
        this.config = this.storageService.LoadObject<ApplicationConfig>(Core.Constants.ConfigFile);
    }
}