using System;
using UnityEngine;

public class Timer
{
    private float _time;
    private float _maxTime;
    public float CurrentTime => _time;

    public enum TimerReset { Automatic, Manual }
    private TimerReset _timerReset;

    public event Action OnTimerDone;

    public void CountTimer()
    {
        if (_time > 0)
        {
            _time -= Time.fixedDeltaTime;
        }
        else if (_time <= 0)
        {
            OnTimerDone?.Invoke();
            if (_timerReset == TimerReset.Automatic) ResetTimer();
        }
    }
    public void ResetTimer()
    {
        _time = _maxTime;
    }

    public Timer(float time, TimerReset timerReset = TimerReset.Automatic)
    {
        _maxTime = time;
        _time = time;
        _timerReset = timerReset;
    }
}