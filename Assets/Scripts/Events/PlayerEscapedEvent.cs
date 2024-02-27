using UnityEngine;

public class PlayerEscapedEvent : MonoBehaviour
{
    [SerializeField] private SoundType soundToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>() != null)
        {
            GameService.Instance.GetSoundView().PlaySoundEffects(soundToPlay);
            EventService.Instance.OnPlayerEscapedEvent.InvokeEvent();
        }
    }
}
