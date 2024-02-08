// ***********************************************************************
// Assembly         : MemTrans.Modules.UI
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="UIModule.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Modules.UI;

using JaINTP.MemTrans.Core;
using JaINTP.MemTrans.Modules.UI.Dialogs;
using JaINTP.MemTrans.Modules.UI.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Navigation.Regions;

/// <summary>
///     The module initialization class.
/// </summary>
public class UIModule
    : IModule
{
    private readonly IRegionManager regionManager;

    /// <summary>
    ///     Initializes a new instance of the <see cref="UIModule"/> class.
    /// </summary>
    /// <param name="regionManager">The region manager.</param>
    public UIModule(IRegionManager regionManager)
    {
        this.regionManager = regionManager;
    }

    /// <summary>
    ///     Notifies the module that it has been initialized.
    /// </summary>
    /// <param name="containerProvider">
    ///     The container provider.
    /// </param>
    public void OnInitialized(IContainerProvider containerProvider)
    {
        this.regionManager.RequestNavigate(RegionNames.MainRegion, "MainView");
        this.regionManager.RequestNavigate(RegionNames.SettingsRegion, "SettingsView");
        this.regionManager.RequestNavigate(RegionNames.PerSettingsRegion, "UISettingsView");
    }

    /// <summary>
    ///     Used to register types with the container that will be used by your application.
    /// </summary>
    /// <param name="containerRegistry">
    ///     The container registry.
    /// </param>
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Views.
        containerRegistry.RegisterForNavigation<MainView>();
        containerRegistry.RegisterForNavigation<SettingsView>();
        containerRegistry.RegisterForNavigation<UISettingsView>();

        // Dialogs.
        containerRegistry.RegisterDialog<WarningDialog>();
    }
}