// ***********************************************************************
// Assembly         : MemTrans.Modules.UI
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="SettingsViewModel.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Modules.UI.ViewModels;

using JaINTP.MemTrans.Core.Mvvm;
using Prism.Navigation.Regions;

/// <summary>
///     The settings viewmodel.
/// </summary>
public class SettingsViewModel
    : RegionViewModelBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SettingsViewModel" /> class.
    /// </summary>
    /// <param name="regionManager">
    ///     The region manager.
    /// </param>
    /// <param name="storageService">
    ///     The storage service.
    /// </param>
    public SettingsViewModel(
        IRegionManager regionManager)
        : base(regionManager)
    {
    }
}