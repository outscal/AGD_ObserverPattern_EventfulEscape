using UnityEngine;

public class KeyView : MonoBehaviour, IInteractable
{
    [SerializeField] private SoundType soundType;
    public void Interact()
    {
        int currentKeys = GameService.Instance.GetPlayerController().KeysEquipped;
        GameService.Instance.GetInstructionView().HideInstruction();
        GameService.Instance.GetSoundView().PlaySoundEffects(soundType);
        EventService.Instance.OnKeyPickedUp.InvokeEvent(++currentKeys);
        gameObject.SetActive(false);
    }
}