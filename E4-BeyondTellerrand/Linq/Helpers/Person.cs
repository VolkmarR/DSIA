namespace Demos.Helpers;

public class Person
{
    public int ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDay { get; set; }

    public List<Address> Addresses { get; set; }
}