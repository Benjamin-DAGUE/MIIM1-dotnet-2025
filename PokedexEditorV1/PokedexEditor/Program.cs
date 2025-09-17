using PokedexEditor.Models;
using System.Text.RegularExpressions;

namespace PokedexEditor;

internal partial class Program
{
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

        #region Main Menu

        do
        {
            Console.WriteLine("Bienvenue dans le Pokédex .NET Console !");
            Console.WriteLine("1 : Parcourir le Pokédex");
            Console.WriteLine("2 : Ajouter un Pokémon");
            Console.WriteLine("0 : Quitter");

            string? userInput = Console.ReadLine();

            switch (userInput)
            {
                case "0":
                    exit = true;
                    break;
                case "1":
                    ReadPokedex();
                    break;
                case "2":
                    AddPokemon();
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
    /// <param name="pokedex">Liste des contacts.</param>
    /// <param name="searchTerm">Terme recherché.</param>
    private static void ReadPokedex()
    {
        bool exit = false;
        int currentIndex = 0;

        do
        {
            Console.Clear();

            if (_pokedex.Count == 0)
            {
                Console.WriteLine("Le Pokédex est vide");
                Console.WriteLine("Appuyez sur une touche pour retourner au menu principal...");
                Console.ReadKey();
                exit = true;
                break;
            }

            Pokemon pokemon = _pokedex[currentIndex];

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
                    currentIndex = currentIndex + 1 == _pokedex.Count ? 0 : currentIndex + 1;
                    break;
                case "2":
                    //On va à l'entrée précédente ou à la fin de la liste si on est au début.
                    currentIndex = currentIndex - 1 < 0 ? _pokedex.Count - 1 : currentIndex - 1;
                    break;
                case "3":

                    Console.Write("Confirmer la suppression (O/N) : ");
                    if (Console.ReadLine()?.ToUpper() == "O")
                    {
                        //On supprime l'entrée de la liste en cours de lecture.
                        _pokedex.Remove(pokemon);
                        //On change l'index si on supprime l'entrée à la fin de la liste pour prendre l'entrée précédente.
                        currentIndex = currentIndex >= _pokedex.Count ? _pokedex.Count - 1 : currentIndex;

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
        //Console.WriteLine(string.Format("Id : {0}", pokemon.Id));
        //Console.WriteLine("Id : " + pokemon.Id);

        Console.WriteLine($"Nom : {pokemon.Name}");
        Console.WriteLine($"Description : {pokemon.Description}");
        if (pokemon.Types != PokemonType.None)
        {
            Console.WriteLine($"Type : {pokemon.Types.ToFriendlyString()}");
            //Console.WriteLine($"Type : {PokemonTypeExtensions.ToFriendlyString(pokemon.Types)}");
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

        _pokedex.Add(pokemon);
        _pokedex = _pokedex.OrderBy(p => p.Id).ToList();

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

    [GeneratedRegex("^[a-zA-Z]+$")]
    private static partial Regex CheckPokemonName();

    #endregion
}
