namespace BusinessObjects;

public partial class DrinkCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Drink> Drinks { get; set; } = new List<Drink>();
}
