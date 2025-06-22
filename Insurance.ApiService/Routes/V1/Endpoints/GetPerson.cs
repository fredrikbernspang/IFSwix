using InsuranceInfo.Models;
using Newtonsoft.Json;
public static class GetPerson
{

    public static async Task<Person?> SingleAsync(IHttpClientFactory httpClientFactory, string id)
    {
        var person = PersonData.Persons.GetValueOrDefault(id);
        if (person == null)
        {
            return null;
        }

        // If the person has car insurances, we can fetch the vehicle details from the VehicleInfo API
        foreach (var insurance in person.Insurances.OfType<CarInsurance>())
        {
            var client = httpClientFactory.CreateClient("vehicleInfoApiService");
            Console.WriteLine($"VehicleInfoApiService BaseAddress: {client.BaseAddress}");
            var response = await client.GetAsync($"/api/v1/vehicle/{insurance.LicensePlate}");
            var vehicleJson = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(vehicleJson))
            {
                var vehicle = JsonConvert.DeserializeObject<VehicleInfo.Models.Vehicle>(vehicleJson);
                insurance.Vehicle = vehicle;
            }
        }
        return person;
    }

    public static List<Person> All()
    {
        return PersonData.Persons.Values.ToList();
    }

}