namespace Web.Utility.Interfaces;

public interface IUpdateable
{
    string Id { get; init; }

    DateTime UpdateDate { get; init; }

    string UpdateBy { get; init; }
}
