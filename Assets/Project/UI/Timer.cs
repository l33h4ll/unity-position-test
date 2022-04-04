using Project.UI;
using System;
using UnityEngine;
using Zenject;

public class Timer : MonoBehaviour
{
    private CountdownPresenter _countdownPresenter;    

    private float elapsedTime = 0f;
    private int countDown = 0;
    private bool timerEnabled = false;

    public int Counter = 3;

    public event Action<int> OnTimerUpdate;
    public event Action OnTimerEnd;


    [Inject]
    public void Construct(CountdownPresenter countdownPresenter)
    {
        _countdownPresenter = countdownPresenter;
    }


    void Update()
    {
        if (timerEnabled)
        {
            // Update elapsed time
            elapsedTime += Time.deltaTime;

            // Has 1 second passed?
            if (elapsedTime >= 1f)
            {
                // Update counter
                countDown -= 1;
                elapsedTime = 0f;
            }

            if (OnTimerUpdate != null)
            {
                // Trigger OnTimerUpdate event with new counter value.
                OnTimerUpdate(countDown);
            }

            // Is Time up?
            if (countDown <= 0)
            {
                // Trigger time up event.
                OnTimeUp();
            }
        }        
    }

    /// <summary>
    /// Method used to start the timer counting down.
    /// </summary>
    public void StartTimer()
    {
        elapsedTime = 0;
        countDown = Counter;
        timerEnabled = true;
    }

    /// <summary>
    /// Event triggered with timer reaches Zero
    /// </summary>
    private void OnTimeUp()
    {
        timerEnabled = false;

        if (OnTimerEnd != null)
        {
            // Timer OnTimerEnd event
            OnTimerEnd();
        }
    }
    
}
