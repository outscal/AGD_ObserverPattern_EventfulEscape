using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController
{
    public Action baseEvent;
    public void AddListener(Action Listener) => baseEvent += Listener;

    public void RemoveListener(Action Listener) => baseEvent -= Listener;

    public void InvokeEvent() => baseEvent?.Invoke();

}

public class EventController<T>
{
    public Action<T> baseEvent;
    public void AddListener(Action<T> listener) => baseEvent += listener;
    public void RemoveListener(Action<T> listener) => baseEvent -= listener;
    public void InvokeEvent(T type) => baseEvent?.Invoke(type);
}

