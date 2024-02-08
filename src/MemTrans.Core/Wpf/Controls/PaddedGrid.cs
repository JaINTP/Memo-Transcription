// ***********************************************************************
// Assembly         : MemTrans.Core
// Author           : Jai Brown
// Created          : 6-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 6-2-2024
// ***********************************************************************
// <copyright file="PaddedGrid.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Core.Wpf.Controls;

using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

/// <summary>
/// Represents a custom Grid control with a Padding property that applies margin to its child elements.
/// </summary>
public class PaddedGrid
    : System.Windows.Controls.Grid
{
    private static readonly DependencyProperty PaddingProperty =
        DependencyProperty.Register(
            "Padding",
            typeof(Thickness),
            typeof(PaddedGrid),
            new UIPropertyMetadata(
                new Thickness(0.0),
                new PropertyChangedCallback(OnPaddingChanged)));

    /// <summary>
    /// Initializes a new instance of the <see cref="PaddedGrid"/> class.
    /// </summary>
    public PaddedGrid()
    {
        this.Loaded += this.PaddedGrid_Loaded;
    }

    /// <summary>
    /// Gets or sets the padding of the PaddedGrid.
    /// </summary>
    [Description("The padding property.")]
    [Category("Common Properties.")]
    public Thickness Padding
    {
        get => (Thickness)this.GetValue(PaddingProperty);
        set => this.SetValue(PaddingProperty, value);
    }

    /// <summary>
    /// Gets the DependencyProperty for the Margin property of the specified DependencyObject.
    /// </summary>
    /// <param name="dependencyObject">The DependencyObject to get the Margin DependencyProperty for.</param>
    /// <returns>The DependencyProperty for the Margin property.</returns>
    protected static DependencyProperty GetMarginProperty(DependencyObject dependencyObject)
    {
        foreach (var propDesc in TypeDescriptor.GetProperties(dependencyObject))
        {
            var depProcDesc = DependencyPropertyDescriptor.FromProperty(propDesc as PropertyDescriptor);

            if (depProcDesc != null && depProcDesc.Name == "Margin")
            {
                return depProcDesc.DependencyProperty;
            }
        }

        return null;
    }

    /// <summary>
    /// Handles the change of the Padding property.
    /// Updates the layout of the PaddedGrid.
    /// </summary>
    /// <param name="dependencyObject">The DependencyObject that triggered the change.</param>
    /// <param name="args">The arguments containing the old and new values of the property.</param>
    private static void OnPaddingChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        (dependencyObject as PaddedGrid).UpdateLayout();
    }

    /// <summary>
    /// Handles the Loaded event of the PaddedGrid control.
    /// Binds the Padding property to the Margin property of child elements.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The event arguments.</param>
    private void PaddedGrid_Loaded(object sender, RoutedEventArgs e)
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(this); i++)
        {
            var child = VisualTreeHelper.GetChild(this, i);
            var marginProperty = GetMarginProperty(child);

            if (marginProperty != null)
            {
                var binding = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath("Padding"),
                };

                BindingOperations.SetBinding(child, marginProperty, binding);
            }
        }
    }
}