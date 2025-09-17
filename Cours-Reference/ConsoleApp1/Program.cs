using System.Reflection.Metadata;

namespace ConsoleApp1;

internal class Contact
{
    public string? Name { get; set; }

    public Contact()
    {

    }
}

internal class Program
{
    //Lors de l'appel de Main, le tas est initialisé et une pile est créée pour Main.
    //Les arguments de Main sont mis en mémoire (la pile et le tas) dès le début de la méthode.
    static void Main(string[] args)
    {
        //L'appel d'une méthode va créé une pile spécifique pour cet appel
        TypeValeur();
        //Cette pile est détruite à la fin de l'exécution de la méthode

        TypeReference();

        TypeString();

        Console.ReadKey();
    }

    static void TypeValeur()
    {
        Console.Clear();

        //Les structures sont des types valeurs
        //il n'y a pas de référence et les instances sont ajoutées dans la pile et non pas dans le tas.
        int typeValeur = 665;

        Increment(typeValeur); 

        //Ici l'incrément ne fonctionne pas
        Console.WriteLine(typeValeur);

        //La syntaxe oblige l'écriture de ref pour indiquer que la méthode a la capacité de modifier le type valeur passé en paramètre.
        IncrementByRef(ref typeValeur); 

        //Ici l'incrément fonctionne
        Console.WriteLine(typeValeur);

        Console.Write("Appuyer sur une touche pour continuer...");
        Console.ReadKey();
    }

    static void Increment(int toIncrement) //L'appel de la méthode créé une pile
    {
        //Cette version d'Increment attend en paramètre une structure (int) et donc un type valeur.
        //Ceci signifie que la pile de la méthode va contenir une COPIE de l'instance passée en paramètre.
        //L'instance incrémentée sera celle de la pile de Increment
        toIncrement = toIncrement + 1;

        //La pile est détruite à la fin de la méthode et l'instance qui a été incrémentée va être supprimée
    }

    static void IncrementByRef(ref int toIncrement) //L'appel de la méthode créé une pile
    {
        //Cette version d'Increment attend en paramètre une REFERENCE vers une structure (int), ce n'est plus un type valeur mais un type référence.
        //Ceci signifie que la pile de la méthode va contenir une référence qui pointe vers l'instance passée en paramètre par l'appelant.
        //L'instance incrémentée sera celle provenant de la pile de l'appelant.
        toIncrement = toIncrement + 1;

        //La pile est détruite à la fin de la méthode, la référence passée en paramètre est alors supprimée.
    }

    static void TypeReference()
    {
        Console.Clear();

        //Les classes sont des types références
        //Une référence est créée dans la pile et elle est affectée à une instance créée dans le tas.
        //<Reference> = <Instance>
        Contact contact = new Contact();

        SetName(contact);

        //L'instance du contact a bien été modifié.
        Console.WriteLine(contact.Name);
    }

    static void SetName(Contact contact) // Ici, l'appel de la méthode créé une pile qui contient une référence qui point vers l'instance passée par l'appelant.
    {
        //L'accès à l'instance par la référence va bien donner accès à l'instance fournie par l'appelant sous la forme d'une référence.
        Console.WriteLine("Veuiller saisir le nom du contact :");
        contact.Name = Console.ReadLine();
    }

    static void TypeString()
    {
        //string est une classe, c'est donc un type référence.
        //string est un cas particulier car pour des raisons techniques, l'instance d'un string est immuable.
        //La modification de l'instance n'est donc pas possible et engendre la création d'une nouvelle instance dans le tas.
        string name = "toto";

        ToUpperNotWorking(name); //Cette version ne modifie pas l'instance "toto" référencée par name

        Console.WriteLine(name);

        name = ToUpperReturn(name); //Cette version fonctionne.

        Console.WriteLine(name);

        name = "toto";

        ToUpperByRef(ref name); //Cette version fonctionne.

        Console.WriteLine(name);
    }

    static void ToUpperNotWorking(string toModify) // On obtient ici une référence qui pointe vers une instance du tas
    {
        //Il n'est pas possible de modifier une instance.
        //ToUpper a créé une nouvelle instance dans le tas et en retourne une référence.
        //On a alors deux instances dans le tas, celle créée initialement dans TypeString() ("toto") et celle retournée par ToUpper() ("TOTO").
        //la référence toModifiy pointe désormais sur cette nouvelle instance ("TOTO").
        toModify = toModify.ToUpper();

        //A la fin de la méthode, toModify est la dernière référence qui pointe vers cette nouvelle instance ("TOTO")
        //Le GC va alors plus tard nettoyer cette instance. puisque la pile de la méthode actuelle va être supprimée.
    }

    static string ToUpperReturn(string toModify)
    {
        //Cette version fonctionne car la méthode retourne une référence vers l'instance ("TOTO") créée par ToUpper)
        toModify = toModify.ToUpper();
        return toModify;
    }

    static void ToUpperByRef(ref string toModify) //ref permet de contourner cette limitation et indique qu'il faut modifier la référence de l'appelant.
    {
        //string est un type référence qui se comporte comme un type valeur
        toModify = toModify.ToUpper();
    }
}
