using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Timer will be used to call methods after some delay 
*/

public class Timer : MonoBehaviour
{
    private class TimedEvent
    {
        public float timeToExecute;
        public Callback Method;
    }

    public delegate void Callback();
    private List<TimedEvent> events;

    private void Awake()
    {
        events = new List<TimedEvent>();
    }

    public void Add(Callback method, float inSeconds)
    {
        events.Add(new TimedEvent { Method = method, timeToExecute = Time.time + inSeconds });
    }

    private void Update()
    {
        if (events.Count == 0)
            return;

        for (int i = 0; i < events.Count; i++)
        {
            var timedEvent = events[i];
            if(timedEvent.timeToExecute <= Time.time)
            {
                timedEvent.Method();
                events.Remove(timedEvent);
            }

        }
    }
}
