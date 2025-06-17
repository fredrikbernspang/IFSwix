namespace VehicleInfo.Models;

public record Vehicle(
    string RegistrationNumber,
    string Make,
    string Model,
    int Year,
    string Color,
    string FuelType,
    int Mileage
);

public static class VehicleData
{
    public static readonly Dictionary<string, Vehicle> Vehicles = new()
    {
        ["ABC123"] = new Vehicle("ABC123", "Toyota", "Camry", 2022, "Silver", "Hybrid", 25000),
        ["XYZ789"] = new Vehicle("XYZ789", "BMW", "X5", 2021, "Black", "Petrol", 35000),
        ["DEF456"] = new Vehicle("DEF456", "Tesla", "Model 3", 2023, "White", "Electric", 12000),
        ["GHI321"] = new Vehicle("GHI321", "Honda", "Civic", 2020, "Blue", "Petrol", 45000),
        ["JKL654"] = new Vehicle("JKL654", "Audi", "A4", 2022, "Red", "Diesel", 28000)
    };
}