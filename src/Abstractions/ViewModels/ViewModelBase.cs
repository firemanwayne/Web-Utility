namespace Web.Utility.Abstractions.ViewModels;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text.Json.Serialization;

using Web.Utility.Extensions;
using Web.Utility.Interfaces;

public abstract class ViewModelBase : IUpdateable, INotifyPropertyChanged
{
    /// <summary>
    /// Constructor based off a new entity.
    /// </summary>
    /// <param name="user"></param>
    protected ViewModelBase(ClaimsPrincipal user)
    {
        Id = Guid.NewGuid().ToString();
        IsNew = true;
        CurrentUser = user;
        UpdateBy = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
    }

    /// <summary>
    /// Constructor based off an existing entity.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="user"></param>
    protected ViewModelBase(string id, ClaimsPrincipal user)
    {
        Id = id;
        IsNew = false;
        CurrentUser = user;
    }

    /// <summary>
    /// Constructor based off an existing entity.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="user"></param>
    protected ViewModelBase(Guid id, ClaimsPrincipal user)
    {
        Id = id.ToString();
        IsNew = false;
        CurrentUser = user;
    }

    /// <summary>
    /// Id of the entity. BASE CLASS PROPERTY.
    /// </summary>
    [Required, Display(Name = "Id")]
    public string Id { get; init; }

    /// <summary>
    /// DateTime model instance was updated. BASE CLASS PROPERTY.
    /// </summary>
    [Required, Display(Name = "Update Date")]
    public DateTime UpdateDate { get; init; }

    /// <summary>
    /// User who updated the model instance. BASE CLASS PROPERTY.
    /// </summary>
    [StringLength(100), Display(Name = "Update By")]
    public string UpdateBy { get; init; } = string.Empty;

    /// <summary>
    /// Convenience property to determine if the instance 
    /// is a new model or an existing model
    /// </summary>
    [Display(Name = "Is New?")]
    public bool IsNew { get; set; }

    public bool SpinnerVisible { get; set; }

    public bool DisableSpinnerButton { get; set; }

    public string AddEditButtonText => IsNew ? "Submit" : "Update";

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string property = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }

    public void ShowSpinnerAndDisableButton()
    {
        SpinnerVisible = true;
        DisableSpinnerButton = true;

        OnPropertyChanged();
    }

    public void HideSpinnerAndEnableButton()
    {
        SpinnerVisible = false;
        DisableSpinnerButton = false;

        OnPropertyChanged();
    }

    /// <summary>
    /// Convenience property to set the url segment of index page
    /// </summary>
    public static string IndexSegment { get; set; } = string.Empty;

    [JsonIgnore] public ClaimsPrincipal CurrentUser { get; set; }

    public override int GetHashCode() => HashCode.Combine(Id, UpdateDate);
}

public abstract class ViewModelBase<T> : ViewModelBase where T : IUpdateable
{
    /// <summary>
    /// Constructor based off a new entity
    /// </summary>
    /// <param name="user"></param>
    protected ViewModelBase(ClaimsPrincipal user) : base(user)
    {
        string? userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

        UpdateBy = userId ?? string.Empty;

        UpdateDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Constructor based off an existing entity
    /// </summary>
    /// <param name="e"></param>
    /// <param name="user"></param>
    protected ViewModelBase(T e, ClaimsPrincipal user) : base(e.Id, user)
    {
        string? userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

        ArgumentException.ThrowIfNullOrEmpty(userId, nameof(user));

        UpdateBy = string.IsNullOrWhiteSpace(e.UpdateBy) ? userId : e.UpdateBy;

        UpdateDate = e.UpdateDate;
    }

    /// <summary>
    /// Method for generating an updated entity based on view model values. This method is where you update the <typeparamref name="UpdateDate"/>
    /// </summary>
    /// <returns></returns>
    public abstract T GenerateUpdatedEntity();
}
