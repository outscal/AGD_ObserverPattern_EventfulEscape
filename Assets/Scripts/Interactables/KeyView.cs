using UnityEngine;

public class KeyView : MonoBehaviour, IInteractable
{
    [SerializeField] private SoundType soundType;

    public void Interact()
    {

        int currentKey = GameService.Instance.GetPlayerController().KeysEquipped;
        GameService.Instance.GetInstructionView().HideInstruction();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.KeyPickUp);
        currentKey++;
        EventService.Instance.OnKeyPickedUp.InvokeEvent(currentKey);
        GameService.Instance.GetSoundView().PlaySoundEffects(soundType);
        EventService.Instance.OnKeyPickedUp.InvokeEvent(++currentKey);
        gameObject.SetActive(false);
    }
}
