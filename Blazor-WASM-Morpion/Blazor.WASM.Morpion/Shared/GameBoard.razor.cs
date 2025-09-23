using Blazor.WASM.Morpion.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.WASM.Morpion.Shared;

public partial class GameBoard
{
    #region Fields

    /// <summary>
    ///     Joueur actif.
    /// </summary>
    private CellValue _currentPlayer = CellValue.X;

    /// <summary>
    ///     Matrice des valeurs des cellules du jeu.
    /// </summary>
    private readonly CellValue[,] _cells = new CellValue[3, 3];

    /// <summary>
    ///     Utilisée pour faire des foreach dans l'UI.
    /// </summary>
    private readonly int[] _indexes = [0, 1, 2];

    #endregion

    #region Properties

    /// <summary>
    ///     Indique qui est le gagnant de la partie. Si la valeur est <see cref="CellValue.Empty"/>, c'est une égalité.
    /// </summary>
    [Parameter]
    public CellValue? Winner { get; set; }

    /// <summary>
    ///     Indique que le paramètre <see cref="Winner"/> a changé de valeur.
    /// </summary>
    [Parameter]
    public EventCallback<CellValue?> WinnerChanged { get; set; }

    #endregion

    #region Methods

    /// <summary>
    ///     Cette méthode est déclenchée lorsqu'une cellule change de valeur.
    /// </summary>
    /// <param name="row">index de la ligne</param>
    /// <param name="col">index de la colonne</param>
    private void OnCellValueChanged(int row, int col)
    {
        // Si on est pas en cours de reset
        if (_cells[row, col] != CellValue.Empty)
        {
            bool _hasEmptyCell = false;

            //Changement du joueur
            _currentPlayer = _currentPlayer == CellValue.X ? CellValue.O : CellValue.X;

            //Vérification d'une condition de victoire.
            for (int i = 0; i < 3; i++)
            {
                // Check rows
                if (_cells[i, 0] != CellValue.Empty && _cells[i, 0] == _cells[i, 1] && _cells[i, 1] == _cells[i, 2])
                {
                    Winner = _cells[i, 0];
                    WinnerChanged.InvokeAsync(Winner);
                    return;
                }
                // Check columns
                else if (_cells[0, i] != CellValue.Empty && _cells[0, i] == _cells[1, i] && _cells[1, i] == _cells[2, i])
                {
                    Winner = _cells[0, i];
                    WinnerChanged.InvokeAsync(Winner);
                    return;
                }
                else if (_cells[1, 1] != CellValue.Empty)
                {
                    // Check diagonals
                    if ((_cells[0, 0] == _cells[1, 1] && _cells[1, 1] == _cells[2, 2]) ||
                        (_cells[0, 2] == _cells[1, 1] && _cells[1, 1] == _cells[2, 0]))
                    {
                        Winner = _cells[1, 1];
                        WinnerChanged.InvokeAsync(Winner);
                        return;
                    }
                }

                for (int y = 0; y < 3; y++)
                {
                    _hasEmptyCell = _hasEmptyCell || _cells[i, y] == CellValue.Empty;
                }
            }

            //Vérification de l'égalité (si on a pas au moins une cellule vide.
            if (_hasEmptyCell == false)
            {
                Winner = CellValue.Empty;
                WinnerChanged.InvokeAsync(Winner);
            }
        }
    }

    /// <summary>
    ///     Permet de réinitialiser la partie.
    /// </summary>
    public void Reset()
    {
        // On réaffecte les valeurs des composants enfant grace au binding.
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                _cells[row, col] = CellValue.Empty;
            }
        }
        // On remet le joueur de départ.
        _currentPlayer = CellValue.X;
        // On réinitialise le gagnant.
        Winner = null;
        WinnerChanged.InvokeAsync(Winner);
    }

    #endregion
}
