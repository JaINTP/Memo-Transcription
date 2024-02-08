// ***********************************************************************
// Assembly         : MemTrans
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="App.xaml.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans;

using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using DryIoc;
using JaINTP.MemTrans.Core.Mvvm.Adapters;
using JaINTP.MemTrans.Modules.UI;
using JaINTP.MemTrans.Modules.UI.Dialogs;
using JaINTP.MemTrans.Modules.UI.ViewModels;
using JaINTP.MemTrans.Modules.UI.Views;
using JaINTP.MemTrans.Services;
using JaINTP.MemTrans.Services.Interfaces;
using JaINTP.MemTrans.Views;
using NLog;
using NLog.Targets;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Navigation.Regions;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
public partial class App
    : PrismApplication
{
    public static IContainer AppContainer { get; set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Format dates and currency.
        FrameworkElement.LanguageProperty.OverrideMetadata(
            typeof(FrameworkElement),
            new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.Name)));

        // Format text/control flow direction.
        var flowDirection = CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        FrameworkElement.FlowDirectionProperty.OverrideMetadata(
            typeof(FrameworkElement),
            new FrameworkPropertyMetadata(flowDirection));
    }

    /// <inheritdoc/>
    protected override Window CreateShell()
    {
        var debugFileName = Path.Combine(Core.Constants.LogFolder, $"debug-log_{DateTime.Today:dd-MM-yyyy}.txt");
        var errorFileName = Path.Combine(Core.Constants.LogFolder, $"error-log_{DateTime.Today:dd-MM-yyyy}.txt");
        var layout = "${longdate} ${level} ${message}  ${exception} ${event-properties:myProperty}";

        if (!Directory.Exists(Core.Constants.LogFolder))
        {
            Directory.CreateDirectory(Core.Constants.LogFolder);
        }

        NLog.LogManager.Setup()
            .SetupExtensions(s =>
            {
                s.AutoLoadExtensions();
                s.RegisterTarget<DebugTarget>("Debug");
            })
            .LoadConfiguration(builder =>
            {
                var config = NLog.LogManager.Configuration;

                NLog.LogManager.Configuration = config;

                builder.ForLogger()
                       .FilterMinLevel(LogLevel.Trace)
                       .WriteToDebug();
                builder.ForLogger()
                       .FilterMinLevel(LogLevel.Info)
                       .WriteToDebug()
                       .WriteToFile(fileName: debugFileName, layout: layout);
                builder.ForLogger()
                       .FilterMinLevel(LogLevel.Debug)
                       .WriteToDebug()
                       .WriteToFile(fileName: debugFileName, layout: layout)
                       .WriteToFile(fileName: errorFileName, layout: layout);
                builder.ForLogger()
                       .FilterMinLevel(LogLevel.Error)
                       .WriteToDebug()
                       .WriteToFile(fileName: errorFileName, layout: layout);
            });

        return this.Container.Resolve<MainWindow>();
    }

    /// <inheritdoc/>
    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        moduleCatalog.AddModule<UIModule>();
        base.ConfigureModuleCatalog(moduleCatalog);
    }

    /// <inheritdoc/>
    protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
    {
        regionAdapterMappings.RegisterMapping(
            typeof(StackPanel),
            this.Container.Resolve<GroupedStackPanelRegionAdapter>());

        base.ConfigureRegionAdapterMappings(regionAdapterMappings);
    }

    /// <inheritdoc/>
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        AppContainer = containerRegistry.GetContainer();

        // Windows.
        containerRegistry.RegisterDialogWindow<MetroDialogWindow>(nameof(MetroDialogWindow));

        // Navigation.
        containerRegistry.RegisterForNavigation<MainView>();
        containerRegistry.RegisterForNavigation<UIModule>();

        // Services.
        containerRegistry.RegisterSingleton<IStorageService, JsonStorageService>();
        containerRegistry.RegisterSingleton<ILocalizationService, LocalizationService>();
        containerRegistry.RegisterSingleton<IAudioFileConverterService, WavFileConverterService>();
        containerRegistry.RegisterSingleton<IAiTranscriptionService, WhisperTranscriptionService>();

        // Wind32 Dialogs.
        containerRegistry.RegisterSingleton<IOpenFileDialogService, OpenFileDialogService>();

        // Dialogs.
        containerRegistry.RegisterDialog<WarningDialog, WarningDialogViewModel>();
    }
}