namespace Web.Utility.Abstractions.Components;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using Web.Utility.Abstractions.ViewModels;

public abstract class FormComponentBase<T> : ComponentBase where T : ViewModelBase
{
    /// <summary>
    /// Model used in the form
    /// </summary>
    [EditorRequired, Parameter] public T? ViewModel { get; set; }

    /// <summary>
    /// Callback for when the user submits a form
    /// </summary>
    [EditorRequired, Parameter] public EventCallback<T> OnValidSubmit { get; set; }

    [Parameter] public EventCallback<EditContext> OnInValidSubmit { get; set; }

    [Parameter] public EventCallback OnCancel { get; set; }

    [Parameter] public bool ShowSpinner { get; set; }

    [Parameter] public EventCallback<bool> ShowSpinnerValueChanged { get; set; }

    protected virtual async Task HandleValidSubmitAsync()
    {
        if (OnValidSubmit.HasDelegate && ViewModel is not null)
        {
            ShowSpinner = true;

            await ShowSpinnerValueChanged.InvokeAsync(ShowSpinner);

            await OnValidSubmit.InvokeAsync(ViewModel);

            ShowSpinner = false;

            await ShowSpinnerValueChanged.InvokeAsync(ShowSpinner);
        }
    }

    protected virtual async Task HandleInvalidSubmitAsync(EditContext context)
    {
        if (OnInValidSubmit.HasDelegate)
        {
            await OnInValidSubmit.InvokeAsync(context);
        }
    }

    protected void HandleCancel()
    {
        if (OnCancel.HasDelegate)
        {
            OnCancel.InvokeAsync();
        }
    }
}
