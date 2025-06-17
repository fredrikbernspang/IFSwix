
using VehicleInfo.Models;
public static class GetInsurance
{

    public static Vehicle? Single(string regnum)
    {
        return VehicleData.Vehicles.GetValueOrDefault(regnum);
    }

    public static List<Vehicle> All()
    {
        return VehicleData.Vehicles.Values.ToList();
    }

}