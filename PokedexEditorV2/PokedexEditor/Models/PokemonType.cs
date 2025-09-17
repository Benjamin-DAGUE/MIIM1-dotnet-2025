namespace PokedexEditor.Models;

[Flags]
public enum PokemonType
{
    None = 0,
    Normal = 1,
    Fire = 2,
    Water = 4,
    Electric = 8,
    Grass = 16,
    Ice = 32,
    Fighting = 64,
    Poison = 128,
    Ground = 256,
    Flying = 512,
    Psychic = 1024,
    Bug = 2048,
    Rock = 4096,
    Ghost = 8192,
    Dragon = 16384,
    Dark = 32768,
    Steel = 65536,
    Fairy = 131072
}