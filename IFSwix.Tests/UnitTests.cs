using InsuranceInfo.Models;
using Moq;
using Xunit;

namespace IFSwix.Tests.Tests;

public class UnitTests
{
    [Fact]
    public async Task SingleAsync_ReturnsNull_WhenPersonDoesNotExist()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        PersonData.Persons.Clear(); // Empty dictionary

        // Act
        var result = await GetPerson.SingleAsync(httpClientFactoryMock.Object, "nonexistent-id");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SingleAsync_ReturnsPerson_WhenPersonExists()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var person = new Person("12345", "Test", "Dummy", "DevNull 1", "Nerdtown", "111 22", new List<Insurance>());
        PersonData.Persons.Clear();
        PersonData.Persons.Add("12345", person);

        // Act
        var result = await GetPerson.SingleAsync(httpClientFactoryMock.Object, "12345");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("12345", result.PersonalIdentificationNumber);
    }

    [Fact]
    public async Task SingleAsync_SetsMockVehicleData_WhenUseMockVehicleApiIsTrue()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var carInsurance = new CarInsurance(Guid.NewGuid(), "CAR-001", DateTime.Today, DateTime.Today.AddYears(1), 30) with { LicensePlate = "ABC123" };
        var person = new Person("54321", "Mock", "User", "Some Address", "Some City", "222 33", new List<Insurance> { carInsurance });
        PersonData.Persons.Clear();
        PersonData.Persons.Add("54321", person);

        // Act
        var result = await GetPerson.SingleAsync(httpClientFactoryMock.Object, "54321", useMockVehicleApi: true);

        // Assert
        var returnedCarInsurance = Assert.IsType<CarInsurance>(result!.Insurances[0]);
        Assert.NotNull(returnedCarInsurance.Vehicle);
        Assert.Equal("MockMake", returnedCarInsurance.Vehicle.Make);
        Assert.Equal("MockModel", returnedCarInsurance.Vehicle.Model);
        Assert.Equal(2020, returnedCarInsurance.Vehicle.Year);
    }
}