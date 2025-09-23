namespace ConsoleApp1;

internal class Contact
{
    private string _hiddenField = "test";
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public override string ToString() => $"{FirstName} {LastName}";
}
