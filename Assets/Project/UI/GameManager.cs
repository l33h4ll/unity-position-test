using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Project.UI
{
    public static class Symbols {
        public const string Rock = "rock";
        public const string Paper = "paper";
        public const string Scissors = "scissors";
        public const string Question = "question_mark";
    }

    public static class GameResult
    {
        public const string PlayerWins = "Player Wins!";
        public const string OpponentWins = "AI Wins!";
        public const string Tie = "Tie!";
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

        private string[] validEntries = new string[] { Symbols.Rock, Symbols.Paper, Symbols.Scissors };

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
            _playerSymbolPresenter.SetImage(GetSymbolImage(Symbols.Question));
            _opponentInputPresenter.Reset();
            _opponentSymbolPresenter.SetImage(GetSymbolImage(Symbols.Question));
            SetState(GameState.PlayerTurn);
        }

        public void OnPlayerEntryEntered(string entry)
        {
            if (gameState != GameState.PlayerTurn)
                return;

            if (ValidateEntry(entry))
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

            if (ValidateEntry(entry))
            {
                opponentEntry = entry;
                SetState(GameState.RevealWinner);
            }
        }

        private bool ValidateEntry(string entry)
        {
            return string.IsNullOrWhiteSpace(entry) == false && validEntries.Contains(entry.ToLower());
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
            Sprite playerSelectedSymbol = GetSymbolImage(playerEntry);
            Sprite opponentSelectedSymbol= GetSymbolImage(opponentEntry);

            if (playerSelectedSymbol != null)
            {
                _playerSymbolPresenter.SetImage(playerSelectedSymbol);
            }

            if (opponentSelectedSymbol != null)
            {
                _opponentSymbolPresenter.SetImage(opponentSelectedSymbol);
            }
            
            StartCoroutine(RevelSelections(RevelTimeInSeconds));

            StartCoroutine(RestartGame(GameResetTimeInSeconds));
        }

        private IEnumerator RevelSelections(float delayInSeconds)
        {
            yield return new WaitForSeconds(delayInSeconds);

            _resultTextPresenter.ShowResult(GetWinner(playerEntry, opponentEntry));
        }

        private IEnumerator RestartGame(float delayInSeconds)
        {
            yield return new WaitForSeconds(delayInSeconds);

            SetState(GameState.GameStart);
        }

        private string GetWinner(string player, string opponent)
        {
            if (player == opponent)
            {
                return GameResult.Tie;
            }
            
            if (player == Symbols.Rock && opponent == Symbols.Scissors ||
                player == Symbols.Scissors && opponent == Symbols.Paper || 
                player == Symbols.Paper && opponent == Symbols.Rock)
            {
                return GameResult.PlayerWins;
            }
            else
            {
                return GameResult.OpponentWins;
            }
        }

        private Sprite GetSymbolImage(string name)
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
    }
}
