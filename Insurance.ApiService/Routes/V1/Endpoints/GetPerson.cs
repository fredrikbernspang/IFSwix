using InsuranceInfo.Models;
using Newtonsoft.Json;

public static class GetPerson
{
    public static async Task<Person?> SingleAsync(IHttpClientFactory httpClientFactory, string id, bool useMockVehicleApi = false)
    {
        var person = PersonData.Persons.GetValueOrDefault(id);
        if (person == null)
        {
            return null;
        }

        foreach (var insurance in person.Insurances.OfType<CarInsurance>())
        {
            if (useMockVehicleApi)
            {
                // Provide mock vehicle data
                insurance.Vehicle = new VehicleInfo.Models.Vehicle(
                    insurance.LicensePlate,
                    "MockMake",
                    "MockModel",
                    2020,
                    "Gray",
                    "Petrol",
                    12345
                );
            }
            else
            {
                var client = httpClientFactory.CreateClient("vehicleInfoApiService");
                var response = await client.GetAsync($"/api/v1/vehicle/{insurance.LicensePlate}");
                var vehicleJson = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(vehicleJson))
                {
                    var vehicle = JsonConvert.DeserializeObject<VehicleInfo.Models.Vehicle>(vehicleJson);
                    insurance.Vehicle = vehicle;
                }
            }
        }
        return person;
    }

    public static List<Person> All()
    {
        return PersonData.Persons.Values.ToList();
    }
}