namespace BusinessObjects;

public partial class Drink
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public bool Status { get; set; }

    public virtual DrinkCategory Category { get; set; } = null!;

    public virtual ICollection<DrinkVariation> DrinkVariations { get; set; } = new List<DrinkVariation>();

    public override string ToString()
    {
        return Name;
    }
}
