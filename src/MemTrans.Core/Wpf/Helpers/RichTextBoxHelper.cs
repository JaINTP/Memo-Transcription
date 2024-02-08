// ***********************************************************************
// Assembly         : MemTrans.Core
// Author           : Jai Brown
// Created          : 7-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 7-2-2024
// ***********************************************************************
// <copyright file="RichTextBoxHelper.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Core.Wpf.Helpers;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

/// <summary>
/// A helper class for the RichTextBox control in WPF.
/// </summary>
public static class RichTextBoxHelper
{
    /// <summary>
    /// The bindable text property.
    /// </summary>
    public static readonly DependencyProperty BindableTextProperty =
        DependencyProperty.RegisterAttached(
            "BindableText",
            typeof(string),
            typeof(RichTextBoxHelper),
            new FrameworkPropertyMetadata(
                string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnBindableTextChanged));

    /// <summary>
    /// Gets the bindable text value.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <returns>The bindable text value.</returns>
    public static string GetBindableText(DependencyObject obj)
    {
        return (string)obj.GetValue(BindableTextProperty);
    }

    /// <summary>
    /// Sets the bindable text value.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="value">The bindable text value.</param>
    public static void SetBindableText(DependencyObject obj, string value)
    {
        obj.SetValue(BindableTextProperty, value);
    }

    /// <summary>
    /// Handles the change of the bindable text property.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event arguments.</param>
    private static void OnBindableTextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        if (obj is RichTextBox richTextBox)
        {
            richTextBox.Document.Blocks.Clear();
            richTextBox.Document.Blocks.Add(new Paragraph(new Run(e.NewValue.ToString())));
        }
    }
}