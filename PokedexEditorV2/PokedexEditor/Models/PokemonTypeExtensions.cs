using System.Diagnostics.CodeAnalysis;

namespace PokedexEditor.Models;

public static class PokemonTypeExtensions
{
    private static readonly Dictionary<PokemonType, string> _pokemonTypesStrings = new()
    {
        { PokemonType.Normal , "Normal" },
        { PokemonType.Fire , "Feu" },
        { PokemonType.Water , "Eau" },
        { PokemonType.Electric , "Electrique" },
        { PokemonType.Grass , "Plante" },
        { PokemonType.Ice , "Glace" },
        { PokemonType.Fighting , "Combat" },
        { PokemonType.Poison , "Poison" },
        { PokemonType.Ground , "Sol" },
        { PokemonType.Flying , "Vol" },
        { PokemonType.Psychic , "Psy" },
        { PokemonType.Bug , "Insecte" },
        { PokemonType.Rock , "Roche" },
        { PokemonType.Ghost , "Spectre" },
        { PokemonType.Dragon , "Dragon" },
        { PokemonType.Dark , "Ténèbres" },
        { PokemonType.Steel , "Acier" },
        { PokemonType.Fairy , "Fée" }
    };

    public static string AllTypesToFriendlyString()
    {
        return string.Join(", ", _pokemonTypesStrings.Values);
    }

    public static string ToFriendlyString(this PokemonType types)
    {
        List<string> result = [];

        foreach (KeyValuePair<PokemonType, string> kvp in _pokemonTypesStrings)
        {
            if (types.HasFlag(kvp.Key))
            {
                result.Add(kvp.Value);
            }
        }

        return result.Count > 0 ? string.Join(", ", result) : types.ToString();
    }

    public static bool TryParse(string? input, out PokemonType types)
    {
        types = PokemonType.None;

        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        string[] parts = input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (string part in parts)
        {
            KeyValuePair<PokemonType, string>? match = _pokemonTypesStrings
                .FirstOrDefault(kvp => string.Equals(kvp.Value, part, StringComparison.OrdinalIgnoreCase));

            if (match.HasValue && match.Value.Key != PokemonType.None)
            {
                types |= match.Value.Key;
            }
            else
            {
                return false;
            }
        }

        return types != PokemonType.None;
    }

    public static List<PokemonType> ToList(this PokemonType types)
    {
        List<PokemonType> result = [];

        foreach (PokemonType type in _pokemonTypesStrings.Keys)
        {
            if(types.HasFlag(type))
            {
                result.Add(type);
            }
        }

        return result;
    }
}
