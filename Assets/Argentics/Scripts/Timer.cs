using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class Timer : MonoBehaviour
{
    private float secondsElapsed;
    [Serializable] class TimerEvent : UnityEvent<string> { }
    [SerializeField] private TimerEvent timerEvent;

    // Start is called before the first frame update
    void Start()
    {
        secondsElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        secondsElapsed += Time.deltaTime;
        int minutes = (int) secondsElapsed / 60;
        int seconds = (int) secondsElapsed % 60;
        timerEvent.Invoke($"{minutes}:{seconds:00}");
    }
}
    