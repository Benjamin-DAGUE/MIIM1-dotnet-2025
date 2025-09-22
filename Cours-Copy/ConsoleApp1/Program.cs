
using ConsoleApp1;

Resource boss = new()
{
    Reference = "B001",
    Name = "Alice",
    Manager = null
};
Console.WriteLine($"Création du manager {boss}");

Resource employee = new()
{
    Reference = "E001",
    Name = "Bob",
    Manager = boss
};

Console.WriteLine($"Création de l'employé {employee}");

Console.WriteLine();
Console.WriteLine($"Création d'une copie superficielle de l'employé");
Resource shallowCopy = employee.ShallowClone();
shallowCopy.Reference = "E002";
shallowCopy.Name = "Charlie";

Console.WriteLine("Original Employee:");
Console.WriteLine(employee);

Console.WriteLine("Shallow Copied Employee:");
Console.WriteLine(shallowCopy);

Console.WriteLine("Modification du nom du manager dans la copie superficielle");

if (shallowCopy.Manager is not null)
{
    shallowCopy.Manager.Name = "Alicia"; // Avec une copie superficielle, cela affecte aussi le manager de l'employé originel
}

Console.WriteLine("Original Employee:");
Console.WriteLine(employee);

Console.WriteLine("Shallow Copied Employee:");
Console.WriteLine(shallowCopy);

Console.WriteLine("Cela montre qu'il n'y a qu'une seule instance partagée entre les deux employés");


Console.WriteLine();
Console.WriteLine($"Création d'une copie profondee de l'employé");
Resource deepCopy = employee.DeepClone();
deepCopy.Reference = "E003";
deepCopy.Name = "Jean";

Console.WriteLine("Original Employee:");
Console.WriteLine(employee);

Console.WriteLine("Deep Copied Employee:");
Console.WriteLine(deepCopy);

Console.WriteLine("Modification du nom du manager dans la copie profonde");

if (deepCopy.Manager is not null)
{
    deepCopy.Manager.Name = "Ben"; // Avec une copie profonde, cela n'affecte pas le manager de l'employé originel
}

Console.WriteLine("Original Employee:");
Console.WriteLine(employee);

Console.WriteLine("Deep Copied Employee:");
Console.WriteLine(deepCopy);

Console.WriteLine("Cela montre qu'il y a deux instances différente dans Manager entre les deux employés");

Console.ReadLine();