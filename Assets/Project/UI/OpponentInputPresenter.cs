using JetBrains.Annotations;

namespace Project.UI
{
    [UsedImplicitly]
    public sealed class OpponentInputPresenter
    {        
        private string _selectedSymbol;

        public void SelectSymbol()
        {
            string[] symbols = new[] { Symbols.Rock, Symbols.Paper, Symbols.Scissors };

            int selection = GetRandomSelection();

            _selectedSymbol = symbols[selection - 1];
        }

        public void Reset()
        {
            _selectedSymbol = "";
        }

        public string GetSelectedSymbol()
        {
            return _selectedSymbol;
        }

        private int GetRandomSelection()
        {
            // TODO: Placeholder logic to be replaced with call to Random Number WebService.
            return UnityEngine.Random.Range(1, 4);
        }
    }
}