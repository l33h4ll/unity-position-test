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

        /// <summary>
        /// Event triggered when timer reaches Zero
        /// </summary>
        public event Action OnTimeUp;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Dependency Injected TMP_Text reference</param>
        /// <param name="timer">Dependency Injected Timer reference</param>
        public CountdownPresenter(TMP_Text text, Timer timer)
        {
            _countdownText = text;
            _timer = timer;
            _timer.OnTimerUpdate += OnUpdateCounter;
            _timer.OnTimerEnd += OnTimerEnd;
        }


        /// <summary>
        /// Used to start the timer counting down
        /// </summary>
        public void StartTimer()
        {
            _timer.StartTimer();
        }

        /// <summary>
        /// Event triggered when counter display should be updated i.e. every elapsed second
        /// </summary>
        /// <param name="counter">The value to display</param>
        private void OnUpdateCounter(int counter)
        {
            _countdownText.SetText(counter.ToString());
        }

        /// <summary>
        /// Event triggered with timer reaches Zero
        /// </summary>
        private void OnTimerEnd()
        {
            if (OnTimeUp != null)
            {
                OnTimeUp();
            }
        }

    }
}