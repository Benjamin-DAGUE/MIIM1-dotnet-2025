using Blazor.WASM.Morpion.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.WASM.Morpion.Shared;

public partial class GameCell
{
    #region Properties


    /// <summary>
    ///     Permet à une cellule de connaître le joueur actuel.
    /// </summary>
    [Parameter]
    public CellValue CurrentPlayer { get; set; }

    /// <summary>
    ///     Détermine la valeur de la cellule.
    /// </summary>
    [Parameter]
    public CellValue Value { get; set; }

    /// <summary>
    ///     Déclenché lors du changement de la valeur de la cellule.
    /// </summary>
    [Parameter]
    public EventCallback<CellValue> ValueChanged { get; set; }

    /// <summary>
    ///     Permet au composant parent de rendre la cellule en lecture seule.
    /// </summary>
    [Parameter]
    public bool ReadOnly { get; set; }

    #endregion

    #region Methods

    private void HandleClick()
    {
        // Si le composant est en lecture seule ou si une valeur est déjà présente, on ne fait rien.
        if (ReadOnly || Value != CellValue.Empty)
        {
            return;
        }

        //Autrement on affecte la cellule avec la marque du joueur actuel.
        Value = CurrentPlayer;
        ValueChanged.InvokeAsync(Value);
    }

    #endregion
}