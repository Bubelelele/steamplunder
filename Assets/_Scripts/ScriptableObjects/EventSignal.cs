using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EventSignal : ScriptableObject {

    public List<SignalListener> Listeners = new();

    public void Raise() {
        for (int i = Listeners.Count - 1; i >= 0; i--) {
            Listeners[i].OnSignalRaised();
        }
    }

    public void RegisterListener(SignalListener listener) => Listeners.Add(listener);

    public void DeregisterListener(SignalListener listener) => Listeners.Remove(listener);
}