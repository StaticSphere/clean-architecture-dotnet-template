namespace CleanArchTemplate.Application.Endpoints.People;

public record PersonViewModel
{
    public int? Id { get; init; }
    public string FirstName { get; init; } = "";
    public string LastName { get; init; } = "";
    public bool Active { get; init; }
    public int CreatedBy { get; init; }
    public DateTime CreatedOn { get; init; }
    public int? ModifiedBy { get; init; }
    public DateTime? ModifiedOn { get; init; }
}

