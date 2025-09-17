namespace PokedexEditor.Models;

/// <summary>
///     Représente un pokémon.
/// </summary>
public class Pokemon
{
    #region Properties

    /// <summary>
    ///     Obtient ou définit l'identifiant unique du pokémon.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Obtient ou définit le nom du pokémon.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Obtient ou définit la présentation du pokémon.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    ///     Obtient ou définit le type de pokémon.
    /// </summary>
    public PokemonType Types { get; set; } = PokemonType.None;

    /// <summary>
    ///     Obtient ou définit l'identifiant unique de l'évolution du pokémon.
    /// </summary>
    public int? EvolutionId { get; set; }

    /// <summary>
    ///     Obtient ou définit le niveau d'évolution du pokémon.
    /// </summary>
    public int? EvolutionLevel { get; set; }

    #endregion
}
