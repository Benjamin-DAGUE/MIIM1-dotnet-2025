using PokedexEditor.Models;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace PokedexEditor;

internal partial class Program
{
    #region Constants

    /// <summary>
    ///     Chemin du fichier de sauvegarde.
    /// </summary>
    private const string FILE_PATH = "pokedex.json";

    #endregion

    #region Fields

    /// <summary>
    ///     Liste des pokémons dans le Pokédex.
    /// </summary>
    private static List<Pokemon> _pokedex = [];

    #endregion

    #region Methods

    /// <summary>
    ///     Point d'entrée de l'application
    /// </summary>
    static void Main()
    {
        bool exit = false;

        _pokedex = LoadFromFile();

        #region Main Menu

        do
        {
            Console.WriteLine("Bienvenue dans le Pokédex .NET Console !");
            Console.WriteLine("1 : Parcourir le Pokédex");
            Console.WriteLine("2 : Ajouter un Pokémon");
            Console.WriteLine("3 : Rechercher");
            Console.WriteLine("0 : Quitter");

            string? userInput = Console.ReadLine();

            switch (userInput)
            {
                case "0":
                    exit = true;
                    break;
                case "1":
                    ReadPokedex(_pokedex);
                    break;
                case "2":
                    AddPokemon();
                    break;
                case "3":
                    Search();
                    break;
                default:
                    break;
            }

            Console.Clear();

        } while (exit == false);

        #endregion
    }

    /// <summary>
    ///     Permet la lecture du Pokédex ou d'un résultat de recherche..
    /// </summary>
    /// <param name="pokedex">Liste des pokémons.</param>
    /// <param name="searchTerm">Terme recherché.</param>
    private static void ReadPokedex(List<Pokemon> pokedex, string? searchTerm = null)
    {
        bool exit = false;
        int currentIndex = 0;

        do
        {
            Console.Clear();

            if (string.IsNullOrWhiteSpace(searchTerm) == false)
            {
                Console.WriteLine("Voici les résultats pour la recherche suivante :");
                Console.WriteLine(searchTerm);
            }

            if (pokedex.Count == 0)
            {
                Console.WriteLine(searchTerm == null ? "Le Pokédex est vide" : "Aucun résultat");
                Console.WriteLine("Appuyez sur une touche pour retourner au menu principal...");
                Console.ReadKey();
                exit = true;
                break;
            }

            Pokemon pokemon = pokedex[currentIndex];

            ShowPokemonDetails(pokemon);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1 : Suivant");
            Console.WriteLine("2 : Précédent");
            Console.WriteLine("3 : Supprimer");
            Console.WriteLine("0 : Retour");

            string? userInput = Console.ReadLine();

            switch (userInput)
            {
                case "0":
                    //Retour au menu principal.
                    exit = true;
                    break;
                case "1":
                    //On va à l'entrée suivante ou au début de la liste si on est à la fin.
                    currentIndex = currentIndex + 1 == pokedex.Count ? 0 : currentIndex + 1;
                    break;
                case "2":
                    //On va à l'entrée précédente ou à la fin de la liste si on est au début.
                    currentIndex = currentIndex - 1 < 0 ? pokedex.Count - 1 : currentIndex - 1;
                    break;
                case "3":

                    Console.Write("Confirmer la suppression (O/N) : ");
                    if (Console.ReadLine()?.ToUpper() == "O")
                    {
                        //On supprime l'entrée de la liste en cours de lecture.
                        pokedex.Remove(pokemon);
                        //Dans le cas d'une recherche, on supprime aussi l'entrée de la liste principale.
                        if (searchTerm != null)
                        {
                            _pokedex.Remove(pokemon);
                            SaveToFile();
                        }
                        //On change l'index si on supprime l'entrée à la fin de la liste pour prendre l'entrée précédente.
                        currentIndex = currentIndex >= pokedex.Count ? pokedex.Count - 1 : currentIndex;

                        Console.WriteLine("Suppression réussie.");
                        Console.WriteLine("Appuyez sur une touche pour continuer...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Suppression annulée.");
                        Console.WriteLine("Appuyez sur une touche pour continuer...");
                        Console.ReadKey();
                    }
                    break;
                default:
                    break;
            }
        } while (exit == false);
    }

    /// <summary>
    ///     Affiche les détails d'un pokémon.
    /// </summary>
    /// <param name="pokemon">Pokémon à afficher.</param>
    private static void ShowPokemonDetails(Pokemon pokemon)
    {
        Console.WriteLine("------");
        Console.WriteLine($"Id : {pokemon.Id}");
        Console.WriteLine($"Nom : {pokemon.Name}");
        Console.WriteLine($"Description : {pokemon.Description}");
        if (pokemon.Types != PokemonType.None)
        {
            Console.WriteLine($"Type : {pokemon.Types.ToFriendlyString()}");
        }
        if (pokemon.EvolutionId.HasValue && pokemon.EvolutionLevel.HasValue)
        {
            Console.WriteLine($"Evolue en (Id) : {pokemon.EvolutionId}");
            Console.WriteLine($"Evolue au niveau : {pokemon.EvolutionLevel}");
        }
        Console.WriteLine("------");
    }

    /// <summary>
    ///     Permet l'ajout d'un Pokémon dans le Pokédex.
    /// </summary>
    private static void AddPokemon()
    {
        Console.Clear();

        Pokemon pokemon = new();

        do
        {
            Console.Write("Id* : ");

            if (int.TryParse(Console.ReadLine(), out int id) && id > 0)
            {
                if (_pokedex.Any(p => p.Id == id) == false)
                {
                    pokemon.Id = id;
                }
                else
                {
                    DisplayError("Un Pokémon avec cet identifiant existe déjà.");
                }
            }
            else
            {
                DisplayError("L'identifiant doit être un nombre entier positif.");
            }

        } while (pokemon.Id <= 0);

        do
        {
            Console.Write("Nom* : ");
            string? name = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(name))
            {
                DisplayError("Le nom est obligatoire.");
            }
            else if (CheckPokemonName().IsMatch(name) == false)
            {
                DisplayError("Le nom doit être constitué uniquement de caractères alphabétiques sans accent : [a-zA-Z].");
            }
            else
            {
                pokemon.Name = name;
            }
        } while (string.IsNullOrWhiteSpace(pokemon.Name));


        Console.Write("Description : ");
        pokemon.Description = Console.ReadLine() ?? "";

        do
        {
            Console.Write($"Types* ({PokemonTypeExtensions.AllTypesToFriendlyString()}) : ");

            if (PokemonTypeExtensions.TryParse(Console.ReadLine(), out PokemonType pokemonTypes))
            {
                pokemon.Types = pokemonTypes;
            }
            else
            {
                DisplayError("Le type est invalide.");
            }
        } while (pokemon.Types == PokemonType.None);

        _pokedex.Add(pokemon);
        _pokedex = _pokedex.OrderBy(p => p.Id).ToList();
        SaveToFile();

        string? evolutionIdInput = null;

        do
        {
            Console.Write("Evolue en (Id) : ");
            evolutionIdInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(evolutionIdInput) == false)
            {
                if (int.TryParse(evolutionIdInput, out int evolutionId) && evolutionId > 0)
                {
                    if (_pokedex.Any(p => p.Id == evolutionId) == true)
                    {
                        pokemon.EvolutionId = evolutionId;

                        do
                        {
                            Console.Write("Evolue au niveau : ");
                            if (int.TryParse(Console.ReadLine(), out int evolutionLevel) && evolutionLevel > 0)
                            {
                                pokemon.EvolutionLevel = evolutionLevel;
                            }
                            else
                            {
                                DisplayError("Le niveau d'évolution doit être un nombre entier positif.");
                            }
                        } while (pokemon.EvolutionLevel == null);
                    }
                    else
                    {
                        DisplayError("Aucun Pokémon avec cet identifiant n'existe dans le Pokédex. Veuillez d'abord ajouter le Pokémon évolué.");
                    }
                }
                else
                {
                    DisplayError("L'identifiant d'évolution doit être un nombre entier positif.");
                }
            }

        } while (string.IsNullOrEmpty(evolutionIdInput) == false && pokemon.EvolutionLevel == null);

        Console.Clear();
        Console.WriteLine("Le pokémon suivant a bien été ajouté :");
        ShowPokemonDetails(pokemon);
        Console.WriteLine();

        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    private static void DisplayError(string message)
    {
        ConsoleColor backgroundColor = Console.BackgroundColor;
        ConsoleColor foregroundColor = Console.ForegroundColor;

        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine(message);

        Console.BackgroundColor = backgroundColor;
        Console.ForegroundColor = foregroundColor;
    }

    /// <summary>
    ///     Générateur de Regex de vérification du nom d'un Pokémon.
    /// </summary>
    /// <returns>Regex de vérification du nom d'un Pokémon</returns>
    [GeneratedRegex("^[a-zA-Z]+$")]
    private static partial Regex CheckPokemonName();

    /// <summary>
    ///     Permet de rechercher dans le Pokédex.
    /// </summary>
    private static void Search()
    {
        //IEnumerable<TSource> est une interface générique qui expose une simple méthode : GetEnumerator.
        // C'est c'est méthode qui est utilisée par foreach, ce qui signifie que l'on peut faire un foreach uniquement sur les classes qui implémentent IEnumerable<TSource>.

        //Une interface, une classe ou une méthode générique adapte son comportement à un (ou plusieurs) type(s) spécifié(s).
        //Par exemple, IEnumerable<Pokemon> permet de faire un foreach sur une instance énumérable de type Pokemon.
        //List<Pokemon> est une liste qui permet d'administrer un tableau de Pokemon dynamique (la longueur du tableau est inconnue).


        //La classe static Enumerable expose des méthodes d'extensions qui complètent IEnumerable<TSource>, ce sont les méthodes d'extensions LINQ.

        //LINQ (Language-Integrated Query) est une fonctionnalité de C# et du framework .NET qui permet d'effectuer des requêtes sur divers types de sources de données
        //Telles que des collections en mémoire, des bases de données et des structures de données XML, d'une manière intégrée et uniforme.
        //En utilisant soit une syntaxe de requête spécifique soit des méthodes d'extension, LINQ fournit un moyen puissant et flexible de
        //filtrer, ordonner, regrouper et transformer les données, tout en offrant les avantages de la vérification de type et de l'intellisense
        //dans les environnements de développement tels que Visual Studio. (source : Chat GPT)

        //Ici, on utilise la méthode Where qui filtre en fonction d'un prédicat le pokédex, cela permet d'implémenter un moteur de recherche basique.

        /*
         *
            Lazy Evaluation (Évaluation paresseuse)
                Dans le cas d'une évaluation paresseuse, l'exécution de la requête est différée jusqu'à ce qu'un élément soit réellement demandé.
                Cela signifie que la requête LINQ elle-même n'est pas immédiatement exécutée lorsque vous la déclarez.
                Au lieu de cela, l'exécution est retardée jusqu'à ce que vous commenciez à itérer sur les éléments résultants (par exemple avec une boucle foreach).

            Eager Evaluation (Évaluation immédiate)
                À l'inverse, l'évaluation immédiate signifie que la requête est exécutée dès qu'elle est déclarée.
                Vous pouvez forcer une évaluation immédiate en appelant des méthodes comme ToList() ou ToArray() sur l'objet de requête.

            Pourquoi est-ce important ?
                Performance: Lazy Evaluation peut être plus efficace car elle ne traite que les éléments qui sont réellement nécessaires.
                Ressources: Dans le cas de séquences de données très grandes ou infinies, l'évaluation paresseuse peut être essentielle pour la gestion efficace des ressources.
                Flexibilité: Vous pouvez définir une séquence d'opérations LINQ sans les exécuter, vous laissant la possibilité de les réutiliser ou de les modifier plus tard.
                Sémantique: Certains opérateurs LINQ, comme First() ou Single(), ne sont utiles que dans un contexte d'évaluation paresseuse où seule une sous-partie des données est nécessaire.

         *
         */

        IEnumerable<Pokemon> query = _pokedex;
        string searchTherms = "";
        Console.Clear();

        Console.Write("Rechercher dans le nom : ");
        string? search = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(search) == false)
        {
            string searchName = search;
            query = query.Where((pokemon) => pokemon.Name.Contains(searchName, StringComparison.CurrentCultureIgnoreCase) == true);
            searchTherms += $"Nom : {search}{Environment.NewLine}";
        }

        Console.Write("Rechercher dans la description : ");
        search = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(search) == false)
        {
            string searchDescription = search;
            query = query.Where((pokemon) => pokemon.Description.Contains(searchDescription, StringComparison.CurrentCultureIgnoreCase) == true);
            searchTherms += $"Nom : {search}{Environment.NewLine}";
        }

        Console.Write("Rechercher par type : ");
        search = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(search) == false && PokemonTypeExtensions.TryParse(search, out PokemonType searchTypes))
        {
            PokemonType[] flags = [.. Enum.GetValues<PokemonType>()];

            query = query.Where((pokemon) => searchTypes.ToList().All(searchtype => pokemon.Types.HasFlag(searchtype)));
            searchTherms += $"Nom : {search}{Environment.NewLine}";
        }

        ReadPokedex(query.ToList(), searchTherms);
    }

    /// <summary>
    ///     Charge les données du Pokédex depuis un fichier.
    /// </summary>
    /// <returns>Liste des Pokémons présents dans le fichier.</returns>
    public static List<Pokemon> LoadFromFile()
    {
        List<Pokemon> results = [];

        try
        {
            results = JsonSerializer.Deserialize<List<Pokemon>>(File.ReadAllText(FILE_PATH)) ?? results;
        }
        catch
        {

        }

        return results;
    }

    /// <summary>
    ///     Sauvegarde le Pokédex dans un fichier.
    /// </summary>
    /// <param name="pokemons">Liste des Pokémons à sauvegarder.</param>
    public static void SaveToFile() =>
        File.WriteAllText(FILE_PATH, JsonSerializer.Serialize(_pokedex));

    #endregion
}
