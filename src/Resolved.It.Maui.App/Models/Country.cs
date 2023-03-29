namespace Resolved.It.Maui.App.Models;

public class Country
{
    private Country(Guid id, string name, string code)
    {
        Id = id;
        Name = name;
        Code = code;
    }
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }

    public static Country Create(
        Guid id,
        string name,
        string code)
    {
        var country = new Country(
            id,
            name,
            code
        );
        return country;
    }

    public static Country Germany
        => Create(new Guid("BFD46FFA-3173-4C15-8AF3-9E2F3B8FFC32"), "Deutschland", "DE");
    public static Country French
        => Create(new Guid("2B08F8F1-3E3C-4B14-9C41-CB82C90BD7F5"), "Frankreich", "FR");
    public static Country Austria
        => Create(new Guid("C5EA5E69-8E83-4E38-B946-50D1A2493A76"), "Österreich", "AT");

    public static List<Country> GetCountryList()
    {
        return new List<Country>
        {
            Germany, French, Austria
        };
    }
}

