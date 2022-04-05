using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float elapsedTime = 0f;
    private int countDown = 0;
    private bool timerEnabled = false;

    public int Counter = 3;

    public event Action<int> OnTimerUpdate;
    public event Action OnTimerEnd;

    private bool isTimeUp => countDown <= 0;

    void Update()
    {
        if (timerEnabled)
        {
            elapsedTime += Time.deltaTime;

            // Has 1 second passed?
            if (elapsedTime >= 1f)
            {                
                countDown -= 1;
                elapsedTime = 0f;
            }

            if (OnTimerUpdate != null)
            {
                OnTimerUpdate(countDown);
            }

            if (isTimeUp)
            {
                OnTimeUp();
            }
        }
    }

    public void StartTimer()
    {
        elapsedTime = 0;
        countDown = Counter;
        timerEnabled = true;
    }
    
    private void OnTimeUp()
    {
        timerEnabled = false;

        if (OnTimerEnd != null)
        {
            OnTimerEnd();
        }
    }
    
}
