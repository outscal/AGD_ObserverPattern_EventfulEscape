using System;

public class EventController
{
    public Action baseEvent;
    public void AddListener(Action listener) => baseEvent += listener;
    public void RemoveListener(Action listener) => baseEvent -= listener;
    public void InvokeEvent() => baseEvent?.Invoke();
}

public class EventController<T>
{
    public Action<T> baseClaasEvent;


    public void AddLister(Action<T> lister) => baseClaasEvent += lister;
   
    public void RemoveLister(Action<T> lister) => baseClaasEvent -= lister;


    public void InvokeEvent(T type)
    {

        if (baseClaasEvent != null) baseClaasEvent.Invoke(type);

    }

}