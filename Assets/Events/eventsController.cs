using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventsController
{
    public Action baseEvent;

    public void addListener(Action listener) => baseEvent += listener;
    public void removeListener(Action listener) => baseEvent -= listener;
    public void InvokeAction(Action listener) => baseEvent?.Invoke();
}
