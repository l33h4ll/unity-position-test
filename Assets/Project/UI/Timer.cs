using System;
using System.Collections;
using UniRx;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int Counter = 3;
    
    public event Action<int> OnTimerUpdate;
    public event Action OnTimerEnd;

    public void StartTimer()
    {
        Observable.FromCoroutine<int>(observer => Countdown(Counter, observer))
            .Subscribe(
                i => UpdateCounter(i),
                e => Debug.LogError($"Countdown error: {e.Message}"),
                () => OnTimeUp());
    }

    private void UpdateCounter(int counter)
    {
        if (OnTimerUpdate != null)
        {
            OnTimerUpdate(counter);
        }
    }
    
    private void OnTimeUp()
    {
        if (OnTimerEnd != null)
        {
            OnTimerEnd();
        }
    }

    private IEnumerator Countdown(int duration, IObserver<int> observer)
    {
        if (duration < 0)
        {
            observer.OnError(new ArgumentOutOfRangeException(nameof(duration)));
        }

        var count = duration;
            
        while (count > 0)
        {
            observer.OnNext(count);
            count--;
            yield return new WaitForSeconds(1);
        }

        observer.OnCompleted();
    }    
}
