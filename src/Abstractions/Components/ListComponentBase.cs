namespace Aim.CustomerSelfService.Core;

using Microsoft.AspNetCore.Components;

using Web.Utility.Abstractions.ViewModels;

public abstract class ListComponentBase<T> : ComponentBase where T : ViewModelBase
{
    [Parameter] public EventCallback OnAdd { get; set; }
    [Parameter] public EventCallback<T> OnEdit { get; set; }
    [Parameter] public EventCallback<T> OnDelete { get; set; }

    protected abstract void HandleAdd();
    protected abstract void HandleEdit(T model);
    protected abstract void HandleDelete(T model);
}
