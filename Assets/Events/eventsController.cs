using System;

public class eventsController
{
    public Action baseEvent;

    public void addListener(Action listener) => baseEvent += listener;
    public void removeListener(Action listener) => baseEvent -= listener;
    public void InvokeEvent() => baseEvent?.Invoke();
}
