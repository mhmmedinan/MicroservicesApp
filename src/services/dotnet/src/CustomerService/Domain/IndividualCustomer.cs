namespace Domain;

public class IndividualCustomer:Customer
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Gender { get; set; }
    public string MotherName { get; set; }
    public string FatherName { get; set; }
    public string NationalityIdentity { get; set; }
    public DateTime BirthDate { get; set; }

    public IndividualCustomer()
    {
        
    }

    public IndividualCustomer(string firstName, string lastName, string middleName, string gender, string motherName, string fatherName, string nationalityIdentity, DateTime birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Gender = gender;
        MotherName = motherName;
        FatherName = fatherName;
        NationalityIdentity = nationalityIdentity;
        BirthDate = birthDate;
    }
}
