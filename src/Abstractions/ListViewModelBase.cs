namespace Web.Utility.Abstractions;

using System.ComponentModel;
using System.Runtime.CompilerServices;

/// <summary>
/// View mode base class. USE IF YOUR View model REQUIRES DROP DOWNS.
/// </summary>
public abstract class ListViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public virtual List<string> ToolbarItems { get => new() { "Add", "Edit", "Delete", "Update", "Cancel" }; }

    public void OnPropertyChanged([CallerMemberName] string property = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}

/// <summary>
/// View mode base class. DO NOT USE IF YOUR View model REQUIRES DROP DOWNS.
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

    public HashSet<T> ViewModels => _viewModels.OrderByDescending(a => a.UpdateDate).ToHashSet();

    public void AddViewModel(T model)
    {
        if (_viewModels.Add(model))
        {
            OnPropertyChanged();
        }
        else
        {
            Console.WriteLine("DUPLICATE");
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
