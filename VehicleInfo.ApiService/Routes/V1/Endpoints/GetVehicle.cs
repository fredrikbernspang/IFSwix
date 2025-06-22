
using VehicleInfo.Models;
public static class GetVehicle
{

    public static Vehicle? Single(string licensePlate)
    {
        return VehicleData.Vehicles.GetValueOrDefault(licensePlate);
    }

    public static List<Vehicle> All()
    {
        return VehicleData.Vehicles.Values.ToList();
    }

}