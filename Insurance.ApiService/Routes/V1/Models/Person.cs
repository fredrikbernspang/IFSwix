namespace InsuranceInfo.Models;

public record Person(
    string PersonalIdentificationNumber,
    string FirstName,
    string LastName,
    string Address,
    string City,
    string PostalCode,
    List<Insurance> Insurances
);

public static class PersonData
{
    public static readonly Dictionary<string, Person> Persons = new()
    {
        ["19801231-1234"] = new Person("19801231-1234", "John", "Doe", "Storgatan 1", "Stockholm", "111 22", new List<Insurance>
        {
            InsuranceData.CarInsurance  with
            {
                LicensePlate = "ABC123"
            },
            InsuranceData.PersonalHealthInsurance
        }),
        ["19951231-5678"] = new Person("19951231-5678", "Jane", "Doe", "Lillgatan 2", "Gothenburg", "411 23", new List<Insurance>
        {
            InsuranceData.PetInsurance
        }),
        ["20001231-9012"] = new Person("20001231-9012", "Max", "Mustermann", "Huvudgatan 3", "Malmo", "211 24", new List<Insurance>
        {
            InsuranceData.CarInsurance with
            {
                LicensePlate = "DEF456"
            },
            InsuranceData.PetInsurance
        }),
        ["19850505-1357"] = new Person("19850505-1357", "Erika", "Mustermann", "Sidogatan 4", "Uppsala", "753 23", new List<Insurance>
        { }),
        ["19900505-2468"] = new Person("19900505-2468", "Hans", "Muster", "Kungsleden 5", "Vasteras", "721 30", new List<Insurance>
        {
            InsuranceData.PersonalHealthInsurance
        })
    };
}