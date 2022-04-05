using JetBrains.Annotations;
using System;
using TMPro;

namespace Project.UI
{
    [UsedImplicitly]
    public sealed class CountdownPresenter
    {
        private readonly TMP_Text _countdownText;
        private Timer _timer;

        public event Action OnTimeUp;

        public CountdownPresenter(TMP_Text text, Timer timer)
        {
            _countdownText = text;
            _timer = timer;
            _timer.OnTimerUpdate += OnUpdateCounter;
            _timer.OnTimerEnd += OnTimerEnd;
        }
        public void StartTimer()
        {
            _countdownText.gameObject.SetActive(true);
            _timer.StartTimer();
        }

        public void Hide()
        {
            _countdownText.gameObject.SetActive(false);
        }

        private void OnUpdateCounter(int counter)
        {
            _countdownText.SetText(counter.ToString());
        }

        private void OnTimerEnd()
        {
            _countdownText.gameObject.SetActive(false);

            if (OnTimeUp != null)
            {
                OnTimeUp();
            }
        }

    }
}