using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public Action baseEvent;

    public void AddListener(Action Listener) => baseEvent += Listener;
    
    public void RemoveListener(Action Listener) => baseEvent -= Listener;

    public void InvokeEvent() => baseEvent?.Invoke();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
