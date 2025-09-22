namespace ConsoleApp1;

// MaterialResource hérite de Resource.
// MaterialResource n'est pas une classe abstraite, elle doit donc implémenter toutes les méthodes abstraites de la classe de base.
// La classe MaterialResource est marquée comme sealed, elle ne peut pas être dérivée.
public sealed class MaterialResource : Resource
{
    #region Methods

    // Redéfinition de la méthode abstraite DoWork de la classe Resource.
    public override void DoWork()
    {
        Console.WriteLine($"Material resource is doing something.");
    }

    // La méthode MemberwiseClone masque la méthode MemberwiseClone de la classe Resource.
    public new MaterialResource MemberwiseClone()
    {
        return new()
        {
            Name = Name,
            Reference = Reference
        };
    }

    #endregion
}