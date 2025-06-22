namespace InsuranceInfo.Models;

using Newtonsoft.Json;
using JsonSubTypes;
using VehicleInfo.Models;

[JsonConverter(typeof(JsonSubtypes), "Type")]
[JsonSubtypes.KnownSubType(typeof(CarInsurance), "Car Insurance")]
public record Insurance(
    Guid Id,
    string? PolicyNumber,
    string Type,
    DateTime? StartDate,
    DateTime? EndDate,
    int Price
);

public record CarInsurance(
    Guid Id,
    string? PolicyNumber,
    DateTime? StartDate,
    DateTime? EndDate,
    int Price,
    string LicensePlate
) : Insurance(Id, PolicyNumber, "Car Insurance", StartDate, EndDate, Price)
{
    public Vehicle? Vehicle { get; set; } 
    public string LicensePlate { get; set; } 
};

public static class InsuranceData
{
    public static readonly Insurance PetInsurance = new Insurance(
        Guid.NewGuid(),
        "PET-001",
        "Pet Insurance",
        DateTime.Today,
        DateTime.Today.AddYears(1),
        10
    );

    public static readonly Insurance PersonalHealthInsurance = new Insurance(
        Guid.NewGuid(),
        "HEALTH-001",
        "Personal Health Insurance",
        DateTime.Today,
        DateTime.Today.AddYears(1),
        20
    );

    public static readonly CarInsurance CarInsurance = new CarInsurance(
        Guid.NewGuid(),
        "CAR-001",
        DateTime.Today,
        DateTime.Today.AddYears(1),
        30,
        "ABC123"
    );
}