using JobPlusWPF.Model.Classes;

public class JobSeeker
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public int Age { get; private set; }
    public string PassportNumber { get; private set; }
    public DateTime PassportIssueDate { get; private set; }
    public string PassportIssuedBy { get; private set; }
    public string Phone { get; private set; }
    public string Photo { get; private set; }
    public int CityId { get; private set; } // Свойство для связи с City
    public CityDirectory City { get; set; }
    public int StreetId { get; private set; } // Свойство для связи с Street
    public StreetDirectory Street { get; set; }
    public int EducationLevelId { get; private set; }
    public EducationLevel EducationLevel { get; set; }
    public string Institution { get; private set; }
    public string EducationDocumentScan { get; private set; }
    public string Specialty { get; private set; }
    public int WorkExperience { get; private set; }
    public DateTime RegistrationDate { get; private set; }
    public int UserId { get; private set; }
    public User User { get; private set; }

    public JobSeeker(string name, string surname, int age, string passportNumber, DateTime passportIssueDate, string passportIssuedBy,
                     string phone, string photo, int cityId, int streetId, int educationLevelId, string institution,
                     string educationDocumentScan, string specialty, int workExperience, DateTime registrationDate, int userId)
    {
        Name = name;
        Surname = surname;
        Age = age;
        PassportNumber = passportNumber;
        PassportIssueDate = passportIssueDate;
        PassportIssuedBy = passportIssuedBy;
        Phone = phone;
        Photo = photo;
        CityId = cityId;
        StreetId = streetId;
        EducationLevelId = educationLevelId;
        Institution = institution;
        EducationDocumentScan = educationDocumentScan;
        Specialty = specialty;
        WorkExperience = workExperience;
        RegistrationDate = registrationDate;
        UserId = userId;
    }

    public void SetId(int id)
    {
        Id = id;
    }

    public void Update(
    string name,
    string surname,
    int age,
    string passportNumber,
    DateTime passportIssueDate,
    string passportIssuedBy,
    string phone,
    string photo,
    int cityId,
    int streetId,
    int educationLevelId,
    string institution,
    string educationDocumentScan,
    string specialty,
    int workExperience,
    DateTime registrationDate)
    {
        Name = name;
        Surname = surname;
        Age = age;
        PassportNumber = passportNumber;
        PassportIssueDate = passportIssueDate;
        PassportIssuedBy = passportIssuedBy;
        Phone = phone;
        Photo = photo;
        CityId = cityId;
        StreetId = streetId;
        EducationLevelId = educationLevelId;
        Institution = institution;
        EducationDocumentScan = educationDocumentScan;
        Specialty = specialty;
        WorkExperience = workExperience;
        RegistrationDate = registrationDate;
    }


}
