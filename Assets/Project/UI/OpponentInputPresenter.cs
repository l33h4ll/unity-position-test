using JetBrains.Annotations;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using System;
using System.Threading;
using UnityEngine;

namespace Project.UI
{
    [UsedImplicitly]
    public sealed class OpponentInputPresenter
    {        
        private string _selectedSymbol;

        public async void SelectSymbol()
        {
            string[] symbols = new[] { Symbols.Rock, Symbols.Paper, Symbols.Scissors };

            int selection = await GetRandomSelection(0, symbols.Length -1);

            _selectedSymbol = selection >= 0 ? symbols[selection] : "";
        }

        public void Reset()
        {
            _selectedSymbol = "";
        }

        public string GetSelectedSymbol()
        {
            return _selectedSymbol;
        }

        private async UniTask<int> GetRandomSelection(int min, int max)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfterSlim(TimeSpan.FromSeconds(3));

            try
            {
                var txt = (await UnityWebRequest.Get($"http://www.randomnumberapi.com/api/v1.0/random?min={min}&max={max}").SendWebRequest()).downloadHandler.text;

                var result = txt.Trim(new[] { '[', ']' });

                if (int.TryParse(result, out int selection))
                {
                    return selection;
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Opponent GetRandomSelection timed out");
            }

            return -1;
        }
    }
}