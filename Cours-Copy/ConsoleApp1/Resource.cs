namespace ConsoleApp1;

public class Resource
{
    public string Reference { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Resource? Manager { get; set; }

    // MemberwisClone créé une copie superficielle de l'objet courant.
    // Cela signifie que les champs de type valeur sont copiés directement,
    // tandis que les champs de type référence (comme Manager) ne sont pas clonés profondément.
    // Ainsi, dans la copie, Manager référencera le même objet que dans l'original (c'est la même instance).
    public Resource ShallowClone() => (Resource)MemberwiseClone();

    // DeepClone crée une copie profonde de l'objet courant.
    // Cela signifie que non seulement les champs de type valeur sont copiés,
    // mais aussi les champs de type référence sont clonés profondément.
    // Ainsi, dans la copie, Manager référencera une nouvelle instance qui est une copie de l'original.
    public Resource DeepClone()
    {
        Resource clone = (Resource)MemberwiseClone();
        if (Manager is not null)
        {
            clone.Manager = Manager.DeepClone();
        }
        return clone;
    }

    public override string ToString()
    {
        return $"Reference: {Reference}, Name: {Name}, Manager: [{Manager}]";
    }
}