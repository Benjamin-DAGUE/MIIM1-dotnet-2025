namespace ConsoleApp1;

public class HumanResource : Resource
{
    #region Properties
    
    public string Role { get; set; } = string.Empty;

    #endregion

    #region Methods

    // Redéfinition de la méthode abstraite DoWork de la classe Resource.
    // HumanResource n'est pas une classe abstraite, elle doit donc implémenter toutes les méthodes abstraites de la classe de base.
    // Le mot-clé sealed empêche toute classe dérivée de redéfinir à nouveau cette méthode.
    public sealed override void DoWork()
    {
        Console.WriteLine($"Human resource {Name} is working as {Role}.");
    }

    public override string? ToString()
    {
        return $"{base.ToString()} - {Role}";
    }

    // La méthode MemberwiseClone masque la méthode MemberwiseClone de la classe Resource.
    public new HumanResource MemberwiseClone()
    {
        return new()
        {
            Name = Name,
            Reference = Reference,
            Role = Role
        };
    }

    #endregion
}