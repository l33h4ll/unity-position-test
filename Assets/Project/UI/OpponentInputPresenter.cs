using JetBrains.Annotations;
using UnityEngine;

namespace Project.UI
{
    [UsedImplicitly]
    public sealed class OpponentInputPresenter
    {        
        private string selectedSymbol;
        
        /// <summary>
        /// Method used to tell the opponent its their turn to choose a symbol
        /// </summary>
        public void SelectSymbol()
        {            
            string[] symbols = new[] { "rock", "papper", "scissors" };

            int selection = GetRandomSelection();

            selectedSymbol = symbols[selection - 1];
        }

        /// <summary>
        /// Method returns opponents selected symbol
        /// </summary>
        /// <returns>Opponents selected symbol</returns>
        public string GetSelectedSymbol()
        {
            return selectedSymbol;
        }

        private int GetRandomSelection()
        {
            // TODO: Placeholder logic to be replaced with call to Random Number WebService.
            return Random.Range(1, 4);
        }
    }
}