using System.Linq;

namespace Project.UI
{
    public class GameManager
    {
        private readonly PlayerSymbolPresenter _playerSymbolPresenter;
        private readonly OpponentInputPresenter _opponentInputPresenter;
        private readonly OpponentSymbolPresenter _opponentSymbolPresenter;
        private readonly CountdownPresenter _countdownPresenter;
        private readonly ResultTextPresenter _resultTextPresenter;

        enum GameState
        {
            PlayerTurn,
            OpponentTurn,
            ShowResults,
            EndGame
        };

        private GameState gameState;

        private string playerEntry;
        private string opponentEntry;        
        private string[] validEntries = new string[] { "rock", "paper", "scissors" };

        public GameManager(
                PlayerSymbolPresenter playerSymbolPresenter,
                OpponentInputPresenter opponentInputPresenter,
                OpponentSymbolPresenter opponentSymbolPresenter,
                CountdownPresenter countdownPresenter,
                ResultTextPresenter resultTextPresenter)
        {            
            _playerSymbolPresenter = playerSymbolPresenter;
            _opponentInputPresenter = opponentInputPresenter;
            _opponentSymbolPresenter = opponentSymbolPresenter;
            _countdownPresenter = countdownPresenter;            
            _resultTextPresenter = resultTextPresenter;

            _countdownPresenter.OnTimeUp += OnOpponentTimeUp;

            SetState(GameState.PlayerTurn);
        }


        private void SetState(GameState state)
        {
            gameState = state;

            switch (gameState)
            {
                case GameState.OpponentTurn:
                    OnOpponentTurn();
                break;

                case GameState.ShowResults:
                    // TODO: Update UI with player and opptonents selected symbols
                break;

                default:
                break;
            }
        }


        /// <summary>
        /// Event handler for player enters their selected symbol
        /// </summary>
        /// <param name="entry">Entered symbol</param>
        public void OnPlayerEntryEntered(string entry)
        {
            if (gameState != GameState.PlayerTurn)
                return;

            if (ValidateEntry(entry))
            {
                playerEntry = entry;
                SetState(GameState.OpponentTurn);
            }
        }

        /// <summary>
        /// Event handler for when opponent enters their selected symbol
        /// </summary>
        /// <param name="entry">Entered symbol</param>
        public void OnOpponentEntryEntered(string entry)
        {
            if (gameState != GameState.OpponentTurn)
                return;

            if (ValidateEntry(entry))
            {
                opponentEntry = entry;
                SetState(GameState.ShowResults);
            }            
        }

        private bool ValidateEntry(string entry)
        {
            return string.IsNullOrWhiteSpace(entry) == false && validEntries.Contains(entry.ToLower());
        }

        /// <summary>
        /// Event handler for when its the opponents turn
        /// </summary>
        private void OnOpponentTurn()
        {
            _countdownPresenter.StartTimer();
            _opponentInputPresenter.SelectSymbol();
        }

        /// <summary>
        /// Evemt handler for when opponents turn time has run out
        /// </summary>
        private void OnOpponentTimeUp()
        {
            OnOpponentEntryEntered(_opponentInputPresenter.GetSelectedSymbol());
        }

    }

}
