using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour {

    public EventSignal eventSignal;
    public UnityEvent signalEvent;
    
    public void OnSignalRaised() => signalEvent.Invoke();

    private void OnEnable() => eventSignal.RegisterListener(this);

    private void OnDisable() => eventSignal.DeregisterListener(this);
}