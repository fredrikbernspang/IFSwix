using InsuranceInfo.Models;
public static class GetPerson
{

    public static Person? Single(string id)
    {
        var person = PersonData.Persons.GetValueOrDefault(id);
        if (person == null)
        {
            return null;
        }
/*
        for (int i = person.Insurances.Count - 1; i >= 0; i--)
        {
            if (person.Insurances[i] is CarInsurance car)
            {
                person.Insurances.RemoveAt(i);
                person.Insurances.Add(new CarInsurance(
                    car.Id,
                    car.PolicyNumber,
                    car.StartDate,
                    car.EndDate,
                    car.Price,
                    car.RegistrationNumber
                ));
            }
        }
        */
        return person;
    }

    public static List<Person> All()
    {
        return PersonData.Persons.Values.ToList();
    }

}