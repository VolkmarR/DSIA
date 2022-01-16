namespace Demos.Linq2DB.DB;

public class PersonDB
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDay { get; set; }

    public virtual List<AddressDB> Addresses { get; set; }
}
