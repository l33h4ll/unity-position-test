using System;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.UI
{
    public static class GameResult
    {
        public const string PlayerWins = "Player Wins!";
        public const string OpponentWins = "AI Wins!";
        public const string Tie = "Tie!";
    }

    public static class Symbol
    {
        public const string Rock = "rock";
        public const string Paper = "paper";
        public const string Scissors = "scissors";
        public const string Question = "question_mark";

        public static Sprite GetSymbolImage(string name)
        {
            Sprite symbolImage = null;

            try
            {
                symbolImage = Resources.Load<Sprite>(name);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            return symbolImage;
        }

        public static bool AreEqual(string symbolA, string symbolB)
        {
            return symbolA == symbolB;
        }

        public static bool IsGreater(string symbolA, string symbolB)
        {
            return symbolA == Symbol.Rock && symbolB == Symbol.Scissors || 
                   symbolA == Symbol.Scissors && symbolB == Symbol.Paper ||
                   symbolA == Symbol.Paper && symbolB == Symbol.Rock;
        }

        public static string GetWinner(string playerSymbol, string opponentSymbol)
        {
            if (AreEqual(playerSymbol, opponentSymbol))
            {
                return GameResult.Tie;
            }

            if (string.IsNullOrEmpty(opponentSymbol))
            {
                return GameResult.PlayerWins;
            }

            return IsGreater(playerSymbol, opponentSymbol) ? GameResult.PlayerWins : GameResult.OpponentWins;
        }

        public static bool IsValid(string symbol)
        {
            return string.IsNullOrWhiteSpace(symbol) == false &&
                GetSymbols().Contains(symbol.ToLower().Trim());
        }

        public static string[] GetSymbols()
        {
            return new string[] { Symbol.Rock, Symbol.Paper, Symbol.Scissors };
        }
    }

    public class GameManager : MonoBehaviour
    {
        private PlayerInputPresenter _playerInputPresenter;
        private PlayerSymbolPresenter _playerSymbolPresenter;
        private OpponentInputPresenter _opponentInputPresenter;
        private OpponentSymbolPresenter _opponentSymbolPresenter;
        private CountdownPresenter _countdownPresenter;
        private ResultTextPresenter _resultTextPresenter;

        private float RevelTimeInSeconds => 0.5f;
        private float GameResetTimeInSeconds => 5f;

        enum GameState
        {
            GameStart,
            PlayerTurn,
            OpponentTurn,
            RevealWinner
        };

        private GameState gameState;

        private string playerEntry;
        private string opponentEntry;

        [Inject]
        public void Constructor(
            PlayerInputPresenter playerInputPresenter,
            PlayerSymbolPresenter playerSymbolPresenter,
            OpponentInputPresenter opponentInputPresenter,
            OpponentSymbolPresenter opponentSymbolPresenter,
            CountdownPresenter countdownPresenter,
            ResultTextPresenter resultTextPresenter)
        {
            _playerInputPresenter = playerInputPresenter;
            _playerSymbolPresenter = playerSymbolPresenter;
            _opponentInputPresenter = opponentInputPresenter;
            _opponentSymbolPresenter = opponentSymbolPresenter;
            _countdownPresenter = countdownPresenter;
            _resultTextPresenter = resultTextPresenter;

            _playerInputPresenter.OnSubmit += OnPlayerEntryEntered;
            _countdownPresenter.OnTimeUp += OnOpponentTimeUp;

            SetState(GameState.GameStart);
        }

        private void SetState(GameState state)
        {
            gameState = state;

            switch (gameState)
            {
                case GameState.GameStart:
                    OnNewGame();
                break;
       
                case GameState.OpponentTurn:
                    OnOpponentTurn();
                break;

                case GameState.RevealWinner:
                    OnRevealWinner();
                break;

                default:
                break;
            }
        }

        private void OnNewGame()
        {
            _resultTextPresenter.HideResult();
            _countdownPresenter.Hide();
            _playerInputPresenter.Reset();
            _playerSymbolPresenter.SetImage(Symbol.GetSymbolImage(Symbol.Question));
            _opponentInputPresenter.Reset();
            _opponentSymbolPresenter.SetImage(Symbol.GetSymbolImage(Symbol.Question));
            SetState(GameState.PlayerTurn);
        }

        public void OnPlayerEntryEntered(string entry)
        {
            if (gameState != GameState.PlayerTurn)
                return;

            if (Symbol.IsValid(entry))
            {
                playerEntry = entry;
                _playerInputPresenter.DisableInput();
                SetState(GameState.OpponentTurn);
            }
        }

        public void OnOpponentEntryEntered(string entry)
        {
            if (gameState != GameState.OpponentTurn)
                return;

            opponentEntry = entry;
            SetState(GameState.RevealWinner);
        }

        private void OnOpponentTurn()
        {
            _countdownPresenter.StartTimer();
            _opponentInputPresenter.SelectSymbol();
        }

        private void OnOpponentTimeUp()
        {
            OnOpponentEntryEntered(_opponentInputPresenter.GetSelectedSymbol());
        }

        private void OnRevealWinner()
        {
            Sprite playerSelectedSymbol = Symbol.GetSymbolImage(playerEntry);
            Sprite opponentSelectedSymbol= Symbol.GetSymbolImage(opponentEntry);

            if (playerSelectedSymbol != null)
            {
                _playerSymbolPresenter.SetImage(playerSelectedSymbol);
            }

            if (opponentSelectedSymbol != null)
            {
                _opponentSymbolPresenter.SetImage(opponentSelectedSymbol);
            }

            Observable.Timer(TimeSpan.FromSeconds(RevelTimeInSeconds)).Subscribe(_ => RevelSelections());

            Observable.Timer(TimeSpan.FromSeconds(GameResetTimeInSeconds)).Subscribe(_ => RestartGame());
        }

        private void RevelSelections()
        {                       
            _resultTextPresenter.ShowResult(Symbol.GetWinner(playerEntry, opponentEntry));
        }

        private void RestartGame()
        {
            SetState(GameState.GameStart);
        }
    }
}
