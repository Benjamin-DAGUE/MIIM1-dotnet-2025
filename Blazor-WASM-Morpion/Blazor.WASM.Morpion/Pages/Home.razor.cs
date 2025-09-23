using Blazor.WASM.Morpion.Models;
using Blazor.WASM.Morpion.Shared;

namespace Blazor.WASM.Morpion.Pages;

public partial class Home
{
    #region Fields

    /// <summary>
    ///     Instance du composant <see cref="GameBoard"/> de l'interface utilisateur.
    ///     On va utiliser cette instance pour appeler la méthode <see cref="GameBoard.Reset"/> qui réinitialise la partie.
    /// </summary>
    private GameBoard? _gameBoard;

    /// <summary>
    ///     Représente le gagnant. Si la valeur est null, la partie est en cours. Si la valeur est <see cref="CellValue.Empty"/> la partie est terminée avec une égalité.
    /// </summary>
    private CellValue? _winner;

    #endregion

    #region Methods

    /// <summary>
    ///     Affiche un texte de résultat différent en fonction du gagnant récupéré dans <see cref="_winner"/>.
    /// </summary>
    /// <returns></returns>
    private string? GetResultString() => _winner switch
    {
        CellValue.X => "X gagne la partie!",
        CellValue.O => "O gagne la partie!",
        CellValue.Empty => "C'est une égalité!",
        _ => null
    };

    /// <summary>
    ///     Déclenche la remise à zéro de la partie.
    /// </summary>
    private void ResetGame() => _gameBoard?.Reset();

    #endregion
}
