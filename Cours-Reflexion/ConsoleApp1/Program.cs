using System.Reflection;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // La réflexion est la capacité d'un programme à s'examiner et à modifier sa propre structure et son comportement à l'exécution.
            // En C#, la réflexion est principalement réalisée à l'aide de l'espace de noms System.Reflection.
            // Elle permet d'inspecter les types, les membres (propriétés, méthodes, champs), et de créer des instances de types dynamiquement.
            // Cela est particulièrement utile pour les scénarios où les types ne sont pas connus à la compilation, comme dans les frameworks de sérialisation, les ORM, ou les systèmes de plugins.
            // Il est important de noter que l'utilisation de la réflexion peut avoir un impact sur les performances et la sécurité, car elle contourne certaines vérifications de type effectuées à la compilation.

            // On peut obtenir un objet Type de plusieurs façons :
            Type type = typeof(Contact);
            type = Type.GetType("ConsoleApp1.Contact") ?? throw new InvalidOperationException();
            type = new Contact().GetType();
            type = Assembly.GetExecutingAssembly().GetType("ConsoleApp1.Contact") ?? throw new InvalidOperationException();

            // On peut créer une instance d'un type de plusieurs façons :
            ConstructorInfo? constructor = type.GetConstructor([]);
            object? contact = constructor?.Invoke(null);
            contact = Activator.CreateInstance(type); // Cette méthode est à privilégier car plus performante

            // Le FullName d'un type inclut le namespace
            Console.WriteLine(type.FullName);

            // On peut obtenir les propriétés d'un type
            foreach (PropertyInfo prop in type.GetProperties())
            {
                Guid guid = Guid.NewGuid();
                Console.WriteLine($"Setting prop {prop.Name}...");

                // Il est important de vérifier le type d'une propriété avant de lui affecter une valeur
                if (prop.PropertyType != typeof(string))
                    continue;

                // On peut définir la valeur d'une propriété
                prop.SetValue(contact, guid.ToString());

                // On peut obtenir la valeur d'une propriété
                object? val = prop.GetValue(contact);
                Console.WriteLine($"Prop {prop.Name}={val}");
            }

            // On peut obtenir les champs d'un type, y compris les champs privés
            // Note : les champs privés ne sont pas hérités, donc si on veut obtenir les champs privés d'une classe parente, il faut le faire explicitement
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance);

            Console.ReadKey();
        }
    }
}
