using ConsoleApp1;


//Erreur, il n'est pas possible d'instancier une classe abstraite.
//Resource resource = new Resource();

// Par contre, il est possible d'instancier une classe dérivée concrète.
// Une référence de type Resource peut référencer une instance d'une classe dérivée concrète.
Resource resource = new HumanResource()
{
    Name = "John Doe",
    Reference = "HR001",
    Role = "Developer"
};

MaterialResource materialResource = new MaterialResource()
{
    Name = "Laptop",
    Reference = "MR001"
};

// Il n'est pas possible de convertir une référence de type Resource en une référence de type HumanResource.
//HumanResource resourceHuman = resource;

// Il faut utiliser un transtypage explicite (cast).
// Attention, si la référence resource ne référence pas une instance de HumanResource, une exception InvalidCastException sera levée à l'exécution.
HumanResource humanResource = (HumanResource)resource;

// Il est possible de vérifier le type de l'objet référencé avant le transtypage avec le mot clef as.
// Si la conversion est impossible, la variable resourceHuman2 vaudra null au lieu de lever une exception.
HumanResource? resourceHuman2 = resource as HumanResource;

// L'appel de la méthode ToString() va utiliser la redéfinition de HumanResource.
Console.WriteLine(humanResource.ToString());
// L'appel de la méthode ToString() va utiliser la redéfinition de HumanResource, même si la référence est de type Resource.
Console.WriteLine(resource.ToString());
// L'appel de la méthode ToString() va utiliser la méthode Resource.ToString() car MaterialResource n'a pas redéfini cette méthode.
Console.WriteLine(materialResource.ToString());

// L'appel de la méthode MemberwiseClone() va utiliser l'implémentation de HumanResource (masquage).
HumanResource resourceHumanClone = humanResource.MemberwiseClone();

// Cependant, en utilisant la référence de type Resource l'appel de la méthode MemberwiseClone() va utiliser l'implémentation de Resource, même si l'instance est de type HumanResource.
// Ce n'est pas un comportement de polymorphisme, mais un masquage de méthode.
// Il est possible d'utiliser le transtypage pour appeler la méthode MemberwiseClone() de HumanResource.
HumanResource humanResourceClone1 = humanResource.MemberwiseClone();                // Appel HumanResource.MemberwiseClone()
HumanResource? humanResourceClone2 = (resource as HumanResource)?.MemberwiseClone();    // Transtypage explicite puis appel HumanResource.MemberwiseClone()
Resource resourceClone = resource.MemberwiseClone();                                // Appel Resource.MemberwiseClone()
