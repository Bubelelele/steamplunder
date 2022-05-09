using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerHandler : MonoBehaviour {

    [SerializeField] private string onTriggerEnterTag;
    [SerializeField] private UnityEvent onTriggerEnter;
    
    [SerializeField] private string onTriggerExitTag;
    [SerializeField] private UnityEvent onTriggerExit;

    private void OnTriggerEnter(Collider other) {
        if (onTriggerEnterTag == String.Empty) onTriggerEnter.Invoke();
        else if (other.CompareTag(onTriggerEnterTag)) onTriggerEnter.Invoke();
    }

    private void OnTriggerExit(Collider other) {
        if (onTriggerExitTag == String.Empty) onTriggerExit.Invoke();
        else if (other.CompareTag(onTriggerExitTag)) onTriggerExit.Invoke();
    }
}