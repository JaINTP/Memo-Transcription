// ***********************************************************************
// Assembly         : MemTrans.Modules.UI
// Author           : Jai Brown
// Created          : 8-2-2024
//
// Last Modified By : Jai Brown
// Last Modified On : 8-2-2024
// ***********************************************************************
// <copyright file="WarningDialogViewModel.cs" company="Jai Brown">
//     Copyright (c) 2024 Jai Brown. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace JaINTP.MemTrans.Modules.UI.ViewModels;

using System.Linq;
using JaINTP.MemTrans.Core.Mvvm;
using JaINTP.MemTrans.Services.Interfaces;

/// <summary>
/// Represents a view model for a Yes/No dialog.
/// </summary>
public class WarningDialogViewModel
    : ViewModelBase, IDialogAware
{
    private readonly ILocalizationService locService;

    private DelegateCommand<string> closeDialogCommand;
    private string title;
    private string string_Ok;
    private string message;

    /// <summary>
    /// Initializes a new instance of the <see cref="WarningDialogViewModel"/> class.
    /// </summary>
    /// <param name="regionManager">The region manager.</param>
    /// <param name="locService">The localization service.</param>
    public WarningDialogViewModel(IRegionManager regionManager, ILocalizationService locService)
        : base(regionManager)
    {
        this.locService = locService;
        this.SetupLocalization();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WarningDialogViewModel"/> class.
    /// Dirty hack constructor to allow design time data to be shown in the XAML designer.
    /// </summary>
    private WarningDialogViewModel()
        : base(null)
    {
    }

    /// <summary>
    /// Gets the design time instance of the view model.
    /// </summary>
    public static WarningDialogViewModel DesignInstance => new WarningDialogViewModel
    {
        Title = "Warning",
        Message = "This is a warning message.",
        String_Ok = "Ok",
    };

    /// <summary>
    /// Gets or sets the string for "Ok".
    /// </summary>
    public string String_Ok
    {
        get => this.string_Ok;
        set => this.SetProperty(ref this.string_Ok, value);
    }

    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    public string Message
    {
        get => this.message;
        set => this.SetProperty(ref this.message, value);
    }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    public string Title
    {
        get => this.title;
        set => this.SetProperty(ref this.title, value);
    }

    /// <summary>
    /// Gets the command to close the dialog.
    /// </summary>
    public DelegateCommand<string> CloseDialogCommand =>
        this.closeDialogCommand ??= new DelegateCommand<string>(this.CloseDialog);

    /// <summary>
    /// Gets the listener for dialog close events.
    /// </summary>
    public DialogCloseListener RequestClose { get; }

    /// <summary>
    /// Determines whether the dialog can be closed.
    /// </summary>
    /// <returns><c>true</c> if the dialog can be closed; otherwise, <c>false</c>.</returns>
    public virtual bool CanCloseDialog()
    {
        return true;
    }

    /// <summary>
    /// Called when the dialog is closed.
    /// </summary>
    public virtual void OnDialogClosed()
    {
        // TODO: Do something.
    }

    /// <summary>
    /// Called when the dialog is opened.
    /// </summary>
    /// <param name="parameters">The dialog parameters.</param>
    public void OnDialogOpened(IDialogParameters parameters)
    {
        this.Title = parameters.GetValue<string>("Title");
        this.Message = parameters.GetValue<string>("Message");
    }

    /// <summary>
    /// Raises the request to close the dialog.
    /// </summary>
    /// <param name="dialogResult">The dialog result.</param>
    public virtual void RaiseRequestClose(IDialogResult dialogResult)
    {
        this.RequestClose.Invoke(dialogResult);
    }

    /// <summary>
    /// Closes the dialog.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    protected virtual void CloseDialog(string parameter)
    {
        var result = ButtonResult.None;

        if (parameter?.ToLower() == "true")
        {
            result = ButtonResult.OK;
        }

        this.RaiseRequestClose(new DialogResult(result));
    }

    /// <summary>
    /// Sets up localization for the view model.
    /// </summary>
    private void SetupLocalization()
    {
        foreach (var configProp in this.GetType()
                                       .GetProperties()
                                       .Where(p => p.Name.Contains("String_")))
        {
            configProp.SetValue(this, this.locService.Localize(configProp.Name, (string)configProp.GetValue(this)));
        }
    }
}