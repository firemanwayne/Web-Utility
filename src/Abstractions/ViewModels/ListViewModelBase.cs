namespace Web.Utility.Abstractions.ViewModels;

using System.ComponentModel;
using System.Runtime.CompilerServices;

/// <summary>
/// View mode base class.
/// </summary>
public abstract class ListViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public virtual List<string> ToolbarItems { get => ["Add", "Edit", "Delete", "Update", "Cancel"]; }

    public void OnPropertyChanged([CallerMemberName] string property = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}

/// <summary>
/// View mode base class.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class ListViewModelBase<T> : ListViewModelBase where T : ViewModelBase
{
    readonly HashSet<T> _viewModels = [];

    protected ListViewModelBase() { }

    protected ListViewModelBase(IEnumerable<T> viewModels)
    {
        foreach (T model in viewModels)
        {
            _viewModels.Add(model);
        }

        OnPropertyChanged();
    }

    public HashSet<T> ViewModels => [.. _viewModels.OrderByDescending(a => a.UpdateDate)];

    public void AddViewModel(T model)
    {
        if (_viewModels.Add(model))
        {
            OnPropertyChanged();
        }
    }

    public void RemoveViewModel(T model)
    {
        if (_viewModels.Remove(model))
        {
            OnPropertyChanged();
        }
    }

    public void ClearViewModels()
    {
        _viewModels.Clear();

        OnPropertyChanged();
    }
}
