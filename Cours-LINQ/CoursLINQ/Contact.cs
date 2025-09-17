namespace CoursLINQ;

internal class Contact
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime BirthDate { get; set; }
    public string FullName => FirstName + " " + LastName;
}
