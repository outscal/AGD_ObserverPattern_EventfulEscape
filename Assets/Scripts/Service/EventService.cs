using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService : MonoBehaviour
{
    private static EventService instance;
    public static EventService Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new EventService();
            }
            return instance;
        }
    }

    public EventController lightToggledAction {  get; private set; }

    public EventService()
    {
        lightToggledAction = new EventController();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
