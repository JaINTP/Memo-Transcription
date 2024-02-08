// ***********************************************************************
// Assembly         : MemTrans.Core
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="GroupedStackPanelRegionAdapter.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Core.Mvvm.Adapters;

using System.Collections.Specialized;
using System.Linq;
using System.Windows.Controls;

/// <summary>
/// Adapter class for managing a StackPanel as a region in the MVVM pattern.
/// </summary>
public class GroupedStackPanelRegionAdapter
    : RegionAdapterBase<StackPanel>
{
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
    private StackPanel regionTarget;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroupedStackPanelRegionAdapter"/> class.
    /// </summary>
    /// <param name="regionBehaviorFactory">The factory for creating region behaviors.</param>
    public GroupedStackPanelRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
        : base(regionBehaviorFactory)
    {
    }

    /// <summary>
    /// Adapts the StackPanel region by attaching event handlers and setting the region target.
    /// </summary>
    /// <param name="region">The region to adapt.</param>
    /// <param name="regionTarget">The StackPanel region target.</param>
    protected override void Adapt(IRegion region, StackPanel regionTarget)
    {
        region.Views.CollectionChanged += this.Views_CollectionChanged;
        this.regionTarget = regionTarget;
    }

    /// <summary>
    /// Creates a new instance of the SingleActiveRegion class.
    /// </summary>
    /// <returns>The created region.</returns>
    protected override IRegion CreateRegion()
    {
        return new SingleActiveRegion();
    }

    /// <summary>
    /// Event handler for the CollectionChanged event of the region's Views collection.
    /// Handles adding and removing views from the StackPanel region.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void Views_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            foreach (UserControl item in e.NewItems)
            {
                var title = this.GetItemTItle(item.DataContext);
                var groupBox = new GroupBox
                {
                    Header = title,
                    Content = item,
                };

                this.regionTarget.Children.Add(groupBox);
                Logger.Debug($"GroupBox '{title}' added.");
            }
        }
        else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
        {
            foreach (UserControl item in e.OldItems)
            {
                var title = this.GetItemTItle(item.DataContext);
                var itemToRemove =
                    this.regionTarget.Children
                                     .OfType<GroupBox>()
                                     .FirstOrDefault(n => n.Content == item);

                this.regionTarget.Children.Remove(itemToRemove);
                Logger.Debug($"GroupBox '{title}' removed.");
            }
        }
    }

    /// <summary>
    /// Gets the title of an item by accessing its "Title" property using reflection.
    /// </summary>
    /// <param name="obj">The object from which to get the title.</param>
    /// <returns>The title of the item.</returns>
    private string GetItemTItle(object obj)
        => obj.GetType().GetProperty("Title").GetValue(obj).ToString();
}