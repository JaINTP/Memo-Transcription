// ***********************************************************************
// Assembly         : MemTrans.Core
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="RegionViewModelBase.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Core.Mvvm;

using Prism.Navigation.Regions;
using System;

/// <summary>
///     Basic class for region navigatable viewmodels.
/// </summary>
/// <seealso cref="ViewModelBase" />
/// <seealso cref="Prism.Navigation.Regions.INavigationAware" />
/// <seealso cref="Prism.Navigation.Regions.IConfirmNavigationRequest" />
public class RegionViewModelBase
    : ViewModelBase, INavigationAware, IConfirmNavigationRequest
{
    private string title;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RegionViewModelBase" /> class.
    /// </summary>
    /// <param name="regionManager">
    ///     The region manager.
    /// </param>
    public RegionViewModelBase(IRegionManager regionManager)
        : base(regionManager)
    {
        this.RegionManager = regionManager;
    }

    public string Title
    {
        get => this.title;
        set => this.SetProperty(ref this.title, value);
    }

    /// <summary>
    ///     Gets the region manager.
    /// </summary>
    /// <value>
    ///     The region manager.
    /// </value>
    protected new IRegionManager RegionManager { get; private set; }

    /// <inheritdoc/>
    public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
    {
        continuationCallback(true);
    }

    /// <inheritdoc/>
    public new virtual bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    /// <inheritdoc/>
    public new virtual void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }

    /// <inheritdoc/>
    public new virtual void OnNavigatedTo(NavigationContext navigationContext)
    {
    }
}