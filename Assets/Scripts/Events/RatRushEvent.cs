using UnityEngine;

public class RatRushEvent : MonoBehaviour
{
    [SerializeField] private Transform rats;
    [SerializeField] private Transform target;
    [SerializeField] private SoundType soundToPlay;

    private float speed = 7.5f;
    private bool rushActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>() != null)
        {
            onRatRush();
            EventService.Instance.OnRatRush.InvokeEvent();
            GameService.Instance.GetSoundView().PlaySoundEffects(soundToPlay);
            GetComponent<Collider>().enabled = false;
        }
    }

    void Update()
    {
        if (rushActive)
        {
            ratMovement();
        }
    }

    private void ratMovement()
    {
        if (rats.position != target.position)
        {
            rats.position = Vector3.MoveTowards(rats.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            rushActive = false;
            rats.gameObject.SetActive(false);
        }
    }

    private void onRatRush()
    {
        rats.gameObject.SetActive(true);
        rushActive = true;
    }
}
