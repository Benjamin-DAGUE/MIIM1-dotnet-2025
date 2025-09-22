namespace ConsoleApp1;

// Une classe marquée comme abstract est abstraite : elle ne peut plus être instanciée directement.
// Ce type de classe doit être utilisée comme classe de base.
public abstract class Resource : Object //Par défaut, une classe hérite de la clase object.
{
    #region Properties

    public string Reference { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    #endregion

    #region Methods

    // Object définit une méthode ToString() qui est virtuelle.
    // Une méthode virtuelle peut être redéfinie dans une classe dérivée en utilisant le mot-clé override.
    // Une méthode virtuelle peut remplacer ou compléter le comportement de la méthode de la classe de base.
    // On dit que la méthode complète le comportement si elle utilise le mot-clé base pour appeler la méthode de la classe de base et lui ajouter du comportement.
    // On dit que la méthode remplace le comportement si elle ne fait pas appel à la méthode de la classe de base.
    public override string? ToString()
    {
        //Cette implémentation complète le comportement de la méthode ToString() de la classe Object.
        /*
        string? baseToString = base.ToString();
        return baseToString?.ToUpper();
        */

        //Cette implémentation remplace le comportement de la méthode ToString() de la classe Object.
        return $"{Reference} - {Name}";
    }

    // Une méthode abstraite n'a pas d'implémentation dans la classe de base.
    // Une méthode abstraite doit être redéfinie dans une classe dérivée en utilisant le mot-clé override.
    // Une classe qui contient une méthode abstraite doit être marquée comme abstract.
    public abstract void DoWork();

    // La méthode MemberwiseClone est une méthode protégée définie dans la classe Object.
    // Cette méthode n'est pas virtuelle, elle ne peut donc pas être redéfinie dans une classe dérivée.
    // Cependant, rien n'empêche de définir une nouvelle méthode MemberwiseClone dans une classe dérivée.
    // Le mot-clé new indique que cette méthode masque la méthode MemberwiseClone de la classe Object.
    // Ce mot-clé n'est pas obligatoire, mais il est recommandé de l'utiliser pour indiquer clairement l'intention de masquer la méthode de la classe de base.
    public new Resource MemberwiseClone()
    {
        throw new NotImplementedException();
    }

    #endregion
}