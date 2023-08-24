
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
    

    //int x = 5;
    //public int getx()
    //{
    //    return x;
    //}

    //public void setx(int value)
    //{
    //    x = value;

    //}

    //public int X { get { return x; } private set { x = value; } }

    //public int X { get;private set; }
    public EventController OnLightSwitchToggled { get; private set; }
    public EventController<int> OnKeyPickedUp { get; private set; }

    public EventService()
    {
        OnLightSwitchToggled = new EventController();
    }
}
