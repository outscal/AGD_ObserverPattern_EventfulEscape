
public class EventService 
{
    private static EventService instance;
    public static EventService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventService();
            }
            return instance;
        }
    }

    public eventsController OnLightSwitchToggled { get; private set; }

    public EventService()
    {
        OnLightSwitchToggled = new eventsController();
    }
}
